using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Item info")]
    public int itemRadius;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;
    public PlayerMovement player;
    // public Inventory inventory;
    // public GameObject collectWeapons;


    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < itemRadius)
        {
            if (Input.GetKeyDown("f"))
            {
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                weapon3.SetActive(false);
                weapon4.SetActive(false);
                weapon5.SetActive(false);
                weapon6.SetActive(false);
                // collectWeapons.SetActive(false);
            }
        }
    }
}
