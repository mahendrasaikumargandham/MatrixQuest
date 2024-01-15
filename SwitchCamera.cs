using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera to assign")]
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject TPCanvas;

    public Animator animator;

    void Update()
    {
        if ((Input.GetButton("Fire2") && Input.GetKey(KeyCode.W)) || (Input.GetButton("Fire2") && Input.GetKey(KeyCode.UpArrow)))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("AimWalk", true);
            animator.SetBool("Walk", true);

            TPCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else if (Input.GetButton("Fire2") || Input.GetButton("Fire1"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("AimWalk", false);
            animator.SetBool("Walk", false);

            TPCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("IdleAim", false);
            animator.SetBool("AimWalk", false);

            TPCanvas.SetActive(true);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }
}
