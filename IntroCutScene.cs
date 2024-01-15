using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutScene : MonoBehaviour
{
    public GameObject cutSceneEnder;
    public GameObject cutScenePanel;
    public GameObject UI;

    private void Start()
    {
        StartCoroutine(CutSceneAnimation());
    }

    IEnumerator CutSceneAnimation()
    {
        cutSceneEnder.SetActive(true);
        cutScenePanel.SetActive(true);
        UI.SetActive(false);
        yield return new WaitForSecondsRealtime(113f);
        cutSceneEnder.SetActive(false);
        cutScenePanel.SetActive(false);
        UI.SetActive(true);
    }
}
