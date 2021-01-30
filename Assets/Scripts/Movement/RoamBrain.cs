using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace uwudles
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RoamBrain : MovementBrain
    {
        [SerializeField] private float _roamRange = 10;
        [SerializeField] private int _roamTime = 10_000;

        private void Awake()
        {
            var nma = GetComponent<NavMeshAgent>();
            Strategy = new RoamStrategy(nma) { RoamRange = _roamRange, RoamTime = _roamTime };
        }
    }
}
