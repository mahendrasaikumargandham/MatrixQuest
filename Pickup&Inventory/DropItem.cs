using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public int itemRadius;
    public GameObject MissionQuest;
    public PlayerMovement player;
    public ShootingController shootingController1;
    public ShootingController shootingController2;
    public ShootingController shootingController3;
    public ShootingController shootingController4;
    public GameObject UI;

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < itemRadius)
        {
            if (Input.GetKeyDown("f"))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                shootingController1.GetComponent<ShootingController>().enabled = false;
                shootingController2.GetComponent<ShootingController>().enabled = false;
                shootingController3.GetComponent<ShootingController>().enabled = false;
                shootingController4.GetComponent<ShootingController>().enabled = false;
                MissionQuest.SetActive(true);
                Time.timeScale = 0f;
                UI.SetActive(false);
                Debug.Log("Question 2");
            }
        }
    }
}
