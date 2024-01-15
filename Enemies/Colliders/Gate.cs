using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject DoorAnimation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoorAnimation.SetActive(true);
            Debug.Log("Door Opened..");
        }
    }
}
