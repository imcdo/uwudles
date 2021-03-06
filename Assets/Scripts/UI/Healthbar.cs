using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace uwudles.UI
{
    [RequireComponent(typeof(Image))]
    public class Healthbar : MonoBehaviour
    {
        public TextMeshProUGUI healthNum;
        private RectTransform _rt;
        private float _fullX;
        [SerializeField] private Damagable _damagable;
        [SerializeField] private float _displayTime;

        private void Awake()
        {
            _rt = GetComponent<RectTransform>();
            _fullX = _rt.rect.width * _rt.localScale.x;
            _damagable.HpListener.AddListener(UpdateBar);
            healthNum.text = ((int) _damagable.Hp).ToString();
        }

        private void UpdateBar()
        {
            healthNum.text = ((int) _damagable.Hp).ToString();
            float fullness = 1.0f - (float)_damagable.Hp / _damagable.MaxHp;
            _rt.sizeDelta = new Vector2(-fullness * _fullX, _rt.sizeDelta.y);
            
        }
    }
}

