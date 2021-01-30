using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles 
{
    [RequireComponent(typeof(Damagable))]
    public class UwuduleScaleWithHealth : MonoBehaviour
    {
        private Damagable _health;
        public Damagable Health => _health ? _health : _health = GetComponent<Damagable>();
    }
}
