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

    // DIALOGUE
    [SerializeField]
    private InterfaceManager voicebox;
    [SerializeField]
    private DialogueData introDialogue;
    public GameObject candyShowcase;
    public float introDialogueDelay = 1.0f;

    [Header("donut touch")]
    public int candyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        // Candy
        SetCandy(0);
        candyBar.SetActive(false);

        // Begin dialogue
        candyShowcase.SetActive(false);

        StartCoroutine(IntroDelay());
    }

    IEnumerator IntroDelay()
    {
        yield return new WaitForSeconds(introDialogueDelay);
        Debug.Log("intro pls");

        voicebox.onDialogueEnd.AddListener(EndIntro);
        voicebox.ActivateDialogue(introDialogue);
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
