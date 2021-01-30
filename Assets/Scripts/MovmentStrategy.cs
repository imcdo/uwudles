using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace uwudles
{
    public abstract class MovementStrategy
    {
        public static readonly IdleStrategy Idle = new IdleStrategy();

        public enum MovementState { Stoped, Moving }

        public abstract void Move();
        public virtual void OnMove() { }
        public virtual void Stop() { }
        public virtual void OnStop() { }
    }

    public class IdleStrategy : MovementStrategy
    {
        public override void Move() { }
    }

    public class FollowStrategy : MovementStrategy
    {
        private NavMeshAgent _agent;
        private Transform _target;
        public float BehindFactor { set; get; } // between 0 and 1
        public float Spacing { set; get; }

        public FollowStrategy(NavMeshAgent agent, Transform target)
        {
            _agent = agent;
            _target = target;
        }

        public override void Move()
        {
            if (Vector3.Distance(_agent.transform.position, _target.position) > Spacing)
                MoveAgent();
        }

        private void MoveAgent()
        {
            Vector3 destination = _target.position - (_target.forward * BehindFactor +
                (_target.position - _agent.transform.position).normalized * (1 - BehindFactor)).normalized * Spacing;

            _agent.destination = destination;
        }
    }
}
