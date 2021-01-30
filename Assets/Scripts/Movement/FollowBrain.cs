using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace uwudles
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowBrain : MovementBrain
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _spacing = 2;

        private void Awake()
        {
            NavMeshAgent nma = GetComponent<NavMeshAgent>();
            if (_target)
                Strategy = new FollowStrategy(nma, _target) { Spacing = _spacing };
            else
                Strategy = MovementStrategy.Idle;
        }
    }
}
