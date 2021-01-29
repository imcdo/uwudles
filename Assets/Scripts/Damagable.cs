using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace uwudles
{
    public class Damagable : MonoBehaviour
    {
        UnityEvent Healthlistener = new UnityEvent();

        [SerializeField] private int _startHp = 10;
        public int Hp { set; get; }

        private void Awake()
        {
            Hp = _startHp;
        }

        void OnValidate()
        {
            Hp = _startHp;
        }
    }
}
