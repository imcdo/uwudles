using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField]
        private float interactRange;
        void Start()
        {
            
        }

        void Update()
        {
            if(Input.GetKeyDown("f"))
            {
                TryInteracting();
            }
        }

        private void TryInteracting()
        {
            RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactRange))
                {
                    Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(Vector3.forward) * interactRange),Color.white, 20);
                    IInteractableObject objectHit = hit.transform.GetComponent<IInteractableObject>();
                    if(objectHit != null)
                    {
                        objectHit.DoAction();
                    }
                }
        }
    }
}