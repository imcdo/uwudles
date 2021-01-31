using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.AI;

namespace uwudles
{
    public enum Element { Fire, Water, Grass }

    [RequireComponent(typeof(Damagable))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(MovementBrain))]
    [RequireComponent(typeof(Animator))]
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

        [SerializeField] private Transform _hatMountPoint;
        [SerializeField] private SkinnedMeshRenderer _smr;

        public Transform HatMountPoint => _hatMountPoint;

        public int Damage => 10;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Movement.Move();
        }

        private void Update()
        {
            if (Movement.Speed != 0)
            {
                _animator.Play("Walk");
            }
            else
            {
                _animator.Play("Idle");
            }
        }


        public void SetSkin(Material skinMat)
        {
            Material[] mats = new Material[_smr.materials.Length]; 
            Array.Copy(_smr.materials, mats, _smr.materials.Length);
            mats[0] = skinMat;
            _smr.materials = mats;
        }

        public void AttackAnimation(Transform target)
        {
            _animator.Play("Attack");
        }
    }
}
