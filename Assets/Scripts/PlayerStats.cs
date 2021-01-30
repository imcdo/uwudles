using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles
{
    public class PlayerStats : MonoBehaviour
    {
        private static PlayerStats _instance;
        public static PlayerStats Instance => _instance ? _instance : _instance = FindObjectOfType<PlayerStats>();
        public int NumGuts;
        public int NumPartyMembers => PartyMembers.Count;
        [HideInInspector]
        public List<Uwudle> PartyMembers;
        public LookWithMouse MouseLook;
        public bool InMenu = false;
        public int MaxPartySize;
        void Start()
        {
            
        }
        void Update()
        {
            
        }
    }
}
