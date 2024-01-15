using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Health")]
    public int maxHealth = 100;
    public int currentHealth = 100;
    public DeathScreenUI deathScreenUI;
    public PlayerHealthBar healthBar;

    [Header("Player movement & gravity")]
    public float movementSpeed = 5f;
    private CharacterController controller;

    public float jumpForce = 2f;

    public float gravity = -9.81f;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float groundDistance = 0.4f;
    private bool isGrounded;

    private Vector3 velocity;

    [Header("Foot steps")]
    public AudioSource leftFootAudioSource;
    public AudioSource rightFootAudioSource;

    public AudioClip[] footStepSound;
    public float footStepInterval = 0.5f;
    private float nextFootStepTime;
    private bool isLeftFootStep = true;

    public Player player;
    public GameObject MissionFailed;
    public AudioSource audioSource;
    public AudioClip playerHurt;
    void Start()
    {
        Application.targetFrameRate = 60;
        controller = GetComponent<CharacterController>();
        healthBar.GiveFullHealth(currentHealth);
    }

    void Awake()
    {
        if (MainMenu.instance.ContinueGamePlay == true)
        {
            player.LoadPlayer();
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        HandleMovement();
        HandleGravity();
        //
        if (isGrounded && controller.velocity.magnitude > 0.1f && Time.time >= nextFootStepTime)
        {
            PlayerFootStepSound();
            nextFootStepTime = Time.time + footStepInterval;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * horizontalInput + transform.forward * verticalInput;
        movement.y = 0;

        controller.Move(movement * movementSpeed * Time.deltaTime);
    }

    void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    void PlayerFootStepSound()
    {
        AudioClip footStepClip = footStepSound[Random.Range(0, footStepSound.Length)];

        if (isLeftFootStep)
        {
            leftFootAudioSource.PlayOneShot(footStepClip);
        }
        else
        {
            rightFootAudioSource.PlayOneShot(footStepClip);
        }

        isLeftFootStep = !isLeftFootStep;
    }

    public void TakeDamage(int damageAmount)
    {
        audioSource.PlayOneShot(playerHurt);
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth == 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(PlayAnimation());
        Debug.Log("Player has died..");

    }

    IEnumerator PlayAnimation()
    {
        MissionFailed.SetActive(true);
        yield return new WaitForSeconds(4f);
        currentHealth = 100;
        healthBar.GiveFullHealth(currentHealth);
        player.LoadPlayer();
        MissionFailed.SetActive(false);
    }
}
