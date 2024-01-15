using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour
{
    public GameObject Question1;
    public GameObject Question2;
    public GameObject Question3;
    void Start()
    {
        Question1.SetActive(false);
        Question2.SetActive(false);
        Question3.SetActive(false);
    }
}
