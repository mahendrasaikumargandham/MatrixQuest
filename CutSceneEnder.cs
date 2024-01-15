using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnder : MonoBehaviour
{
    public GameObject videoPlayer;
    public GameObject videoSource;
    public GameObject splash;

    void Start()
    {
        Application.targetFrameRate = -1;
        StartCoroutine(DisableCutScene());
    }

    IEnumerator DisableCutScene()
    {
        yield return new WaitForSeconds(110.2f);
        videoPlayer.SetActive(false);
        videoSource.SetActive(false);
        splash.SetActive(false);
    }

}
