using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOpponent : MonoBehaviour
{
    public Animator animator;
    public Missions missions;
    public GameObject missionPassed;
    public float currentHealth = 10f;


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            animator.SetBool("Dead", true);
            if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission3 == true && missions.Mission4 == true && missions.Mission6 == false)
            {
                missions.Mission5 = true;
                StartCoroutine(StartAnimation());
            }
        }
    }

    IEnumerator StartAnimation()
    {
        missionPassed.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        missionPassed.SetActive(false);
    }
}
