using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle things")]
    public new Camera camera;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 15f;

    public Animator animator;

    public PlayerScript player;

    [Header("Rifle Ammo and Mag")]
    private float nextTimeToShoot = 0f;
    private int maximumAmmunition = 40;
    private int presentAmmunition;
    private int mag = 15;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;


    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject impactEffect;
    public GameObject bloodEffect;
    public GameObject metalEffect;

    public AudioClip shootingSound;
    public AudioClip reloadAnimated;
    public AudioClip reloadingSound;

    public AudioSource audioSource;

    private void Awake()
    {
        presentAmmunition = maximumAmmunition;
    }
    // Update is called once per frame
    void Update()
    {
        if (setReloading)
            return;

        if (presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if ((Input.GetButton("Fire1") && Input.GetKey(KeyCode.W)) || (Input.GetButton("Fire1") && Input.GetKey(KeyCode.UpArrow)))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else if ((Input.GetButton("Fire1") && Input.GetButton("Fire2") && Input.GetKey(KeyCode.W)) || (Input.GetButton("Fire1") && Input.GetButton("Fire2") && Input.GetKey(KeyCode.UpArrow)))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", false);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
            animator.SetBool("Reloading", false);
        }
    }

    void Shoot()
    {

        if (mag == 0)
        {
            // out of ammo
            return;
        }

        presentAmmunition--;

        if (presentAmmunition == 0)
        {
            mag--;
        }

        AmmunitionCount.occurence.UpdateAmmoText(presentAmmunition);
        AmmunitionCount.occurence.UpdateMagText(mag);
        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);

        RaycastHit hitInfo;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, shootingRange))
        {
            // Vector3 directionToShootingPoint = hitInfo.transform.position - player.transform.position;
            // player.transform.rotation = Quaternion.LookRotation(directionToShootingPoint);
            Debug.Log(hitInfo.transform.name);

            Objects objects = hitInfo.transform.GetComponent<Objects>();
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            Guards guard = hitInfo.transform.GetComponent<Guards>();
            ArmsBuilder armsBuilder = hitInfo.transform.GetComponent<ArmsBuilder>();
            EnemyDrone enemyDrone = hitInfo.transform.GetComponent<EnemyDrone>();

            if (objects != null)
            {
                objects.ObjectHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 1f);
            }
            else if (enemy != null)
            {
                enemy.EnemyHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 2f);
            }
            else if (guard != null)
            {
                guard.EnemyHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 2f);
            }
            else if (armsBuilder != null)
            {
                armsBuilder.ArmsBuilderHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 2f);
            }
            else if (enemyDrone != null)
            {
                enemyDrone.EnemyHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(metalEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 5f);
            }
        }

    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading");
        audioSource.PlayOneShot(reloadingSound);
        animator.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadAnimated);
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
        presentAmmunition = maximumAmmunition;
        player.playerSpeed = 1.9f;
        player.playerSprint = 4f;
        setReloading = false;
    }
}
