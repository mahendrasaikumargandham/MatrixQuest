using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthBarSlider;

    public void GiveFullHealth(int health)
    {
        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;
    }
    public void SetHealth(int health)
    {
        healthBarSlider.value = health;
    }
}
