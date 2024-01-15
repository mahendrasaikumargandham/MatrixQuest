using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy health and damage")]
    public float enemyHealth = 120f;
    private float presentHealth;
    public int giveDamage = 5;
    // public Healthbar healthBar;

    public PlayerMovement player;

    [Header("Enemy things")]
    public NavMeshAgent enemyAgent;
    public Transform lookPoint;
    public Camera shootingRayCastArea;
    public Transform playerBody;
    public LayerMask playerLayer;

    [Header("Enemy guarding variables")]
    public GameObject[] walkPoints;
    int currentEnemyPosition = 0;
    public float enemySpeed;
    float walkingPointRadius = 2f;


    // [Header("Sounds and UI")]

    [Header("Enemy Shooting variables")]
    public float timeBtwShoot;
    bool previouslyShoot;
    public AudioSource audioSource;
    public AudioClip rifleSound;

    [Header("Enemy Animation and spark effect")]
    public Animator animator;
    public ParticleSystem muzzleSpark;

    [Header("Enemy Mood")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerInVisionRadius;
    public bool playerInShootingRadius;

    private void Awake()
    {
        presentHealth = enemyHealth;
        // healthBar.GiveFullHealth(enemyHealth);
        playerBody = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
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
        if (Vector3.Distance(walkPoints[currentEnemyPosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentEnemyPosition = Random.Range(0, walkPoints.Length);
            if (currentEnemyPosition > walkPoints.Length)
            {
                currentEnemyPosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentEnemyPosition].transform.position, Time.deltaTime * enemySpeed);
        transform.LookAt(walkPoints[currentEnemyPosition].transform.position);
    }

    // private void PurchuePlayer() {
    //     if(enemyAgent.SetDestination(playerBody.position)) {
    //         visionRadius = 80f;
    //         shootingRadius = 40f;
    //     }
    // }

    private void ShootPlayer()
    {
        // enemyAgent.SetDestination(playerBody.position);
        transform.LookAt(lookPoint);
        enemySpeed = 0f;
        enemyAgent.speed = 0f;
        if (!previouslyShoot)
        {
            // muzzleSpark.Play();
            audioSource.PlayOneShot(rifleSound);
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
                animator.SetBool("AimDie", false);
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
            animator.SetBool("AimDie", true);
            animator.SetBool("Walk", false);
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        enemySpeed = 0f;
        shootingRadius = 0f;
        visionRadius = 0f;
        playerInShootingRadius = false;
        playerInVisionRadius = false;
        Object.Destroy(gameObject, 1f);
    }
}
