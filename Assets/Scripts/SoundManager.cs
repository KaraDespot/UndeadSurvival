using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip enemyDeathSound;
    public AudioClip playerLooseSound;
    public AudioClip playerHitSound;
    public AudioClip enemyHitSound;
    public AudioClip winSound;
    public AudioClip waveCompletedSound;
    public AudioClip playerMeleeAttackSound;
    public AudioClip playerRangeAttackSound;
    public AudioClip enemyMeleeAttackSound;
    public AudioClip enemyRangeAttackSound;
    public AudioClip buttonSelectSound;


    public AudioSource audioSource;
    public AudioSource musicSource;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(enemyDeathSound);
    }

    public void PlayPlayerLooseSound()
    {
        audioSource.PlayOneShot(playerLooseSound);
    }

    public void PlayerHitSound()
    {
        audioSource.PlayOneShot(playerHitSound);
    }

    public void EnemyHitSound()
    {
        audioSource.PlayOneShot(enemyHitSound);
    }

    public void WinSound()
    {
        audioSource.PlayOneShot(winSound);
    }
    public void WaveCompletedSound()
    {
        audioSource.PlayOneShot(waveCompletedSound);
    }
    public void PlayerMeleeSound()
    {
        audioSource.PlayOneShot(playerMeleeAttackSound);
    }
    public void PlayerRangeSound()
    {
        audioSource.PlayOneShot(playerRangeAttackSound);
    }
    public void EnemyRangeSound()
    {
        audioSource.PlayOneShot(enemyRangeAttackSound);
    }
    public void EnemyMeleeSound()
    {
        audioSource.PlayOneShot(enemyMeleeAttackSound);
    }
    public void ButtonSelectionSound()
    {
        audioSource.PlayOneShot(buttonSelectSound);
    }
}