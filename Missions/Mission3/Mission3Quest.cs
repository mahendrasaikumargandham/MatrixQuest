using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission3Quest : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;

    public GameObject Question3Panel;
    public ShootingController shootingController1;
    public ShootingController shootingController2;
    public ShootingController shootingController3;
    public ShootingController shootingController4;
    public Missions missions;
    public GameObject missionPassed;
    public GameObject UI;
    public GameObject map3;
    public GameObject map4;

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

        if (input == "27")
        {
            if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission4 == false && missions.Mission5 == false && missions.Mission6 == false)
            {
                missions.Mission3 = true;
                StartCoroutine(StartAnimation());
                // map3.SetActive(false);
                // map4.SetActive(true);
            }

            Question3Panel.SetActive(false);
            input = "";
            Cursor.lockState = CursorLockMode.Locked;
            shootingController1.GetComponent<ShootingController>().enabled = true;
            shootingController2.GetComponent<ShootingController>().enabled = true;
            shootingController3.GetComponent<ShootingController>().enabled = true;
            shootingController4.GetComponent<ShootingController>().enabled = true;
            Time.timeScale = 1f;
            UI.SetActive(true);
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
