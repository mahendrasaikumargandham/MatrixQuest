using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour
{
    public bool Mission1 = false;
    public bool Mission2 = false;
    public bool Mission3 = false;
    public bool Mission4 = false;
    public bool Mission5 = false;
    public bool Mission6 = false;
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;
    public GameObject map5;

    public Text missionsText;

    private void Update()
    {
        if (Mission1 == false && Mission2 == false && Mission3 == false && Mission4 == false && Mission5 == false && Mission6 == false)
        {
            missionsText.text = "Locate save point and save the game";
            map1.SetActive(true);
            map2.SetActive(false);
            map3.SetActive(false);
            map4.SetActive(false);
            map5.SetActive(false);
        }
        if (Mission1 == true && Mission2 == false && Mission3 == false && Mission4 == false && Mission5 == false && Mission6 == false)
        {
            missionsText.text = "Collect weapons from store & submit at GuardVault";
            map1.SetActive(false);
            map2.SetActive(true);
            map3.SetActive(false);
            map4.SetActive(false);
            map5.SetActive(false);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == false && Mission4 == false && Mission5 == false && Mission6 == false)
        {
            missionsText.text = "Find Computer at hacker's house and get data";
            map1.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(true);
            map4.SetActive(false);
            map5.SetActive(false);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == false && Mission5 == false && Mission6 == false)
        {
            missionsText.text = "Find the prison and unlock your teammate";
            map1.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(false);
            map4.SetActive(true);
            map5.SetActive(false);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == true && Mission5 == false && Mission6 == false)
        {
            missionsText.text = "Kill the Supreme commander with Sniper";
            map1.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(false);
            map4.SetActive(false);
            map5.SetActive(true);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == true && Mission5 == true && Mission6 == false)
        {
            missionsText.text = "Locate coordinates and collect the codes";
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == true && Mission5 == true && Mission6 == true)
        {
            missionsText.text = "Congratulations! you have cleared all the missions";
        }
    }
}
