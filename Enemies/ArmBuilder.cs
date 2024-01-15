using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBuilder : MonoBehaviour
{
    [Header("ArmBuilder health and damage")]
    public float armBuilderHealth = 120f;
    private float presentHealth;
    public int giveDamage = 5;
    // public Healthbar healthBar;

    public PlayerMovement player;

    [Header("ArmBuilder things")]
    public UnityEngine.AI.NavMeshAgent armBuilderAgent;
    public Transform lookPoint;
    public Camera shootingRayCastArea;
    public Transform playerBody;
    public LayerMask playerLayer;

    public Missions missions;

    [Header("ArmBuilder guarding variables")]
    public GameObject[] walkPoints;
    int currentArmBuilderPosition = 0;
    public float armBuilderSpeed;
    float walkingPointRadius = 2f;


    // [Header("Sounds and UI")]

    [Header("ArmBuilder Shooting variables")]
    public float timeBtwShoot;
    bool previouslyShoot;
    public AudioSource audioSource;
    public AudioClip rifleSound;

    [Header("ArmBuilder Animation and spark effect")]
    public Animator animator;
    public ParticleSystem muzzleSpark;

    [Header("ArmBuilder Mood")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerInVisionRadius;
    public bool playerInShootingRadius;

    private void Awake()
    {
        presentHealth = armBuilderHealth;
        // healthBar.GiveFullHealth(armBuilderHealth);
        playerBody = GameObject.Find("Player").transform;
        armBuilderAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInShootingRadius = Physics.CheckSphere(transform.position, shootingRadius, playerLayer);

        if (!playerInShootingRadius && !playerInVisionRadius)
        {
            Guard();
        }

        if (playerInVisionRadius && playerInShootingRadius)
        {
            ShootPlayer();
        }
    }

    private void Guard()
    {
        if (Vector3.Distance(walkPoints[currentArmBuilderPosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentArmBuilderPosition = Random.Range(0, walkPoints.Length);
            if (currentArmBuilderPosition > walkPoints.Length)
            {
                currentArmBuilderPosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentArmBuilderPosition].transform.position, Time.deltaTime * armBuilderSpeed);
        transform.LookAt(walkPoints[currentArmBuilderPosition].transform.position);
    }

    private void ShootPlayer()
    {
        transform.LookAt(lookPoint);
        armBuilderSpeed = 0f;
        armBuilderAgent.speed = 0f;
        if (!previouslyShoot)
        {
            muzzleSpark.Play();
            // audioSource.PlayOneShot(rifleSound);
            RaycastHit hit;
            if (Physics.Raycast(shootingRayCastArea.transform.position, shootingRayCastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("Shooting ... " + hit.transform.name);
                PlayerMovement player = hit.transform.GetComponent<PlayerMovement>();

                if (player != null)
                {
                    player.TakeDamage(giveDamage);
                }
                animator.SetBool("Shoot", true);
                animator.SetBool("Die", false);
                animator.SetBool("Walk", false);
            }
            previouslyShoot = true;
            Invoke(nameof(ActiveShooting), timeBtwShoot);
        }
    }

    private void ActiveShooting()
    {
        previouslyShoot = false;
    }

    public void EnemyHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        // healthBar.SetHealth(presentHealth);
        shootingRadius = 25f;
        visionRadius = 25f;
        ShootPlayer();

        if (presentHealth <= 0)
        {
            animator.SetBool("Shoot", false);
            animator.SetBool("Die", true);
            animator.SetBool("Walk", false);
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        armBuilderSpeed = 0f;
        shootingRadius = 0f;
        visionRadius = 0f;
        playerInShootingRadius = false;
        playerInVisionRadius = false;
        Object.Destroy(gameObject, 20f);
    }
}
