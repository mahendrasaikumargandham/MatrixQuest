using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guards : MonoBehaviour
{
    [Header("Guard health and damage")]
    public float GuardHealth = 120f;
    private float presentHealth;
    public int giveDamage = 5;
    // public Healthbar healthBar;

    public PlayerMovement player;

    [Header("Guard things")]
    public UnityEngine.AI.NavMeshAgent GuardAgent;
    public Transform lookPoint;
    public Camera shootingRayCastArea;
    public Transform playerBody;
    public LayerMask playerLayer;

    [Header("Guard guarding variables")]
    public GameObject[] walkPoints;
    int currentGuardPosition = 0;
    public float GuardSpeed;
    float walkingPointRadius = 2f;


    // [Header("Sounds and UI")]

    [Header("Guard Shooting variables")]
    public float timeBtwShoot;
    bool previouslyShoot;
    public AudioSource audioSource;
    public AudioClip rifleSound;

    [Header("Guard Animation and spark effect")]
    public Animator animator;
    public ParticleSystem muzzleSpark;

    [Header("Guard Mood")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerInVisionRadius;
    public bool playerInShootingRadius;

    private void Awake()
    {
        presentHealth = GuardHealth;
        // healthBar.GiveFullHealth(armBuilderHealth);
        playerBody = GameObject.Find("Player").transform;
        GuardAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
        if (Vector3.Distance(walkPoints[currentGuardPosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentGuardPosition = Random.Range(0, walkPoints.Length);
            if (currentGuardPosition > walkPoints.Length)
            {
                currentGuardPosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentGuardPosition].transform.position, Time.deltaTime * GuardSpeed);
        transform.LookAt(walkPoints[currentGuardPosition].transform.position);
    }

    private void ShootPlayer()
    {
        transform.LookAt(lookPoint);
        GuardSpeed = 0f;
        GuardAgent.speed = 0f;
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
        GuardSpeed = 0f;
        shootingRadius = 0f;
        visionRadius = 0f;
        playerInShootingRadius = false;
        playerInVisionRadius = false;
        Object.Destroy(gameObject, 20f);
    }
}
