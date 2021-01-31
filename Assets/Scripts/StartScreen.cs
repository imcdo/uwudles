using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(BeginLoop());
    }

    IEnumerator BeginLoop()
    {
        yield return new WaitForSeconds(startDelay);
        SceneManager.LoadScene("Main");
    }
}
