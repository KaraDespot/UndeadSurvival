using System.Collections;
using UnityEngine;

public class Enemy2BulletController : MonoBehaviour
{
    public float bulletSpeed = 5f; 
    public float rotationSpeed = 200f; 
    public float destroyDelay = 5f; 

    private Transform player;
    private Vector3 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 

        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
        }

        StartCoroutine(DestroyBulletAfterDelay());
    }

    void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    IEnumerator DestroyBulletAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
