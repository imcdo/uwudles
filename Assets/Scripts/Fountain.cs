using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour , IInteractableObject
{
    // [SerializeField]
    // private float interactionRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAction()
    {
        Debug.Log("Hi you interacted with the fountain");
    }
}
