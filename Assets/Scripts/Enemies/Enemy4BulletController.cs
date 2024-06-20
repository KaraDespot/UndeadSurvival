using System.Collections;
using UnityEngine;

public class Enemy4BulletController : MonoBehaviour
{
    public float bulletSpeed = 5f; 
    public float rotationSpeed = 200f; 
    public float destroyDelay = 5f; 

    private int bulletsToSpawn = 8; 
    private float angleStep; 
    private float currentAngle; 

    void Start()
    {
        angleStep = 360f / bulletsToSpawn; 

        StartCoroutine(DestroyBulletAfterDelay());
    }

    void Update()
    {
        currentAngle += angleStep * Time.deltaTime;
        currentAngle %= 360f; 

        Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * Vector3.right;
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    IEnumerator DestroyBulletAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
