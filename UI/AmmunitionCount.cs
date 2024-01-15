using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmunitionCount : MonoBehaviour
{
    public Text ammunitionText;
    public Text magText;

    public static AmmunitionCount occurence;

    private void Awake() {
        occurence = this;
    }

    public void UpdateAmmoText(int presentAmmunition) {
        ammunitionText.text = "" + presentAmmunition;
    }

    public void UpdateMagText(int presentMag) {
        magText.text = "" + presentMag;
    }
}
