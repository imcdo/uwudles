using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace uwudles.UI {
    public class UwudleUI : MonoBehaviour
    {
        [SerializeField] private GameObject portrait;
        [SerializeField] private Slider HealthBar;
        private Outline portraitOutline;
        public Uwudle uwudle;

        private void Start() {
            portrait.GetComponent<Image>().sprite = uwudle.Portrait;
            portraitOutline = portrait.GetComponent<Outline>();
            HealthBar.value = uwudle.Health.Hp;
            uwudle.Health.HpListener.AddListener(updateHealth);
        }

        public void enableOutline(bool enable){
            portraitOutline.enabled = enable;
        }
        
        public void updateHealth(){
            HealthBar.value = (float)uwudle.Health.Hp / uwudle.Health.MaxHp;
        }

        private void OnDestroy() {
            uwudle.Health.HpListener.RemoveListener(updateHealth);
        }
        
    }
}
