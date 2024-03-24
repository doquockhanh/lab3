using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float updateFireRate = 0f;
    private float fireTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / (fireRate + updateFireRate))
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            fireTimer = 0f;
        }
    }
}
