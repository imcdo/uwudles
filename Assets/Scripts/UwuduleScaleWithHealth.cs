using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles 
{
    [RequireComponent(typeof(Damagable))]
    public class UwuduleScaleWithHealth : MonoBehaviour
    {
        [SerializeField] private float scaleForSize;
        [SerializeField] private float minScale;
        private Damagable _health;
        public Damagable Health => _health ? _health : _health = GetComponent<Damagable>();
        private Vector3 _startScale;

        private void Start() {
            _startScale = transform.localScale;
            updateScale();
            Health.HpListener.AddListener(updateScale);
        }

        private void updateScale(){
            float scale = Health.Hp/scaleForSize;
            transform.localScale = Vector3.Lerp(minScale * _startScale, _startScale, scale);
        }
    }
}
