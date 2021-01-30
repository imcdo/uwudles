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
            PlayerStats.Instance.PartyMembers.Add(newWudle);
            FollowBrain followBrain = newWudle.GetComponent<FollowBrain>();
            followBrain.Target = PlayerStats.Instance.transform;
            UI.FaceTransform faceTransform = newWudle.GetComponentInChildren<UI.FaceTransform>();
            faceTransform.Target = PlayerStats.Instance.transform;
            newWudleObj.SetActive(true);
        }
    }
}
