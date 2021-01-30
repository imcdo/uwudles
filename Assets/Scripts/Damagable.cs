using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace uwudles
{
    public class Damagable : MonoBehaviour
    {

        [SerializeField]  private int _hp = 10;
        public int Hp 
        { 
            set 
            {
                _hp = Mathf.Clamp(value, 0, MaxHp);
                HpListener.Invoke();
            }
            get
            {
                return _hp;
            }
        }

        public int MaxHp { get; private set; }
        public UnityEvent HpListener = new UnityEvent();

        public void UpdateMaxHp(int maxHp)
        {
            if (Hp == MaxHp)
                Hp = MaxHp = maxHp;
            else
                MaxHp = maxHp;
        
        }

        private void Awake()
        {
            MaxHp = Hp;
        }
        
        public void Damage(int damage)
        {
            Hp -= damage;
        }

        public void Heal(int health)
        {
            Hp += health;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("hurt");
                Damage(1);
            }
        }
    }
}
