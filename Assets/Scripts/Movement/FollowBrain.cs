using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace uwudles
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowBrain : MovementBrain
    {
        public Transform Target;
        [SerializeField] private float _spacing = 2;

        private void Awake()
        {
            NavMeshAgent nma = GetComponent<NavMeshAgent>();
            if (Target)
                Strategy = new FollowStrategy(nma, Target) { Spacing = _spacing };
            else
                Strategy = MovementStrategy.Idle;
        }
    }
}
