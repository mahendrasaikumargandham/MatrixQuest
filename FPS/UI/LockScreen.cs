using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LockScreen : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;
    public GameObject QuestionPanel;

    public void ValidateInput()
    {
        string input = inputField.text;

        if (input == "TEAM-DEV")
        {
            QuestionPanel.SetActive(false);
        }
        else
        {
            resultText.text = "Incorrect Answer, Try Again.";
            input = "";
        }
    }
}
