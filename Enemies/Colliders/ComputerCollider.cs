using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerCollider : MonoBehaviour
{
    private Collider myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Transfering the data..");
        }

        if (myCollider != null)
        {
            myCollider.enabled = false;
        }
    }
}
