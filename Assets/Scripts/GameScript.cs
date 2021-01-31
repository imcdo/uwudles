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

    [Header("Fountain Intro")]
    public DialogueData fountainIntro;
    public float candyDelay = 2.0f;

    [Header("Candy Explain")]
    public DialogueData candyExplain;

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
        fountain.interactState = uwudles.Fountain.InteractState.None;

        Debug.Log("Time for fountain intro");
        voicebox.onDialogueEnd.AddListener(CandyShowcase);
        voicebox.ActivateDialogue(fountainIntro);
    }

    void CandyShowcase()
    {
        voicebox.onDialogueEnd.RemoveListener(CandyShowcase);
        candyShowcase.SetActive(true);
        StartCoroutine(ExplainCandy());
    }

    IEnumerator ExplainCandy()
    {
        // TA DA
        candyBar.SetActive(true);

        yield return new WaitForSeconds(candyDelay);
    
        SetCandy(10);
        candyShowcase.SetActive(false);
        voicebox.onDialogueEnd.AddListener(TimeToPlay);
        voicebox.ActivateDialogue(candyExplain);
    }

    void TimeToPlay()
    {
        voicebox.onDialogueEnd.RemoveListener(TimeToPlay);
        fountain.interactState = uwudles.Fountain.InteractState.Normal;
        Debug.Log("time to playyy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCandy(int qty)
    {
        SetCandy(uwudles.PlayerStats.Instance.NumGuts + qty);
    }

    public void SetCandy(int qty)
    {
        uwudles.PlayerStats.Instance.NumGuts = qty;
        candyQtyObj.text = uwudles.PlayerStats.Instance.NumGuts.ToString();
    }
}
