using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [SerializeField]
    private InterfaceManager voicebox;
    [SerializeField]
    private DialogueData introDialogue;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        /*
        voicebox.onDialogueEnd.AddListener(EndIntro);
        voicebox.ActivateDialogue(introDialogue);
        */
    }

    void EndIntro()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
