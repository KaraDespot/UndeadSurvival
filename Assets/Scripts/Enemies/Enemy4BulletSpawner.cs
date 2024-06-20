using System.Collections;
using UnityEngine;

public class Enemy4BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public int bulletCount = 8; 
    public float bulletSpeed = 5f;
    public float spawnRadius = 1f; 
    public float spawnInterval = 5f; 

    void Start()
    {
        StartCoroutine(SpawnBulletsWithInterval());
    }

    IEnumerator SpawnBulletsWithInterval()
    {
        while (true)
        {
            SpawnBullets();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBullets()
    {
        float angleStep = 360f / bulletCount; 

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep; 
            Vector3 direction = Quaternion.Euler(0f, 0f, angle) * Vector3.right; 

            Vector3 spawnPosition = transform.position + direction * spawnRadius; 
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity); 
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); 

            rb.velocity = direction.normalized * bulletSpeed;
            Destroy(bullet, 5f); 
        }
    }
}



