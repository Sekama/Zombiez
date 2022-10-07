using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireGun : MonoBehaviour
{
    [Header("Gun Parameters")]
    [SerializeField] private float nextTimeToFire;
    public float fireRate;
    public float range;
    public float damage;
    
    [Header("Reload Parameters")]
    public int maxAmmo;
    [SerializeField] private int currentAmmo;
    public float reloadTime;
    public bool isReloading;

    [Header("Object Parameters")] 
    public GameObject player;
    public GameObject weaponHolder;
    public TrailRenderer bulletTrail;
    public ParticleSystem muzzleFlash;

    private Vector3 destination;
    private float trailSpeed = 250f;

    private UIManager uiManager;
    
    private void Start()
    {
        currentAmmo = maxAmmo;

        uiManager = GameObject.Find("PlayerUI").GetComponent<UIManager>();

         uiManager.UpdateAmmo(currentAmmo);
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            muzzleFlash.Play();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        while (currentAmmo < maxAmmo)
        {
            currentAmmo++;
            yield return new WaitForSeconds(reloadTime);
        }

        uiManager.UpdateAmmo(currentAmmo);
        isReloading = false;
    }
    void Shoot()
    {
        currentAmmo  --;
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit;
        destination = ray.GetPoint(range);
        uiManager.UpdateAmmo(currentAmmo);

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
            Debug.DrawLine(player.transform.position, destination, Color.green, 1f);
        }
        else
        {
            destination = ray.GetPoint(range);
            Debug.DrawLine(player.transform.position, ray.GetPoint(range), Color.green, 1f);
        }

        InstantiateProjectile(weaponHolder.transform);

        if (Physics.Raycast(ray, out hit))
        {
            ZombieHealth zombieHealth = hit.transform.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(damage);
            }
        }
    }
    
    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate (bulletTrail, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * trailSpeed;
    }
}
