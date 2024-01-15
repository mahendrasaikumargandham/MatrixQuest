using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission2Quest : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;

    public GameObject Question1Panel;
    public ShootingController shootingController1;
    public ShootingController shootingController2;
    public ShootingController shootingController3;
    public ShootingController shootingController4;
    public Missions missions;

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;

    public GameObject vehicle;

    public GameObject missionPassed;
    public GameObject UI;
    public GameObject map2;
    public GameObject map3;
    void Start()
    {
        if (inputField != null)
        {
            inputField.Select();
            inputField.ActivateInputField();
        }
    }
    public void ValidateInput()
    {
        string input = inputField.text;

        if (input == "507")
        {
            if (missions.Mission1 == true && missions.Mission2 == false && missions.Mission3 == false && missions.Mission4 == false && missions.Mission5 == false && missions.Mission6 == false)
            {
                missions.Mission2 = true;
                vehicle.SetActive(true);
                weapon1.SetActive(true);
                weapon2.SetActive(true);
                weapon3.SetActive(true);
                weapon4.SetActive(true);
                weapon5.SetActive(true);
                weapon6.SetActive(true);
                StartCoroutine(StartAnimation());
            }

            Question1Panel.SetActive(false);
            input = "";
            Cursor.lockState = CursorLockMode.Locked;
            shootingController1.GetComponent<ShootingController>().enabled = true;
            shootingController2.GetComponent<ShootingController>().enabled = true;
            shootingController3.GetComponent<ShootingController>().enabled = true;
            shootingController4.GetComponent<ShootingController>().enabled = true;
            Time.timeScale = 1f;
            UI.SetActive(true);
            Debug.Log("Question 2");
        }
        else
        {
            resultText.text = "Incorrect Answer, Try Again.";
            input = "";
        }
    }

    IEnumerator StartAnimation()
    {
        missionPassed.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        missionPassed.SetActive(false);
    }
}
