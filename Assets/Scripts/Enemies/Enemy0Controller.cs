using UnityEngine;
using UnityEngine.UI;

public class Enemy0Controller : MonoBehaviour
{
    private Animator anim;
    public Transform target; // Target player
    public float moveSpeed = 1f; // Enemy's speed
    public int enemyHealth = 30;
    public int maxHealth = 30;
    public GameObject enemyHealthBar; // Prefab for enemy health bar
    private Slider _slider;
    public GameObject[] dropItems; // Prefabs for drops
    private EnemyDeathCounter deathCounter;
    private bool isDead = false;

    private SoundManager soundManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _slider = this.gameObject.GetComponentInChildren<Slider>();
        _slider.gameObject.SetActive(false);
        target = GameObject.FindWithTag("Player").transform;
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

            //If the distance is greater than a certain threshold, move towards the player
            if (distanceToPlayer > 0.3f)
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
            if (enemyHealth > 0 && enemyHealth < maxHealth)
            {
                SetHealthValue(enemyHealth, maxHealth);
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
            //SetHealthValue(enemyHealth, maxHealth);

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 forceDirection = target.position - transform.position;
            forceDirection.Normalize();
            rb.AddForce(-forceDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    public void SetHealthValue(int enemyHealth, int maxHealth)
    {
        if (enemyHealth < maxHealth)
            _slider.gameObject.SetActive(true);
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
            // Generate random number for the loot drop
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
