using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public float objectHealth = 100f;
    public ParticleSystem blastEffect;

    public void ObjectHitDamage(float amount)
    {
        objectHealth -= amount;
        if (objectHealth <= 0f)
        {
            blastEffect.Play();
            StartCoroutine(DelayInBlast());
        }
    }

    IEnumerator DelayInBlast()
    {
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
}
