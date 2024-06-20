using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3Controller : MonoBehaviour
{
    public GameObject projectilePrefab; // bullet prefab
    public Transform projectileSpawnPoint; 
    public float projectileSpeed = 10f; 
    public float fireRate = 3.5f; 

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

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            projectile.GetComponent<Rigidbody2D>().velocity = shootingDirection * projectileSpeed;

            Destroy(projectile, 5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D enemyCol)
    {
        //When enemy hits the player, reduce health
        if (enemyCol.gameObject.CompareTag("EnemyTouch"))
        {
            Destroy(this.gameObject);
        }
    }
}
