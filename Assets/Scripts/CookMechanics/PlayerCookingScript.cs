using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCookingScript : MonoBehaviour
{
    [SerializeField] private string targetTag; //You need to give to this variable your food model's tag
    [SerializeField] private float attachDistance; //Max distance between player and food's model
    [SerializeField] private KeyCode attachKey; //Key to take the food from the table
    [SerializeField] private Vector3 foodPos; //Food's position to player

    private GameObject attachedObject; //Variable takes the taken by player model
    private Rigidbody attachedRigidbody;

    private void Update()
    {
        if (Input.GetKeyDown(attachKey))
        {
            if (attachedObject == null)
            {
                TryAttachObject();
            }
            else
            {
                DetachObject();
            }
        }
    }

    private void TryAttachObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attachDistance) && hit.collider.CompareTag(targetTag))
        {
            attachedObject = hit.collider.gameObject;
            attachedObject.transform.SetParent(transform);
            attachedObject.transform.localPosition = foodPos;

            attachedRigidbody = attachedObject.GetComponent<Rigidbody>();
            if (attachedRigidbody != null)
            {
                attachedRigidbody.useGravity = false;
                attachedRigidbody.isKinematic = true;
            }
        }
    }

    private void DetachObject()
    {
        if (attachedRigidbody != null)
        {
            attachedRigidbody.isKinematic = false;
            attachedRigidbody.useGravity = true;
        }

        attachedObject.transform.SetParent(null);
        attachedObject = null;
    }
}
