using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGlow : MonoBehaviour
{
    public Missions missions;
    public Player player;
    public GameObject gameSaved;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.SavePlayer();
            Debug.Log("Game Saved");
            StartCoroutine(GameSaved());
        }
    }

    IEnumerator GameSaved() {
        gameSaved.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        gameSaved.SetActive(false);
    }
}
