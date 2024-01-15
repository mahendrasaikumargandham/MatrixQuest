using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArmsBuilder : MonoBehaviour
{
    [Header("ArmsBuilder health and damage")]
    public float ArmsBuilderHealth = 120f;
    private float presentHealth;
    public float giveDamage = 5f;
    public Healthbar healthBar;

    public PlayerScript player;

    [Header("ArmsBuilder things")]
    public NavMeshAgent ArmsBuilderAgent;
    public Transform lookPoint;
    public Camera shootingRayCastArea;
    public Transform playerBody;
    public LayerMask playerLayer;

    [Header("ArmsBuilder guarding variables")]
    public GameObject[] walkPoints;
    int currentArmsBuilderPosition = 0;
    public float ArmsBuilderSpeed;
    float walkingPointRadius = 2f;


    // [Header("Sounds and UI")]

    [Header("ArmsBuilder Shooting variables")]
    public float timeBtwShoot;
    bool previouslyShoot;
    public AudioSource audioSource;
    public AudioClip rifleSound;

    [Header("ArmsBuilder Animation and spark effect")]
    public Animator animator;

    [Header("ArmsBuilder Mood")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerInVisionRadius;
    public bool playerInShootingRadius;

    private void Awake()
    {
        presentHealth = ArmsBuilderHealth;
        healthBar.GiveFullHealth(ArmsBuilderHealth);
        playerBody = GameObject.Find("Player").transform;
        ArmsBuilderAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInShootingRadius = Physics.CheckSphere(transform.position, shootingRadius, playerLayer);

        if (!playerInShootingRadius && !playerInVisionRadius)
        {
            Guard();
        }

        // if(playerInVisionRadius && !playerInShootingRadius) {
        //     PurchuePlayer();
        // }

        if (playerInVisionRadius && playerInShootingRadius)
        {
            ShootPlayer();
        }
    }

    private void Guard()
    {
        if (Vector3.Distance(walkPoints[currentArmsBuilderPosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentArmsBuilderPosition = Random.Range(0, walkPoints.Length);
            if (currentArmsBuilderPosition > walkPoints.Length)
            {
                currentArmsBuilderPosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentArmsBuilderPosition].transform.position, Time.deltaTime * ArmsBuilderSpeed);
        transform.LookAt(walkPoints[currentArmsBuilderPosition].transform.position);
    }

    // private void PurchuePlayer() {
    //     if(ArmsBuilderAgent.SetDestination(playerBody.position)) {
    //         visionRadius = 80f;
    //         shootingRadius = 40f;
    //     }
    // }

    private void ShootPlayer()
    {
        // ArmsBuilderAgent.SetDestination(playerBody.position);
        transform.LookAt(lookPoint);

        if (!previouslyShoot)
        {
            audioSource.PlayOneShot(rifleSound);
            RaycastHit hit;
            if (Physics.Raycast(shootingRayCastArea.transform.position, shootingRayCastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("Shooting ... " + hit.transform.name);
                PlayerScript player = hit.transform.GetComponent<PlayerScript>();

                if (player != null)
                {
                    player.PlayerHitDamage(giveDamage);
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

    public void ArmsBuilderHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);
        shootingRadius = 25f;
        visionRadius = 25f;
        ShootPlayer();

        if (presentHealth <= 0)
        {
            animator.SetBool("Shoot", false);
            animator.SetBool("Die", true);
            animator.SetBool("Walk", false);
            ArmsBuilderDie();
        }
    }

    private void ArmsBuilderDie()
    {
        ArmsBuilderSpeed = 0f;
        shootingRadius = 0f;
        visionRadius = 0f;
        playerInShootingRadius = false;
        playerInVisionRadius = false;
        Object.Destroy(gameObject, 2f);
    }
}
