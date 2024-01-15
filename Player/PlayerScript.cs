using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 2f;
    public float playerSprint = 3f;

    [Header("Player Animator and Gravity")]
    public CharacterController cc;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player script cameras")]
    public Transform playerCamera;

    [Header("Player jumping and velocity")]
    Vector3 velocity;
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    public Transform surfaceCheck;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    bool onSurface;
    public float jumpRange = 1f;

    [Header("Player Health")]
    public float playerHealth = 120f;
    private float presentHealth;
    public Healthbar healthBar;
    public DeathScreen deathScreen;
    public GameObject bloodScreen;


    void Start()
    {
        Application.targetFrameRate = -1;
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;
        healthBar.GiveFullHealth(playerHealth);
    }

    void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        PlayerMove();
        PlayerSprint();
        Jump();
    }

    void PlayerMove()
    {
        float vertical_axis = Input.GetAxisRaw("Vertical");
        float horizontal_axis = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            animator.SetBool("AimWalk", false);
            animator.SetBool("IdleAim", false);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cc.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetTrigger("Jump");
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            animator.SetBool("AimWalk", false);
        }
    }

    void PlayerSprint()
    {
        if ((Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) && onSurface) || (Input.GetButton("Sprint") && Input.GetKey(KeyCode.UpArrow) && onSurface))
        {
            float vertical_axis = Input.GetAxisRaw("Vertical");
            float horizontal_axis = Input.GetAxisRaw("Horizontal");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Running", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                animator.SetBool("IdleAim", false);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cc.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Walk", false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.ResetTrigger("Jump");
        }
    }

    public void PlayerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);
        StartCoroutine(ShowBloodScreen());
        if (presentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator ShowBloodScreen()
    {
        bloodScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        bloodScreen.SetActive(false);
    }

    private void Die()
    {
        deathScreen.showDeadScreen = true;
        animator.SetBool("Die", true);
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 5f);
    }
}
