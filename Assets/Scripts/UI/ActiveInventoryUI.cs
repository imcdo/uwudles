using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace uwudles.UI {
    public class ActiveInventoryUI : MonoBehaviour {
        [SerializeField] private GameObject[] uwudlesUI;
        private GameObject activeUwudle;

        void Awake() {
            activeUwudle = uwudlesUI[0];
            activeUwudle.transform.Find("Portrait").GetComponent<Outline>().enabled = true;
        }
        
        public void setActiveUI(int index){
            activeUwudle.transform.Find("Portrait").GetComponent<Outline>().enabled = false;
            activeUwudle = uwudlesUI[index];
            activeUwudle.transform.Find("Portrait").GetComponent<Outline>().enabled = true;
        }

        public void setUwudleHealth(int index, float health){
            uwudlesUI[index].transform.Find("HealthBar").GetComponent<Slider>().value = health;
        }
    }
}
