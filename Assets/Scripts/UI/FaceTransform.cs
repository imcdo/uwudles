using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles.UI
{
    public class FaceTransform : MonoBehaviour
    {
        public Transform Target;

        private void Awake()
        {
            if (Target == null)
                Target = Camera.main.transform;
        }

        private void Update()
        {
            if (Target)
                transform.LookAt(transform.position + Target.forward);
        }
    }
}
