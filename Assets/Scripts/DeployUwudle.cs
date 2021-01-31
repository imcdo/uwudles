using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles {
    public class DeployUwudle : MonoBehaviour
    {
        [SerializeField] private PlayerCursor cursor;
        [SerializeField] private bool inBattle;
        [SerializeField] private float _spacing = 3;
        public Poof poofPrefab;

        private Battle currentBattle;
        private void Update() {
            // send active uwudle
            if(!inBattle && Input.GetMouseButtonDown(0)){
                Uwudle targetUwudle = cursor.getTargetUwudle();
                Uwudle activeUwudle = InventoryController.Instance.getActiveUwudle();
                if(targetUwudle != null && activeUwudle != null){
                    startbattle(activeUwudle, targetUwudle);
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
            boundsOffset += _spacing;

            // ensure that teleport position is infront of the target uwudle
            Vector3 tpPos = target.transform.position;
            tpPos += boundsOffset * target.transform.forward;
            if (poofPrefab != null)
            {
                Instantiate(poofPrefab, ourUwudle.transform.position, Quaternion.identity);
                Instantiate(poofPrefab, tpPos, Quaternion.identity);
            }
            ourUwudle.NavAgent.Warp(tpPos);
            
            ourUwudle.NavAgent.updateRotation = false;
            target.NavAgent.updateRotation = false;

            ourUwudle.transform.LookAt(target.transform.position);
            target.transform.LookAt(ourUwudle.transform.position);

            ourUwudle.NavAgent.updateRotation = true;
            target.NavAgent.updateRotation = true;
        }

        private void startbattle(Uwudle ourUwudle, Uwudle target){
            MovementStrategy ourOldStrat = ourUwudle.Movement.Strategy;
            MovementStrategy targetOldStrat = target.Movement.Strategy;
            ourUwudle.Movement.Stop();
            target.Movement.Stop();
            ourUwudle.Movement.Strategy = MovementStrategy.Idle;
            target.Movement.Strategy = MovementStrategy.Idle;
            teleportUwudle(ourUwudle, target);

            currentBattle = new Battle(ourUwudle, target);
            inBattle = true;
            currentBattle.Start((Uwudle winner) => endBattle(winner, ourUwudle, target, ourOldStrat, targetOldStrat));
        }

        public void endBattle(Uwudle winner, Uwudle ourUwudle, Uwudle target, MovementStrategy ourOldStrat, MovementStrategy targetOldStrat){
            inBattle = false;
            ourUwudle.Movement.Strategy = ourOldStrat;
            target.Movement.Strategy = targetOldStrat;
            ourUwudle.Movement.Move();
            target.Movement.Move();

            ourUwudle.Health.Hp = ourUwudle.Health.MaxHp;
            var wild = target.GetComponent<WildUwudle>();
            if (wild && target.Health.Hp == 0){
                wild.Kill();
                ourUwudle.LevelUp();
            }
        }
    }
}
