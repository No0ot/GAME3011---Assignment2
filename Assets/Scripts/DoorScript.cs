using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Rigidbody rigidbody;
    bool locked = true;

    [SerializeField]
    bool canInteract;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(canInteract)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.StartLockpickingMinigame(this);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    public void Unlock()
    {
        locked = false;
        rigidbody.isKinematic = false;
    }
}
