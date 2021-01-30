using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles
{
    public class Fountain : MonoBehaviour , IInteractableObject
    {
        [SerializeField]
        private int summonCost;
        [SerializeField]
        private GameObject fountainMenuObject;
        [SerializeField]
        private GameObject sacrificeMenuObject;
        private bool menuUp = false;
        private SacrificeMenuPortrait[] SacrificeFrames;
        private UwudleSpawner uwudleSpawner;
        void Start()
        {
            fountainMenuObject.SetActive(false);
            SacrificeFrames = GetComponentsInChildren<SacrificeMenuPortrait>();
            uwudleSpawner = GetComponent<UwudleSpawner>();
            foreach(SacrificeMenuPortrait portraitFrame in SacrificeFrames)
            {
                portraitFrame.gameObject.SetActive(false);
            }
            
            
        }
        public void DoAction()
        {
            Debug.Log("Hi you interacted with the fountain");
            
            fountainMenuObject.SetActive(true);
            menuUp = true;
            PlayerStats.Instance.MouseLook.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            // if(PlayerStats.Instance.NumGuts >= summonCost)
            // {
                // PlayerStats.Instance.NumGuts -= summonCost;
                // Debug.Log("Yuh" + " " + PlayerStats.Instance.NumGuts + " Guts Left");
                
            // }
            // else
            // {
            //     Debug.Log("Nuh" + " " + PlayerStats.Instance.NumGuts + " Guts Left");
            // }
        }

        public void OnSacrificeClicked()
        {
            Debug.Log("Clicked Sacrifice");
            fountainMenuObject.SetActive(false);
            sacrificeMenuObject.SetActive(true);
        }

        public void OnRollClicked()
        {
            Debug.Log("Clicked Roll");
            fountainMenuObject.SetActive(false);
            if(PlayerStats.Instance.NumPartyMembers >= PlayerStats.Instance.MaxPartySize)
            {
                sacrificeMenuObject.SetActive(true);
            }
            else{
                if(PlayerStats.Instance.NumGuts >= summonCost)
                {
                    // if()
                    PlayerStats.Instance.NumGuts -= summonCost;
                    Debug.Log("Yuh" + " " + PlayerStats.Instance.NumGuts + " Guts Left");
                    uwudleSpawner.SpawnUwudle();
                    OnQuitMenuClicked();
                }
                else
                {
                    Debug.Log("Nuh" + " " + PlayerStats.Instance.NumGuts + " Guts Left");
                    if(PlayerStats.Instance.NumPartyMembers > 0)
                    {

                    }
                }
            }
        }

        private void SetupSacrificeCanvas()
        {
            for(int i = 0; i < PlayerStats.Instance.NumPartyMembers; ++i)
            {
                SacrificeFrames[i].gameObject.SetActive(true);
            }
        }
        
        public void OnQuitMenuClicked()
        {
            fountainMenuObject.SetActive(false);
            foreach(SacrificeMenuPortrait portraitFrame in SacrificeFrames)
            {
                portraitFrame.gameObject.SetActive(false);
            }
            sacrificeMenuObject.SetActive(false);
            menuUp = false;
            PlayerStats.Instance.MouseLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }


    }
}
