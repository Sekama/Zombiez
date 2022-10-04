using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
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
    [SerializeField] private bool isReloading;
    
    [Header("Object Parameters")]
    public GameObject weaponHolder;
    public TrailRenderer bulletTrail;

    private Vector3 destination;
    private float trailSpeed = 250f;
    
    private void Start()
    {
        currentAmmo = maxAmmo;
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

        isReloading = false;
    }
    void Shoot()
    {
        currentAmmo  --;
        Ray ray = new Ray(weaponHolder.transform.position, weaponHolder.transform.forward);
        RaycastHit hit;
        destination = ray.GetPoint(range);

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
            Debug.DrawLine(weaponHolder.transform.position, destination, Color.green, 1f);
        }
        else
        {
            destination = ray.GetPoint(range);
            Debug.DrawLine(weaponHolder.transform.position, ray.GetPoint(range), Color.green, 1f);
        }

        InstantiateProjectile(weaponHolder.transform);

        if (Physics.Raycast(ray, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
    
    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate (bulletTrail, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * trailSpeed;
    }
}
