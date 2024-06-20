using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemyData
{
    public EnemyPrefabCount[] enemyPrefabCounts; 
}

[System.Serializable]
public class EnemyPrefabCount
{
    public GameObject enemyPrefab; 
    public int count; 
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemyTypes;
    public float spawnRadius = 10f;
    public float timeBetweenWaves = 10f;

    private Transform player;
    private int currentWaveIndex = -1;
    private int activeEnemiesCount; 
    private int deadEnemiesCount;
    private EnemyDeathCounter deathCounter; 
    public Text waveText;
    public Text enemiesLeft;

    public GameObject winPanel;

    public ActiveWeapon activeWeapon;
    public Text upgradeText;

    private BoundaryController boundaryController;

    private SoundManager soundManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        deathCounter = FindObjectOfType<EnemyDeathCounter>();
        StartCoroutine(SpawnWaves());

        activeWeapon = FindObjectOfType<ActiveWeapon>();

        winPanel.SetActive(false);
        boundaryController = FindObjectOfType<BoundaryController>();

        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        deadEnemiesCount = deathCounter.currentDeathCount;
        UpdateRemainingEnemies();
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            UpdateWaveText();
            UpdateRemainingEnemies();
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWaveIndex++;

            deathCounter.currentDeathCount = 0;

            EnemyData currentWave = enemyTypes[currentWaveIndex];
            activeEnemiesCount = GetTotalEnemyCount(currentWave);

            for (int i = 0; i < currentWave.enemyPrefabCounts.Length; i++)
            {
                yield return StartCoroutine(SpawnEnemiesOfType(currentWave.enemyPrefabCounts[i]));
                yield return new WaitForSeconds(0.5f); 
            }

            //win state
            if (currentWaveIndex == 14 && activeEnemiesCount == 0)
            {
                soundManager.WinSound();
                winPanel.SetActive(true);
                break;
            }

            if ((currentWaveIndex + 1) % 4 == 0 && currentWaveIndex <= enemyTypes.Length - 1)
            {

                activeWeapon.ObtainWeapon();
                upgradeText.text = "New weapon available!";
                StartCoroutine(ClearUpgradeText(3f));
            }

            if (currentWaveIndex != 14)
                soundManager.WaveCompletedSound();

            yield return null;
        }
    }

    IEnumerator SpawnEnemiesOfType(EnemyPrefabCount enemyPrefabCount)
    {
        GameObject enemyPrefab = enemyPrefabCount.enemyPrefab;
        int count = enemyPrefabCount.count;

        for (int i = 0; i < count; i++)
        {
            if (boundaryController.IsWithinBoundary(transform))
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    int GetTotalEnemyCount(EnemyData wave)
    {
        int totalEnemyCount = 0;
        foreach (var enemyPrefabCount in wave.enemyPrefabCounts)
        {
            totalEnemyCount += enemyPrefabCount.count;
        }
        return totalEnemyCount;
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = player.position + new Vector3(randomCircle.x, randomCircle.y, 0f);
        return spawnPosition;
    }
    void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + (currentWaveIndex + 2);
        }
    }
    void UpdateRemainingEnemies()
    {
        int remainingEnemies = activeEnemiesCount - deadEnemiesCount;
        enemiesLeft.text = "Enemies left:\n" + remainingEnemies;
    }

    IEnumerator ClearUpgradeText(float delay)
    {
        yield return new WaitForSeconds(delay);
        upgradeText.text = "";
    }
}







