using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text healthTxt;
    [SerializeField] public int playerHealth;
    [SerializeField] private int maxHealth = 100;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private SoundManager soundManager;

    private float damageInterval = 1f;
    private float lastDamageTime;
    private int enemyTouchDamage = 5;
    private bool enemyIsTouching = false;
    private Rigidbody2D rb;
    private bool isDeathSoundPlayed = false;

    private Coroutine damageCoroutine;

    public GameObject losePanel;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Start()
    {
        playerHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue;

        soundManager = FindObjectOfType<SoundManager>();

        UpdateStatistics();

        losePanel.SetActive(false);
    }

    private void Update()
    {
        AdjustPlayerFacingDirection();

        if (playerHealth <= 0 && !isDeathSoundPlayed)
        {
            animator.SetBool("IsAlive", false);
            playerHealth = 0;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Dead"))
            {
                soundManager.PlayPlayerLooseSound();
                losePanel.SetActive(true);
                Time.timeScale = 0f;
                isDeathSoundPlayed = true;
            }
        }

        if (enemyIsTouching == false && damageCoroutine != null)
            StopCoroutine(damageCoroutine);
    }

    void FixedUpdate()
    {
        bool isRunning = rb.velocity.magnitude > 0.1f;

        animator.SetBool("IsRunning", isRunning);
    }

    private void OnTriggerEnter2D(Collider2D playerCol)
    {
        if (playerCol != null && playerCol.gameObject.CompareTag("EnemyTouch"))
        {
            enemyIsTouching = true;
            damageCoroutine = StartCoroutine(PeriodicDamageCoroutine());
        }
        else if (!playerCol.gameObject.CompareTag("EnemyTouch"))
        {
            enemyIsTouching = false;
        }

                if (playerCol.gameObject.CompareTag("Enemy 2 Bullet"))
        {
            soundManager.PlayerHitSound();
            playerHealth -= 10;
            UpdateStatistics();
            animator.SetBool("Hit", true);
        }
    }

    private void OnTriggerExit2D(Collider2D playerCol)
    {
        if (playerCol != null && playerCol.gameObject.CompareTag("EnemyTouch"))
        {
            enemyIsTouching = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
    }

    IEnumerator PeriodicDamageCoroutine()
    {
        while (enemyIsTouching)
        {
            soundManager.PlayerHitSound();
            playerHealth -= enemyTouchDamage;
            UpdateStatistics();
            animator.SetBool("Hit", true);
            lastDamageTime = Time.time;
            yield return new WaitForSeconds(damageInterval);
            animator.SetBool("Hit", false);
        }
    }

    //======================Player Parameters=========================

    public void Meat()
    {
        playerHealth += 10;
        if (playerHealth > maxHealth)
            playerHealth = maxHealth;
        UpdateStatistics();
    }

    public void Hotwings()
    {
        playerHealth += 20;
        if (playerHealth > maxHealth)
            playerHealth = maxHealth;
        UpdateStatistics();
    }

    public void Noodles()
    {
        playerHealth += 30;
        if (playerHealth > maxHealth)
            playerHealth = maxHealth;
        UpdateStatistics();
    }

    public void Pizza()
    {
        playerHealth += 40;
        if (playerHealth > maxHealth)
            playerHealth = maxHealth;
        UpdateStatistics();
    }

    //==================================Sliders and UI=========================================
    private void UpdateStatistics()
    {
        healthTxt.text = $"{playerHealth}/{maxHealth}";
        if (healthSlider != null)
        {
            healthSlider.value = playerHealth;
        }
    }

    //==================================Movement and animations============================================
    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

    private Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = GetMousePosition();
        Vector3 playerPosition = GetPlayerScreenPosition();

        if (mousePos.x < playerPosition.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}