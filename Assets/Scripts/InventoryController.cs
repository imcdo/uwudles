using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles {
    public class InventoryController : MonoBehaviour {
        private static InventoryController _instance;
        public static InventoryController Instance => _instance ? _instance : _instance = FindObjectOfType<InventoryController>();
        [SerializeField] private Uwudle[] startUwudles;
        [SerializeField] private UI.UwudleUI uwudleUIPrefab;
        [SerializeField] private Transform uwudleUIContainer;
        private Dictionary<Uwudle, UI.UwudleUI> activeUwudles = new Dictionary<Uwudle, UI.UwudleUI>();
        private List<Uwudle> orderedUwudles = new List<Uwudle>();
        private Uwudle activeUwu;
        private int currentIndex;

        void Awake() {
            foreach(Uwudle uwudle in startUwudles){
                addUwudle(uwudle);
            }
            activeUwu = (orderedUwudles.Count > 0) ? orderedUwudles[0] : null;
        }

        void Update() {
            if(orderedUwudles.Count > 0){
                playerScroll();
            }
        }

        private void playerScroll(){
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if(activeUwu == null){
                activeUwu = orderedUwudles[currentIndex];
            }
            activeUwudles[activeUwu].enableOutline(false);
            if(scroll < 0){
                // scroll up
                currentIndex = (currentIndex + 1) % orderedUwudles.Count;
                activeUwu = orderedUwudles[currentIndex];
            }else if(scroll > 0){
                // scroll down
                currentIndex = (currentIndex == 0) ? (orderedUwudles.Count - 1) : (currentIndex - 1);
                activeUwu = orderedUwudles[currentIndex];
            }
            activeUwudles[activeUwu].enableOutline(true);
        }

        public void addUwudle(Uwudle uwudle){
            if(uwudle == null){
                return;
            }
            orderedUwudles.Add(uwudle);
            UI.UwudleUI ui = Instantiate(uwudleUIPrefab);
            ui.uwudle = uwudle;
            ui.transform.SetParent(uwudleUIContainer);
            activeUwudles.Add(uwudle, ui);
      
            if(orderedUwudles.Count == 1){
                currentIndex = 0;
                activeUwu = orderedUwudles[currentIndex];
            }
        }

        public void removeUwudle(Uwudle uwudle){
            Debug.Log("remove uwudleUI");
            UI.UwudleUI ui = activeUwudles[uwudle];
            activeUwudles.Remove(uwudle);
            orderedUwudles.Remove(uwudle);
            Destroy(ui.gameObject);

            currentIndex = 0;
            if(orderedUwudles.Count > 0){
                activeUwu = orderedUwudles[currentIndex];
            }else{
                activeUwu = null;
            }
        }

        public Uwudle getActiveUwudle(){
            return activeUwu;
        }
    }
}
