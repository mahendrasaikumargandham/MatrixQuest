using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenUI : MonoBehaviour
{
    public GameObject MissionFailed;
    public Player player;
    public PlayerMovement playerHealthStatus;
    public bool showDeadScreen = false;

    void Update()
    {
        if (showDeadScreen)
        {
            MissionFailed.SetActive(true);
            StartCoroutine(DisableMissionFailed());
        }
    }


    IEnumerator DisableMissionFailed()
    {
        yield return new WaitForSeconds(4f);
        showDeadScreen = false;
        playerHealthStatus.currentHealth = 100;
        player.LoadPlayer();
        MissionFailed.SetActive(false);
    }
}
