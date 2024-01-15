using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public float objectHealth = 100f;

    public void ObjectHitDamage(float amount)
    {
        objectHealth -= amount;
        if (objectHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
