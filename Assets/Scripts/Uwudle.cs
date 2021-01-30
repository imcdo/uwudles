using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.AI;

namespace uwudles
{

    [RequireComponent(typeof(Damagable))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Uwudle : MonoBehaviour
    {
        private Damagable _health;
        public Damagable Health => _health ? _health : _health = GetComponent<Damagable>();
        private Coroutine _goToRoutine;

        private NavMeshAgent _nme;
        public NavMeshAgent Movement => _nme ? _nme : _nme = GetComponent<NavMeshAgent>();

        private MovementStrategy _movementStrategy;
        private Vector3 _prevPosition;

        public MovementStrategy.MovementState MovementState { get; private set; }

        public Transform FollowTarget;

        private void Awake()
        {
            _movementStrategy = MovementStrategy.Idle;
            _prevPosition = transform.position;

            if (FollowTarget) Follow(FollowTarget);
        }

        public void Update()
        {
            /*
            if (Vector3.Distance(_prevPosition, transform.position) >= float.Epsilon)
            {
                if (MovementState == MovementStrategy.MovementState.Stoped)
                    _movementStrategy.OnMove();
                _movementStrategy.Move();
                MovementState = MovementStrategy.MovementState.Moving;

            }
            else
            {
                if (MovementState == MovementStrategy.MovementState.Moving)
                    _movementStrategy.OnStop();
                _movementStrategy.Stop();
                MovementState = MovementStrategy.MovementState.Stoped;
            } */
            _movementStrategy.Move();
        }

        public void Follow(Transform target) 
        {
            FollowStrategy strat = new FollowStrategy(Movement, target);
            strat.BehindFactor = 0;
            strat.Spacing = 2f;
            _movementStrategy = strat;
        }

        public void StopFollowing()
        {
            _movementStrategy = MovementStrategy.Idle;
        }
    }
}
