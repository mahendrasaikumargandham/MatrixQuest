using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessMan : MonoBehaviour
{
    [Header("BusinessMan health and damage")]
    public float businessManHealth = 120f;
    private float presentHealth;

    public Missions missions;

    public Animator animator;

    private void Awake()
    {
        presentHealth = businessManHealth;
    }

    public void BusinessManHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;

        if (presentHealth <= 0)
        {
            animator.SetBool("Die", true);
            if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission4 == false && missions.Mission5 == false && missions.Mission6 == false)
            {
                missions.Mission3 = true;
            }
            BusinessManDie();
        }
    }

    private void BusinessManDie()
    {
        Object.Destroy(gameObject, 10f);
    }
}
