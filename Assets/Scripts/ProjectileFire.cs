using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles
{
    [RequireComponent(typeof(Renderer))]
    public class ProjectileFire : MonoBehaviour
    {
        [SerializeField] private float _maxScale = 1.2f;
        [SerializeField] private float _minScale = .2f;
        [SerializeField] private float _explodeScale = 3;
        [SerializeField] [Range(0, 1)] private float _shootPoint = .5f;
        [SerializeField] private float _explodeLen = .5f;
        [SerializeField] private float _len = 5;
        [SerializeField] private Color _projectileColor = Color.blue;

        public void Shoot(Vector3 target)
        {
            StartCoroutine(ShootRoutine(_len, _explodeLen, target));
        }

        IEnumerator ShootRoutine(float len, float explodeLen, Vector3 target)
        {
            float t = 0;
            float shootT = len * _shootPoint;
            Material m = GetComponent<Renderer>().material;
            
            transform.localScale = _minScale * Vector3.one;
            Vector3 startPos = transform.position;
            Vector3 minScale = _minScale * Vector3.one;
            Vector3 maxScale = _maxScale * Vector3.one;
            Color c = _projectileColor;
            while (t < shootT)
            {
                transform.localScale = Vector3.Lerp(minScale, maxScale, t / shootT);

                yield return null;
                t += Time.deltaTime;
            }
            transform.localScale = maxScale;
            while (t < len)
            {
                transform.position = Vector3.Lerp(startPos, target, (t - shootT) / (len - shootT));
                yield return null;
                t += Time.deltaTime;
            }
            transform.position = target;

            t = 0;
            Vector3 explodeScale = _explodeScale * Vector3.one;
            while (t < explodeLen)
            {
                transform.localScale = Vector3.Lerp(maxScale, explodeScale, t / explodeLen);
                m.SetVector("Color_63f4b09226cc4af3990ff2683cc7f7be", new Color(c.r, c.g, c.b,  (1 - t / explodeLen)));
                yield return null;
                t += Time.deltaTime;
            }

            Destroy(gameObject);
        }
    }
}
