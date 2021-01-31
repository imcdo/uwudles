using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Poof : MonoBehaviour
    {
        public float time = .5f;

        private void Awake()
        {
            StartCoroutine(PoofRoutine());
        }

        private IEnumerator PoofRoutine()
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}
