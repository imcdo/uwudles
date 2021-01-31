using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles
{
    public class UwudleSpawner : MonoBehaviour
    {
        private static UwudleSpawner _instance;
        public static UwudleSpawner Instance => _instance ? _instance : _instance = FindObjectOfType<UwudleSpawner>();
        [SerializeField]
        private GameObject uwudlePrefab;
        public Transform spawnLocation;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SpawnUwudle()
        {
            
            GameObject newWudleObj = GameObject.Instantiate(uwudlePrefab, spawnLocation.position, spawnLocation.rotation, null) as GameObject;
            Uwudle newWudle = newWudleObj.GetComponent<Uwudle>();
            
            FollowBrain followBrain = newWudle.GetComponent<FollowBrain>();
            if(PlayerStats.Instance.NumPartyMembers > 0)
            {
                newWudle.Movement.Strategy = new FollowStrategy(newWudle.NavAgent, PlayerStats.Instance.PartyMembers[PlayerStats.Instance.NumPartyMembers - 1].transform);
                followBrain.Target = PlayerStats.Instance.PartyMembers[PlayerStats.Instance.NumPartyMembers - 1].transform;
            }
            else
            {
                newWudle.Movement.Strategy = new FollowStrategy(newWudle.NavAgent, PlayerStats.Instance.transform);
                followBrain.Target = PlayerStats.Instance.transform;
            }
            PlayerStats.Instance.PartyMembers.Add(newWudle);
            UI.FaceTransform faceTransform = newWudle.GetComponentInChildren<UI.FaceTransform>();
            faceTransform.Target = PlayerStats.Instance.transform;
            newWudleObj.name = "uwudle" + PlayerStats.Instance.NumPartyMembers;
            newWudleObj.SetActive(true);
            Debug.Log("Spawned an Uwudle, numPartyMembers: " + PlayerStats.Instance.NumPartyMembers);
        }
    }
}
