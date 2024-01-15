using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandMap : MonoBehaviour
{
    public GameObject mainMapCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mainMapCanvas.SetActive(true);
        }
    }
}
