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
        public Color _projectileColor = Color.blue;

        [SerializeField] Renderer _otherRenderer;

        public void Shoot(Vector3 target)
        {
            StartCoroutine(ShootRoutine(_len, _explodeLen, target));
        }

        IEnumerator ShootRoutine(float len, float explodeLen, Vector3 target)
        {
            float t = 0;
            float shootT = len * _shootPoint;
            Material m = GetComponent<Renderer>().material;
            Material m2 = _otherRenderer.material;
            transform.localScale = _minScale * Vector3.one;
            Vector3 startPos = transform.position;
            Vector3 minScale = _minScale * Vector3.one;
            Vector3 maxScale = _maxScale * Vector3.one;
            m.SetColor("Color_122854f14d5940b1af97e43cdbfbb7df", _projectileColor);
            m2.SetColor("Color_44eab29d1f85437d8f22173486c8da5e", _projectileColor);

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
                m.SetColor("Color_122854f14d5940b1af97e43cdbfbb7df", new Color(_projectileColor.r, _projectileColor.g, _projectileColor.b,  (1 - t / explodeLen)));
                m2.SetColor("Color_44eab29d1f85437d8f22173486c8da5e", new Color(_projectileColor.r, _projectileColor.g, _projectileColor.b,  (1 - t / explodeLen)));
                yield return null;
                t += Time.deltaTime;
            }

            Destroy(gameObject);
        }
    }
}
