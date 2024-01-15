using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShootingController : MonoBehaviour
{
    public Animator animator;
    public Transform firePoint;
    public float fireRate = 0.1f;
    public float fireRange = 10f;

    public float damagePerShot = 10f;

    private float nextFireTime = 0f;

    public bool isAuto = false;

    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1.5f;
    private bool isReloading = false;
    public Text currentAmmoText;

    [Header("Sound Effects")]
    public AudioSource soundAudioSource;
    public AudioClip shootingSoundClip;
    public AudioClip reloadSoundClip;

    public GameObject bloodEffect;

    public ParticleSystem muzzleFlash;
    public GameObject metalEffect;

    void Start()
    {
        Application.targetFrameRate = 60;
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        currentAmmoText.text = currentAmmo.ToString();
        if (isReloading)
            return;

        if (isAuto == true)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1 / fireRate;
                Shoot();
            }
            else
            {
                animator.SetBool("Shoot", false);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1 / fireRate;
                Shoot();
            }
            else
            {
                animator.SetBool("Shoot", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if (currentAmmo > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, fireRange))
            {
                Debug.Log(hit.transform.name);

                Objects objects = hit.transform.GetComponent<Objects>();
                Doors doors = hit.transform.GetComponent<Doors>();
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                Guards guard = hit.transform.GetComponent<Guards>();
                ArmBuilder ab = hit.transform.GetComponent<ArmBuilder>();
                BusinessMan bm = hit.transform.GetComponent<BusinessMan>();
                EnemyDrone enemyDrone = hit.transform.GetComponent<EnemyDrone>();
                KillOpponent ko = hit.transform.GetComponent<KillOpponent>();

                if (objects != null)
                {
                    objects.ObjectHitDamage(damagePerShot);
                    GameObject impactGo = Instantiate(metalEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 2f);
                }

                else if (doors != null)
                {
                    doors.ObjectHitDamage(damagePerShot);
                    GameObject impactGo = Instantiate(metalEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 2f);
                }

                else if (enemy != null)
                {
                    enemy.EnemyHitDamage(damagePerShot);
                    GameObject impactGo = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 2f);
                }

                else if (ko != null)
                {
                    ko.TakeDamage(damagePerShot);
                    GameObject impactGo = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 2f);
                }

                else if (guard != null)
                {
                    guard.EnemyHitDamage(damagePerShot);
                    GameObject impactGo = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 2f);
                }

                else if (enemyDrone != null)
                {
                    enemyDrone.EnemyHitDamage(damagePerShot);
                    GameObject impactGo = Instantiate(metalEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 2f);
                }

                else if (ab != null)
                {
                    ab.EnemyHitDamage(damagePerShot);
                    GameObject impactGo = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 2f);
                }
                else if (bm != null)
                {
                    bm.BusinessManHitDamage(damagePerShot);
                    GameObject impactGo = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 2f);
                }
            }
            animator.SetBool("Shoot", true);
            muzzleFlash.Play();
            currentAmmo--;
            soundAudioSource.PlayOneShot(shootingSoundClip);
        }
        else
        {
            Reload();
        }
    }

    private void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
        {
            animator.SetTrigger("Reload");
            isReloading = true;
            soundAudioSource.PlayOneShot(reloadSoundClip);
            Invoke("FinishReloading", reloadTime);
        }
    }

    private void FinishReloading()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        animator.ResetTrigger("Reload");
    }
}
