using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text buttonText;
    private Color originalColor;
    private FontStyle originalFontStyle;
    public Color hoverColor = Color.black;
    public FontStyle hoverFontStyle = FontStyle.Bold;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        originalColor = buttonText.color;
        originalFontStyle = buttonText.fontStyle;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;
        buttonText.fontStyle = hoverFontStyle;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originalColor;
        buttonText.fontStyle = originalFontStyle;
    }
}
