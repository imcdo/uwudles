using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles.UI
{
    public class FaceTransform : MonoBehaviour
    {
        public Transform Target;

        private void Update()
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, -Target.forward);
        }
    }
}
