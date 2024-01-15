using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerQuality : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = -1;
    }
}
