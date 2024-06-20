using UnityEngine;
using UnityEngine.UI;

public class Enemy2Controller : MonoBehaviour
{
    private Animator anim;
    public Transform target; // Target player
    public float moveSpeed = 1f; // Enemy's speed
    public int enemyHealth = 30;
    public int maxHealth = 30;
    public GameObject enemyHealthBar; // Prefab for enemy health bar
    private Slider _slider;
    public GameObject[] dropItems; // Prefabs for drops

    public GameObject enemy2Bullet; // Reference to the empty object for bullet instantiation
    private float stoppingDistance = 5f; // Distance at which the enemy stops
    private float bulletSpawnInterval = 4f; // Interval between bullet spawns
    private float lastBulletSpawnTime; // Time of the last bullet spawn

    private EnemyDeathCounter deathCounter;
    private bool isDead = false;
    private SoundManager soundManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _slider = GetComponentInChildren<Slider>();
        _slider.gameObject.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        deathCounter = GameObject.FindObjectOfType<EnemyDeathCounter>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the player
            Vector3 direction = target.position - transform.position;
            direction.Normalize(); // Normalize to get a unit direction vector

            // Check the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            if (distanceToPlayer > stoppingDistance)
            {
                // Move towards the player if the distance is greater than the stopping distance
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Stop moving if the distance is less than or equal to the stopping distance
                anim.SetBool("IsWalking", false);
            }

            if (Time.time - lastBulletSpawnTime >= bulletSpawnInterval)
            {
                if (isDead == false)
				{
                    SpawnBullet();
                    lastBulletSpawnTime = Time.time;
				}
                
            }
        }

        if (enemyHealth <= 0 && !isDead)
        {
            soundManager.PlayEnemyDeathSound();
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        anim.SetBool("Dead", true);
        gameObject.tag = "DeadEnemy";
        enemyHealth = 0;
        moveSpeed = 0;
        Destroy(enemyHealthBar); // Destroy the health bar when the enemy dies
        this.gameObject.GetComponent<EnemyHealthBar>().enabled = false;
        Destroy(gameObject, 3f);
        deathCounter.IncreaseDeathCount();
    }

    void SpawnBullet()
    {
        soundManager.EnemyRangeSound();
            // Используем позицию и направление BulletInstance для спавна пули
            Vector3 bulletSpawnPosition = transform.Find("BulletInstance").position;
            Quaternion bulletSpawnRotation = Quaternion.identity;

            // Создаем пулю
            GameObject bullet = Instantiate(enemy2Bullet, bulletSpawnPosition, bulletSpawnRotation);
    }

    private void OnTriggerEnter2D(Collider2D enemyCol)
    {
        // When enemy hits the player, reduce health
        if (enemyCol.gameObject.CompareTag("Weapon 0"))
        {
            HandleHit(5, 1f);
        }
        else if (enemyCol.gameObject.CompareTag("Weapon 1"))
        {
            HandleHit(8, 2f);
        }
        else if (enemyCol.gameObject.CompareTag("Bullet 2"))
        {
            HandleHit(15, 5f);
        }
        else if (enemyCol.gameObject.CompareTag("Bullet 3"))
        {
            HandleHit(5, 0.1f);
        }
    }

    private void HandleHit(int damage, float knockbackForce = 5f)
    {
        soundManager.EnemyHitSound();
        if (enemyHealth > 0)
        {
            anim.SetTrigger("Hit");
            enemyHealth -= damage;
            SetHealthValue(enemyHealth, maxHealth);

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 forceDirection = target.position - transform.position;
            forceDirection.Normalize();
            rb.AddForce(-forceDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    public void SetHealthValue(int enemyHealth, int maxHealth)
    {
        _slider.gameObject.SetActive(enemyHealth < maxHealth);
        _slider.value = enemyHealth;
        _slider.maxValue = maxHealth;
    }

    private void OnDestroy()
    {
        // Drop loot when the enemy is destroyed
        DropLoot();
    }

    private void DropLoot()
    {
        {
            float randomChance = Random.Range(0f, 100f);

            if (randomChance <= 5f)
            {
                Instantiate(dropItems[0], transform.position, Quaternion.identity);
            }
            else if (randomChance <= 8f)
            {
                Instantiate(dropItems[1], transform.position, Quaternion.identity);
            }
            else if (randomChance <= 9.5f)
            {
                Instantiate(dropItems[2], transform.position, Quaternion.identity);
            }
            else if (randomChance <= 10f)
            {
                Instantiate(dropItems[3], transform.position, Quaternion.identity);
            }
        }
    }
}
