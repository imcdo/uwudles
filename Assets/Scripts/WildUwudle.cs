using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles
{
    [RequireComponent(typeof(Uwudle))]
    public class WildUwudle : MonoBehaviour
    {
        private Uwudle _uwudle;
        public Uwudle Uwudle => _uwudle ? _uwudle : _uwudle = GetComponent<Uwudle>();
        
        private void OnDestroy()
        {
            WildUwudleManager.Instance.Remove(this);
        }
    }
}

