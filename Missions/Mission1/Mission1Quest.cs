using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mission1Quest : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;

    public GameObject Question1Panel;
    public ShootingController shootingController1;
    public ShootingController shootingController2;
    public ShootingController shootingController3;
    public ShootingController shootingController4;
    public Missions missions;
    public GameObject map1;
    public GameObject map2;
    public GameObject missionPassed;
    public GameObject UI;

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

        if (input == "25 8 8 19 8 26")
        {
            if (missions.Mission2 == false && missions.Mission3 == false && missions.Mission4 == false && missions.Mission5 == false && missions.Mission6 == false)
            {
                missions.Mission1 = true;
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
