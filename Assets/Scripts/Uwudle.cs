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
    [RequireComponent(typeof(MovementBrain))]
    public class Uwudle : MonoBehaviour
    {
        private Damagable _health;
        public Damagable Health => _health ? _health : _health = GetComponent<Damagable>();
        private Coroutine _goToRoutine;

        private NavMeshAgent _nme;
        public NavMeshAgent NavAgent => _nme ? _nme : _nme = GetComponent<NavMeshAgent>();

        private MovementBrain _mBrain;
        public MovementBrain Movement => _mBrain ? _mBrain : _mBrain = GetComponent<MovementBrain>();
        public Sprite Portrait;
        private void Start()
        {
            Movement.Move();
        }
    }
}
