using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace uwudles.UI {
    public class UwudleUI : MonoBehaviour
    {
        [SerializeField] private Outline portrait;
        [SerializeField] private Slider HealthBar;
        [SerializeField] private TextMeshProUGUI level;
        public Uwudle uwudle;

        private void Start() {
            portrait.GetComponent<Image>().sprite = uwudle.Portrait;
            HealthBar.value = uwudle.Health.Hp;
            level.text = uwudle.level + "";
            uwudle.Health.HpListener.AddListener(updateHealth);
            uwudle.levelListener.AddListener(updateLevel);
        }

        public void enableOutline(bool enable){
            portrait.enabled = enable;
        }
        
        public void updateHealth(){
            HealthBar.value = (float)uwudle.Health.Hp / uwudle.Health.MaxHp;
        }

        public void updateLevel(){
            level.text = uwudle.level + "";
        }

        private void OnDestroy() {
            uwudle.Health.HpListener.RemoveListener(updateHealth);
        }
        
    }
}
