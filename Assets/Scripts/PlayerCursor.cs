using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles {
    public class PlayerCursor : MonoBehaviour {
        [SerializeField] private float range;
        [SerializeField] private LayerMask enemyUwudleLayer;
        private Ray cameraRay;
        private GameObject cursor;
        private GameObject targetUwudle;
        void Update() {
            // send out a ray from the center of the screen  
            RaycastHit hit;
            cameraRay = Camera.main.ViewportPointToRay(new Vector3(.5f,.5f,0));
            bool uwudleHit = Physics.Raycast(cameraRay, out hit, range, enemyUwudleLayer);
            // if an enemy uwudle is hit set its cursor active
            if(uwudleHit){
                if(cursor != null){
                   cursor.SetActive(false); 
                }  
                targetUwudle = hit.collider.gameObject;
                Transform statCanvas = targetUwudle.transform.Find("StatsCanvas");
                cursor = statCanvas.Find("Cursor").gameObject;
                cursor.SetActive(true);
            }else{
                // turn off cursor once you stop hovering
                if(cursor != null){
                   cursor.SetActive(false); 
                }   
                targetUwudle = null;
            }
        }

        /// <summary>
        /// Returns the enemy uwudle that the player is targeting/looking at
        /// </summary>
        /// <returns>The GameObject that represents the targeted enemy
        /// uwudle. Will return null if no enemy is being targeted</returns>
        public Uwudle getTargetUwudle(){
            return targetUwudle.GetComponent<Uwudle>();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(cameraRay.origin, cameraRay.direction * range);
        }
    }
}
