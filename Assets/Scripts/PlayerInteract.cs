using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uwudles
{

    public class PlayerInteract : MonoBehaviour
    {
        public GameObject interactIndicator;

        [SerializeField]
        private float interactRange;
        void Start()
        {
            
        }

        void Update()
        {
            if(!PlayerStats.Instance.InMenu && Input.GetKeyDown("f"))
            {
                TryInteracting();
            }

/*
            // lol sorry
            RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactRange))
                {
                    Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(Vector3.forward) * interactRange),Color.white, 20);
                    IInteractableObject objectHit = hit.transform.GetComponent<IInteractableObject>();

                    SetIndicator(objectHit != null);
                }
                */
        }

        private void SetIndicator(bool active)
        {
            Debug.Log(active);
            interactIndicator.SetActive(active);
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