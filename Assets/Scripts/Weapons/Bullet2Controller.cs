using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2Controller : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform projectileSpawnPoint; 
    public float projectileSpeed = 10f; 
    public float fireRate = 1f;
    private float nextFireTime = 0f; 

    private SoundManager soundManager;

	private void Start()
	{
        soundManager = FindObjectOfType<SoundManager>();
    }

	void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            soundManager.PlayerRangeSound();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 shootingDirection = mousePosition - projectileSpawnPoint.position;
            shootingDirection.z = 0f;

            shootingDirection.Normalize();

            CreateProjectile(shootingDirection);

            float angle = 20f;
            CreateProjectile(Quaternion.Euler(0, 0, angle) * shootingDirection);
            CreateProjectile(Quaternion.Euler(0, 0, -angle) * shootingDirection);
        }
    }

    void CreateProjectile(Vector3 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        Destroy(projectile, 4f);
    }
   }
