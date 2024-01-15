using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingPanel;
    public bool ContinueGamePlay = false;

    public static MainMenu instance;

    private void Awake()
    {
        instance = this;
    }
    public void StartGame()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadGame());
    }

    public void ContinueGame()
    {
        Debug.Log("Retrieving Game Data");
        ContinueGamePlay = true;
        loadingPanel.SetActive(true);
        StartCoroutine(LoadGame());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("scene_night");
    }
}
