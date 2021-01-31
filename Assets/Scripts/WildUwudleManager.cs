using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace uwudles
{
    public class WildUwudleManager : MonoBehaviour
    {
        private static WildUwudleManager _instance;
        public static WildUwudleManager Instance => _instance ? _instance : _instance = FindObjectOfType<WildUwudleManager>();

        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private int _maxWandering = 20;
        [SerializeField] private UwudleBuilder _builder;
        [SerializeField] private LayerMask enemyUwudleLayer;

        private HashSet<WildUwudle> _wildUwudles = new HashSet<WildUwudle>();

        public void UpdateWilds()
        {
            Random r = new Random();
            while (_wildUwudles.Count < _maxWandering)
            {
                SpawnWildUwudle(r.Next(_spawnPoints.Length));
            }
        }

        public void Remove(WildUwudle uwudle)
        {
            _wildUwudles.Remove(uwudle);
        }

        private void SpawnWildUwudle(int spawnId)
        {
            Uwudle uwudle = _builder.BuildRandom();
            uwudle.transform.position = _spawnPoints[spawnId].position;
            uwudle.transform.rotation = _spawnPoints[spawnId].rotation;
            var wild = uwudle.gameObject.AddComponent<WildUwudle>();
            uwudle.gameObject.layer = enemyUwudleLayer;

            uwudle.Movement.Strategy = new RoamStrategy(uwudle.NavAgent) { RoamTime=1000, RoamRange=10 };
            
            UI.FaceTransform ft = uwudle.GetComponentInChildren<UI.FaceTransform>();
            if (ft)
                ft.Target = PlayerStats.Instance.transform;

            _wildUwudles.Add(wild);
        }

        private void Start()
        {
            UpdateWilds();
        }
    }
}
