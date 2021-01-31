using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.AI;
using UnityEngine.Events;

namespace uwudles
{
    public enum Element { Plant, Wet, Hot }

    [RequireComponent(typeof(Damagable))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(MovementBrain))]
    [RequireComponent(typeof(Animator))]
    public class Uwudle : MonoBehaviour
    {
        public static readonly Dictionary<Element, Color> ElementColors = new Dictionary<Element, Color>() {
            { Element.Plant, Color.green },
            { Element.Wet, Color.blue },
            { Element.Hot, Color.red }
        };


        public static readonly Dictionary<Element, Dictionary<Element, float>> ElementDamageModifiers = new Dictionary<Element, Dictionary<Element, float>>()
        {
            { Element.Plant , new Dictionary<Element, float>(){ { Element.Plant, .75f}, { Element.Wet, 1.5f }, { Element.Hot, .75f} } },
            { Element.Wet , new Dictionary<Element, float>(){ { Element.Plant, .75f}, { Element.Wet, .75f }, { Element.Hot, 1.5f} } },
            { Element.Hot , new Dictionary<Element, float>(){ { Element.Plant, 1.5f }, { Element.Wet, .75f }, { Element.Hot, .75f} } }
        };

        private Damagable _health;
        public Damagable Health => _health ? _health : _health = GetComponent<Damagable>();
        private Coroutine _goToRoutine;

        private NavMeshAgent _nme;
        public NavMeshAgent NavAgent => _nme ? _nme : _nme = GetComponent<NavMeshAgent>();

        private MovementBrain _mBrain;
        public MovementBrain Movement => _mBrain ? _mBrain : _mBrain = GetComponent<MovementBrain>();
        public Sprite Portrait;

        public Element Element;

        [SerializeField] private Transform _hatMountPoint;
        [SerializeField] private SkinnedMeshRenderer _smr;
        [SerializeField] private ProjectileFire _projectilePrefab;
        [SerializeField] private Transform _projectileSpawn;

        public Transform HatMountPoint => _hatMountPoint;

        [SerializeField] private int _MaxDamage;
        [SerializeField] private int _MinSDamage;
        private int _damage;
        public int Damage {
            set{
                _damage = value;
            }
            get{
                return UnityEngine.Random.Range(_MinSDamage, _MaxDamage);
            }

        }

        public int CalcDamage(Element otherElement)
        {
            float mod = ElementDamageModifiers[this.Element][otherElement];
            return (int)(mod * Damage);
        }

        private Animator _animator;
        private Coroutine _damageRoutine;
        [SerializeField]private AnimationCurve _damageRoutineCurve;

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
            _animator.SetFloat("Speed", Movement.Speed);
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
            Debug.Log("attack animation");
            _animator.SetTrigger("Attack");
            GetComponent<AudioSource>().Play();
            ProjectileFire p = Instantiate(_projectilePrefab, _projectileSpawn.transform.position, _projectileSpawn.transform.rotation);
            p._projectileColor = ElementColors[Element];
            p.Shoot(target.position);
        }

        public void AnimateDamage(int damage, float t)
        {
            if (_damageRoutine != null) StopCoroutine(_damageRoutine);
            _damageRoutine = StartCoroutine(DamageRoutine(Health.Hp - damage, t  * .9f));
        }

        private IEnumerator DamageRoutine(int hp, float endt)
        {
            float t = 0;
            int startHp = Health.Hp;
            while (t < endt)
            {
                float v = _damageRoutineCurve.Evaluate(t / endt);
                print(t);
                print(endt);
                Health.Hp = Mathf.RoundToInt(Mathf.Lerp(startHp, hp, v));
                yield return null;
                t += Time.deltaTime;
            }
            Health.Hp = hp;
        }

        private int _level = 1;
        public int level {
            private set{
                _level = value;
                levelListener.Invoke();
            }
            get{
                return _level;
            }
        }
        public UnityEvent levelListener = new UnityEvent();

        public void LevelUp(){
            level = Mathf.Clamp(level + 1, 1, 3);
        }
    }
}
