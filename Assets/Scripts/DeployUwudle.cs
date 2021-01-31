using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles {
    public class DeployUwudle : MonoBehaviour
    {
        [SerializeField] private PlayerCursor cursor;
        [SerializeField] private bool inBattle;
        private void Update() {
            // send active uwudle
            if(Input.GetMouseButtonDown(0)){
                Uwudle targetUwudle = cursor.getTargetUwudle();
                Uwudle activeUwudle = InventoryController.Instance.getActiveUwudle();
                if(targetUwudle != null && activeUwudle != null){
                    teleportUwudle(activeUwudle, targetUwudle);
                }
            }
        }

        private void teleportUwudle(Uwudle ourUwudle, Uwudle target){
            Bounds? ourBounds = FollowStrategy.GetMeshBounds(ourUwudle);
            Bounds? targetBounds = FollowStrategy.GetMeshBounds(target);

            float boundsOffset = 0;
            // calculates the distance that the two uwudles should stand 
            // from eachother's center points
            if(ourBounds.HasValue){
                boundsOffset += ourBounds.Value.size.z * ourUwudle.transform.lossyScale.z;
            }
            if(targetBounds.HasValue){
                boundsOffset += targetBounds.Value.size.z * target.transform.lossyScale.z;
            }
            boundsOffset /= 2;

            // ensure that teleport position is infront of the target uwudle
            Vector3 tpPos = target.transform.position;
            tpPos += boundsOffset * target.transform.forward;
            
            ourUwudle.transform.position = tpPos;
        }

        /* private void StartBattle(){

        } */
    }
}
