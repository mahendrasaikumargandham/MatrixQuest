using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    // public GameObject miniMap;
    // public GameObject health;
    // public GameObject rifle;
    // public SwitchCamera switchCamera;
    // public Rifle rifleScript;
    // public GameObject TPC;
    // public GameObject AC;
    public static bool GameIsStopped = false;

    private string playerPositionKey = "PlayerPosition";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsStopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void SavePlayerPosition()
    {

    }

    public void LoadPlayerPosition()
    {
        // Load the player position from PlayerPrefs
        float x = PlayerPrefs.GetFloat(playerPositionKey + "_x", 0f);
        float y = PlayerPrefs.GetFloat(playerPositionKey + "_y", 0f);
        float z = PlayerPrefs.GetFloat(playerPositionKey + "_z", 0f);

        // Set the player position
        transform.position = new Vector3(x, y, z);

        Debug.Log("Player position loaded.");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        // switchCamera.GetComponent<SwitchCamera>().enabled = true;
        // rifleScript.GetComponent<Rifle>().enabled = true;
        // miniMap.SetActive(true);
        // health.SetActive(true);
        // rifle.SetActive(true);
        // TPC.SetActive(true);
        // AC.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("scene_night");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        // miniMap.SetActive(false);
        // health.SetActive(false);
        // rifle.SetActive(false);
        // switchCamera.GetComponent<SwitchCamera>().enabled = false;
        // rifleScript.GetComponent<Rifle>().enabled = false;
        // TPC.SetActive(false);
        // AC.SetActive(false);

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsStopped = true;
    }
}
