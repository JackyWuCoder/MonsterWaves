using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    [SerializeField] private int initialMonstersPerWave = 5;
    private int currentMonstersPerWave;

    [SerializeField] private float spawnDelay = 0.5f; // Delay between spawning each zombie in a wave

    [SerializeField] private int currentWave = 0;

    [SerializeField] private float waveCooldown = 10.0f; // Time in seconds between waves

    private bool inCooldown;
    private float cooldownCounter; // Used for testing and UI

    private List<Enemy> currentMonstersAlive = new List<Enemy>();

    [SerializeField] private GameObject zombiePrefab;

    [SerializeField] private GameObject cyberMonsterPrefab;

    [SerializeField] private TextMeshProUGUI waveOverUI;
    [SerializeField] private TextMeshProUGUI cooldownCounterUI;
    [SerializeField] private TextMeshProUGUI currentWaveUI;

    [SerializeField] private List<Transform> spawnPoints; // List of spawn points

    private void Start()
    {
        currentMonstersPerWave = initialMonstersPerWave;
        GlobalReferences.Instance.waveNumber = currentWave;
        StartNextWave();
    }

    private void Update()
    {
        // Get all dead monsters
        List<Enemy> monstersToRemove = new List<Enemy>();
        foreach (Enemy monster in currentMonstersAlive)
        {
            if (monster.isDead)
            {
                monstersToRemove.Add(monster);
            }
        }

        // Actually remove all dead monsters
        foreach (Enemy monster in monstersToRemove)
        {
            currentMonstersAlive.Remove(monster);
        }

        // Start Cooldown if all monsters are dead
        if (currentMonstersAlive.Count == 0 && !inCooldown)
        {
            // Start cooldown for next wave
            StartCoroutine(WaveCooldown());
        }

        // Run the cooldown counter
        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
            if (cooldownCounter < 0)
            {
                cooldownCounter = 0;
            }
        }
        else
        {
            cooldownCounter = waveCooldown;
        }
        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    private void StartNextWave()
    {
        currentMonstersAlive.Clear();
        currentWave++;
        GlobalReferences.Instance.waveNumber = currentWave;
        currentWaveUI.text = "Wave: " + currentWave.ToString();
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentMonstersPerWave; i++)
        {
            // Select a random spawn point from the list
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Instantiate a Monster
            int randomNumber = Random.Range(0, 2);
            GameObject monster;
            if (randomNumber == 0)
            {
                monster = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                monster = Instantiate(cyberMonsterPrefab, spawnPoint.position, Quaternion.identity);
            }

            // Get Enemy Script
            Enemy enemyScript = monster.GetComponent<Enemy>();

            // Track this monster
            currentMonstersAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        waveOverUI.gameObject.SetActive(true);
        cooldownCounter = waveCooldown; // Set the counter to the wave cooldown time when cooldown starts

        while (cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;
            cooldownCounterUI.text = cooldownCounter.ToString("F0");
            yield return null;
        }

        inCooldown = false;
        waveOverUI.gameObject.SetActive(false);
        // currentMonstersPerWave = initialMonstersPerWave; // Reset the number of monsters for the next wave
        currentMonstersPerWave = currentMonstersPerWave + 2;
        StartNextWave();
    }
}
