using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace uwudles
{
    public class MovementBrain : MonoBehaviour
    {
        public enum MovementState { Stoped, Moving }
        public MovementState BrainState { get; private set; }

        public float Speed { get; private set; }
        private Vector3 _prevPos;

        private MovementStrategy _strategy;
        public MovementStrategy Strategy
        {
            set
            {
                if (_strategy != null)
                    _strategy.Dispose();
                _strategy = value;
            }
            get => _strategy;
        }

        public void Move()
        {
            if (Strategy == null) return;

            if (BrainState != MovementState.Moving)
                Strategy.OnMove();
            BrainState = MovementState.Moving;
            Strategy.Move();
        }
        
        public void Stop()
        {
            if (BrainState != MovementState.Stoped)
                Strategy.OnStop();
            BrainState = MovementState.Stoped;
            Strategy.Stop();
        }

        private void Awake()
        {
            _prevPos = transform.position;
        }

        public void Update()
        {
            if (BrainState == MovementState.Moving)
                Strategy.Move();
        }

        public void FixedUpdate()
        {
            Speed = (transform.position - _prevPos).magnitude / Time.fixedDeltaTime;
            _prevPos = transform.position;
        }

        public void OnDestroy()
        {
            Strategy?.Dispose();
        }
    }
}
