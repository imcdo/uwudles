using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace uwudles
{
    public class Fountain : MonoBehaviour , IInteractableObject
    {
        public enum InteractState
        {
            Normal,
            Intro,
            None
        }
        public InteractState interactState = InteractState.Normal;

        [SerializeField]
        private int summonCost;
        [SerializeField]
        private GameObject fountainMenuObject;
        [SerializeField]
        private GameObject sacrificeMenuObject;
        [SerializeField]
        private GameObject fountainDialogueObject;
        private bool menuUp = false;
        private SacrificeMenuPortrait[] SacrificeFrames;
        private UwudleSpawner uwudleSpawner;
        [SerializeField]
        private InterfaceManager dialogueScript;
        [SerializeField]
        private DialogueData fountainDialogue;
        void Start()
        {
            fountainMenuObject.SetActive(false);
            SacrificeFrames = GetComponentsInChildren<SacrificeMenuPortrait>();
            Debug.Log(SacrificeFrames.Length);
            uwudleSpawner = GetComponent<UwudleSpawner>();
            foreach(SacrificeMenuPortrait portraitFrame in SacrificeFrames)
            {
                portraitFrame.gameObject.SetActive(false);
                Debug.Log(portraitFrame.name);
            }
            sacrificeMenuObject.SetActive(false);
        }

        public void DialogueEndCheck()
        {
            fountainMenuObject.SetActive(true);
            dialogueScript.onDialogueEnd.RemoveListener(DialogueEndCheck);
        }
        public void DoAction()
        {
            switch (interactState)
            {
                case InteractState.Normal:
                    Debug.Log("Hi you interacted with the fountain");
                    
                    menuUp = true;
                    PlayerStats.Instance.MouseLook.enabled = false;
                    PlayerStats.Instance.InMenu = true;
                    Cursor.lockState = CursorLockMode.None;
                    // fountainMenuObject.SetActive(true);
                    fountainDialogueObject.SetActive(true);
                    dialogueScript.onDialogueEnd.AddListener(DialogueEndCheck);
                    dialogueScript.ActivateDialogue(fountainDialogue);
                    break;
                case InteractState.Intro:
                    GameScript.Instance.FountainIntro();
                    break;
                default:
                    break;
            }
        }

        

        public void OnSacrificeClicked()
        {
            Debug.Log("Clicked Sacrifice");
            if(PlayerStats.Instance.NumPartyMembers == 0)
            {
                OnQuitMenuClicked();
            }
            else
            {
                fountainMenuObject.SetActive(false);
                sacrificeMenuObject.SetActive(true);
                SetupSacrificeCanvas();
            }
        }

        public void OnRollClicked()
        {
            Debug.Log("Clicked Roll");
            fountainMenuObject.SetActive(false);
            if(PlayerStats.Instance.NumPartyMembers >= PlayerStats.Instance.MaxPartySize)
            {
                OnSacrificeClicked();
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
                        OnSacrificeClicked();
                    }
                    else
                    {
                        OnQuitMenuClicked();
                    }
                }
            }
        }

        private void SetupSacrificeCanvas()
        {
            Debug.Log(PlayerStats.Instance.NumPartyMembers);
            for(int i = 0; i < PlayerStats.Instance.NumPartyMembers; ++i)
            {
                SacrificeFrames[i].gameObject.SetActive(true);
                Image portrait = SacrificeFrames[i].GetComponentInChildren<Image>();
                TextMeshProUGUI levelText = SacrificeFrames[i].GetComponentInChildren<TextMeshProUGUI>();
                levelText.text = "Level: " + PlayerStats.Instance.PartyMembers[i].level;
                portrait.sprite = PlayerStats.Instance.PartyMembers[i].Portrait;
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
            fountainDialogueObject.SetActive(false);
            menuUp = false;
            PlayerStats.Instance.InMenu = false;
            PlayerStats.Instance.MouseLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnSacrificeMinionClicked(int uwudleNum)
        {
            Debug.Log("Trying to Sacrifice Minion #" + uwudleNum + 1);
            Debug.Log("numpartymembers" + PlayerStats.Instance.NumPartyMembers);
            Uwudle uwudleToSacrifice = PlayerStats.Instance.PartyMembers[uwudleNum];
            if(uwudleNum < PlayerStats.Instance.NumPartyMembers - 1)
            {
                Uwudle nextUwudle = PlayerStats.Instance.PartyMembers[uwudleNum + 1];
                if(uwudleNum == 0)
                {
                    Debug.Log("Sacrificing first minion");
                    nextUwudle.Movement.Strategy = new FollowStrategy(nextUwudle.NavAgent, PlayerStats.Instance.transform);
                }
                else
                {  
                    Debug.Log("Sacrificing minion " + uwudleNum + 1);
                    nextUwudle.Movement.Strategy = new FollowStrategy(nextUwudle.NavAgent, PlayerStats.Instance.PartyMembers[uwudleNum - 1].transform);
                }
            }
            PlayerStats.Instance.PartyMembers.RemoveAt(uwudleNum);
            InventoryController.Instance.removeUwudle(uwudleToSacrifice);
            Destroy(uwudleToSacrifice.gameObject);
            OnQuitMenuClicked();
        }
    }
}
