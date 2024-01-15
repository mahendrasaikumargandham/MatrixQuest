using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] weapons;
    [SerializeField] private int currentWeaponIndex = 0;

    void Start()
    {
        SwitchWeapon(currentWeaponIndex);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(3);
        }
    }
    void SwitchWeapon(int newIndex)
    {
        weapons[currentWeaponIndex].SetActive(false);
        weapons[newIndex].SetActive(true);
        currentWeaponIndex = newIndex;
    }
}
