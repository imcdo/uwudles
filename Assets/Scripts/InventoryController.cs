using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles {
    public class InventoryController : MonoBehaviour {
        [SerializeField] private GameObject[] uwudles;
        [SerializeField] private UI.ActiveInventoryUI PlayerCanvas;
        private GameObject activeUwu;
        private int currentIndex;

        void Awake() {
            activeUwu = uwudles[0];   
        }

        void Update() {
            playerScroll();
            updateUI();

        }

        private void playerScroll(){
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if(scroll < 0){
                // scroll up
                currentIndex = (currentIndex + 1) % uwudles.Length;
                PlayerCanvas.setActiveUI(currentIndex);
                activeUwu = uwudles[currentIndex];
            }else if(scroll > 0){
                // scroll down
                currentIndex = (currentIndex == 0) ? (uwudles.Length - 1) : (currentIndex - 1);
                PlayerCanvas.setActiveUI(currentIndex);
                activeUwu = uwudles[currentIndex];
            }
        }

        private void updateUI(){
            for(int i = 0; i < uwudles.Length; i++){
                GameObject uwu = uwudles[i];
                if(uwu != null){
                    PlayerCanvas.enableUwudleUI(i, true);
                    Uwudle u = uwu.GetComponent<Uwudle>();
                    PlayerCanvas.setUwudleHealth(i, u.Health.Hp);
                    PlayerCanvas.setSpriteAt(i, u.Portrait);
                }else{
                    PlayerCanvas.enableUwudleUI(i, false);
                }
                
            }
        }

        /// <summary>
        /// Changes the uwudle at the given index
        /// </summary>
        public void changeUwudleAt(int index, GameObject uwudle){
            uwudles[index] = uwudle;
        }

        /// <summary>
        /// Returns an array of the active Uwudles. Can direclty modify the array
        /// to change the active Uwudles. UI will be automatically updated too.
        /// </summary>
        public GameObject[] getUwudles(){
            return uwudles;
        }

        /// <summary>
        /// Returns the currelty active uwudle gameobject
        /// </summary>
        public GameObject getActiveUwudle(){
            return activeUwu;
        }
    }
}
