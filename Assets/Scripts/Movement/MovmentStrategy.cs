using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace uwudles
{
    public abstract class MovementStrategy : IDisposable
    {
        public static readonly IdleStrategy Idle = new IdleStrategy();

        public enum MovementState { Stoped, Moving }

        public abstract void Move();
        public virtual void OnMove() { }
        public virtual void Stop() { }
        public virtual void OnStop() { }

        public virtual void Dispose() { }
    }

    public class IdleStrategy : MovementStrategy
    {
        public override void Move() { }
    }

    public class FollowStrategy : MovementStrategy
    {
        private NavMeshAgent _agent;
        private Transform _target;
        private float _tRadius = 0;
        private float _aRadius = 0;
        public float BehindFactor { set; get; } // between 0 and 1
        public float Spacing { set; get; }

        public FollowStrategy(NavMeshAgent agent, Transform target)
        {
            _agent = agent;
            _target = target;
            Bounds? tb = GetMeshBounds(_target);
            if (tb.HasValue)
                _tRadius = (tb.Value.max - tb.Value.min).magnitude / 2;

            Bounds? ab = GetMeshBounds(_target);
            if (ab.HasValue)
                _aRadius = (ab.Value.max - ab.Value.min).magnitude / 2;
        }

        public static Bounds? GetMeshBounds(Component mb)
        {
            var tsm = mb.GetComponent<SkinnedMeshRenderer>();
            if (tsm == null)
                tsm = mb.GetComponentInChildren<SkinnedMeshRenderer>();
            if (tsm != null)
                return tsm.sharedMesh.bounds;

            var tcol = mb.GetComponent<Collider>();
            if (tcol == null)
                tcol = mb.GetComponentInChildren<Collider>();
            if (tcol != null)
            {
                Vector3 scaler =
                    new Vector3(1 / tcol.transform.lossyScale.x, 1 / tcol.transform.lossyScale.y, 1 / tcol.transform.lossyScale.z);
                Bounds ret = new Bounds();
                ret.min = Vector3.Scale(tcol.bounds.min, scaler);
                ret.max = Vector3.Scale(tcol.bounds.max, scaler);
                return ret;
            }

            var tmf = mb.GetComponent<MeshFilter>();
            if (tmf == null)
                tmf = mb.GetComponentInChildren<MeshFilter>();
            if (tmf != null)
                return tmf.sharedMesh.bounds;

            return null;
        }

        public override void Move()
        {
            if (Vector3.Distance(_agent.transform.position, _target.position) > Spacing)
                MoveAgent();
        }

        private void MoveAgent()
        {
            float tRadius = Mathf.Max(_target.lossyScale.x, _target.lossyScale.y, _target.lossyScale.z) * _tRadius;
            float aRadius = Mathf.Max(_agent.transform.lossyScale.x, _agent.transform.lossyScale.y, _agent.transform.lossyScale.z) * _aRadius;
            // Debug.Log(aRadius);
            Vector3 followDirection = -(_target.forward * BehindFactor +
                (_target.position - _agent.transform.position).normalized * (1 - BehindFactor)).normalized;

            Vector3 destination = _target.position + followDirection * Spacing + followDirection * (tRadius + aRadius);

            _agent.destination = destination;
        }
    }

    public class FollowOffsetStrategy : MovementStrategy
    {
        private NavMeshAgent _agent;
        private Transform _target;
        public Vector3 Offset { set; get; }

        public FollowOffsetStrategy(NavMeshAgent agent, Transform target)
        {
            _agent = agent;
            _target = target;
        }

        public override void Move()
        {
            if (Vector3.Distance(_agent.transform.position, _target.position) > Offset.magnitude)
                MoveAgent();
        }

        private void MoveAgent()
        {
            Vector3 destination = _target.position + Offset;

            _agent.destination = destination;
        }
    }

    public class RoamStrategy : MovementStrategy
    {
        public int RoamTime { set; get; }
        public float RoamRange { set; get; }
        private NavMeshAgent _agent;
        private readonly CancellationTokenSource _cancel = new CancellationTokenSource();
        private Task _roamHandle;

        public RoamStrategy(NavMeshAgent agent)
        {
            _agent = agent;
        }


        public override void Move()
        {
        }

        public override async void OnMove()
        {
            try
            {
                await RoamTask(_cancel.Token);
            }
            catch (TaskCanceledException) { }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private async Task RoamTask(CancellationToken cancel)
        {
            while (!cancel.IsCancellationRequested)
            {
                Vector3 dir = UnityEngine.Random.insideUnitSphere * RoamRange;
                Vector3 destination = _agent.transform.position + dir;
                _agent.destination = destination;
                await Task.Delay(RoamTime, cancel);
            }
        }

        public override void OnStop()
        {
            _cancel.Cancel();
            _cancel.Dispose();
        }

        public override void Dispose()
        {
            OnStop();
        }
    }
}
