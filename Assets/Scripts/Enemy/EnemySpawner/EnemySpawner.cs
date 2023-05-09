using UnityEngine;
using UnityEngine.UI;

namespace Enemy.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject portalPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnInterval = 20f;
        [SerializeField] private int maxEnemiesToSpawn = 3;
        [SerializeField] private float portalSpawnDelay = 3f;
        [SerializeField] private Text countdownText;
        [SerializeField] private Text waveNumberText;
        [SerializeField] private AudioSource portalSound;

        private float timeUntilSpawn;
        private int spawnPointIndex;
        private int enemiesToSpawn;
        private bool isSpawningEnemies;
        private bool isPortalSpawned;
        private int waveNumber;

        private void Start()
        {
            timeUntilSpawn = spawnInterval;
            countdownText.text = "";
            waveNumber = 0;
            waveNumberText.text = "Wave Number: " + waveNumber;
        }

        private void Update()
        {
            timeUntilSpawn -= Time.deltaTime;

            if (timeUntilSpawn <= portalSpawnDelay && !isPortalSpawned)
            {
                SpawnPortal();
                isPortalSpawned = true;
            }

            if (timeUntilSpawn <= 0)
            {
                StartSpawningEnemies();
            }

            if (isSpawningEnemies)
            {
                SpawnEnemies();
            }

            UpdateCountdownText();
        }

        private void SpawnPortal()
        {
            spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(portalPrefab, spawnPoints[spawnPointIndex].position + Vector3.up * 1.5f, spawnPoints[spawnPointIndex].rotation);
            portalSound.time = 1.5f;
            portalSound.Play();
        }

        private void StartSpawningEnemies()
        {
            enemiesToSpawn = Random.Range(maxEnemiesToSpawn - 1, maxEnemiesToSpawn + 1);
            isSpawningEnemies = true;
            timeUntilSpawn = spawnInterval;
            isPortalSpawned = false;
            waveNumber++;
            waveNumberText.text = "Wave Number: " + waveNumber;
        }

        private void SpawnEnemies()
        {
            if (enemiesToSpawn > 0)
            {
                Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                enemiesToSpawn--;
            }
            else
            {
                isSpawningEnemies = false;
            }
        }

        private void UpdateCountdownText()
        {
            countdownText.text = "Next wave in: " + Mathf.CeilToInt(timeUntilSpawn).ToString();
        }
    }
}
