using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace uwudles
{
    public class MovementBrain : MonoBehaviour
    {
        public MovementStrategy.MovementState MovementState { get; private set; }


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
            if (MovementState != MovementStrategy.MovementState.Moving)
                Strategy.OnMove();
            MovementState = MovementStrategy.MovementState.Moving;
            Strategy.Move();
        }
        
        public void Stop()
        {
            if (MovementState != MovementStrategy.MovementState.Stoped)
                Strategy.OnStop();
            MovementState = MovementStrategy.MovementState.Stoped;
            Strategy.Stop();
        }

        public void Update()
        {
            if (MovementState == MovementStrategy.MovementState.Moving)
                Strategy.Move();
        }

        public void OnDestroy()
        {
            Strategy.Dispose();
        }
    }
}
