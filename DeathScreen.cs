using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Image targetImage;
    public Text targetText;

    public float duration = 2f;
    public bool showDeadScreen = false;
    private float targetAlpha = 1f;
    private float startAlpha;
    private float elapsedTime = 0f;

    void Start() {
        startAlpha = targetImage.color.a;
    }

    void Update() {
        if(showDeadScreen) {
            if(elapsedTime < duration) {
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime/duration);
                Color newColor = targetImage.color;
                newColor.a = newAlpha;
                targetImage.color = newColor;

                Color newTextAlpha = targetText.color;
                newTextAlpha.a = newAlpha;
                targetText.color = newTextAlpha;

                elapsedTime += Time.deltaTime;
            }
            else {
                Time.timeScale = 0f;
            }
        }
    }
}
