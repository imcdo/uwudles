using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScript : MonoBehaviour
{
    private static GameScript _instance;
    public static GameScript Instance => _instance ? _instance : _instance = FindObjectOfType<GameScript>();
    public GameObject candyBar;
    public TextMeshProUGUI candyQtyObj;
    public GameObject candyShowcase;
    public uwudles.Fountain fountain;

    // DIALOGUE
    [SerializeField]
    private InterfaceManager voicebox;
    
    [Header("Opening cutscene")]

    public DialogueData[] hellos;
    public float[] helloDelays;
    public int helloIdx = 0;
    public IEnumerator activeHelloLoop;

    [Header("donut touch")]
    public int candyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        helloIdx = 0;
        fountain.interactState = uwudles.Fountain.InteractState.Intro;

        // Candy
        SetCandy(0);
        candyBar.SetActive(false);

        // Begin dialogue
        candyShowcase.SetActive(false);

        TryHello();
    }

    void TryHello()
    {
        voicebox.onDialogueEnd.RemoveListener(TryHello);
        if (helloIdx >= hellos.Length)
        {
            EndHellos();
        }
        else
        {
            activeHelloLoop = HelloLoop();
            StartCoroutine(activeHelloLoop);
        }
    }

    IEnumerator HelloLoop()
    {
        yield return new WaitForSeconds(helloDelays[helloIdx]);
        voicebox.onDialogueEnd.AddListener(TryHello);
        voicebox.ActivateDialogue(hellos[helloIdx]);
        helloIdx += 1;
    }

    void EndHellos()
    {
        Debug.Log("done with hellos");
    }

    public void FountainIntro()
    {
        StopCoroutine(activeHelloLoop);
        voicebox.onDialogueEnd.RemoveListener(TryHello);
        Debug.Log("Time for fountain intro");
    }

    void EndIntro()
    {
        voicebox.onDialogueEnd.RemoveListener(EndIntro);
        Debug.Log("end intro");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCandy(int qty)
    {
        SetCandy(candyCount + qty);
    }

    public void SetCandy(int qty)
    {
        candyCount = qty;
        uwudles.PlayerStats.Instance.NumGuts = candyCount;
        candyQtyObj.text = candyCount.ToString();
    }
}
