using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PlayerFireGun : MonoBehaviour
{
    [SerializeField] private float nextTimeToFire;
    public float fireRate;
    public float range;

    public int maxAmmo;
    [SerializeField] private int currentAmmo;
    public float reloadTime;
    [SerializeField] private bool isReloading;

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
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
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
        if (currentAmmo > 0)
        { 
            currentAmmo  --;
        }
       // RaycastHit hit;
     //   if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, range))
       // {
            
      //  }
    }
}
