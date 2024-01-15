using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMCollider : MonoBehaviour
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
            Debug.Log("There is a secret at businessman room 2nd floor");
        }
        if (myCollider != null)
        {
            myCollider.enabled = false;
        }
    }
}
