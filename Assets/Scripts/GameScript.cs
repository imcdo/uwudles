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

    // DIALOGUE
    [SerializeField]
    private InterfaceManager voicebox;
    
    [Header("Opening cutscene")]

    public DialogueData[] hellos;
    public float[] helloDelays;
    public int helloIdx = 0;

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

        // Candy
        SetCandy(0);
        candyBar.SetActive(false);

        // Begin dialogue
        candyShowcase.SetActive(false);

        TryHello();
    }

    void TryHello()
    {

        if (helloIdx >= hellos.Length)
        {
            EndHellos();
        }
        else
        {
            StartCoroutine(HelloLoop());
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
