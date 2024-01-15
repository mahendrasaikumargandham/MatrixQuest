using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Weapons")]
    public GameObject weapon1;
    public bool isWeapon1Picked = false;

    public GameObject weapon2;
    public bool isWeapon2Picked = false;
    
    public GameObject weapon3;
    public bool isWeapon3Picked = false;

    public GameObject weapon4;
    public bool isWeapon4Picked = false;

    public GameObject weapon5;
    public bool isWeapon5Picked = false;

    public GameObject weapon6;
    public bool isWeapon6Picked = false;
    
    public SwitchCamera switchCamera;
    public Rifle rifleScript;
    public GameObject TPC;
    public GameObject AC;
    
    [Header("Inventory")]
    public GameObject inventory;
    bool isPause = false;

    void Update() {
        if(Input.GetKeyDown("tab")) {
            if(isPause) {
                HideInventory();
            }
            else {
                ShowInventory();
            }
        }
    }

    void ShowInventory() {
        switchCamera.GetComponent<SwitchCamera>().enabled = false;
        rifleScript.GetComponent<Rifle>().enabled = false;
        TPC.SetActive(false);
        AC.SetActive(false);

        inventory.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }

    void HideInventory() {
        inventory.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;

        switchCamera.GetComponent<SwitchCamera>().enabled = true;
        rifleScript.GetComponent<Rifle>().enabled = true;
        TPC.SetActive(true);
        AC.SetActive(true);
    }
}
