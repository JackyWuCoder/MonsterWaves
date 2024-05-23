using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    [SerializeField] private int initialMonstersPerWave = 5;
    private int currentMonstersPerWave;

    [SerializeField] private float spawnDelay = 0.5f; // Delay between spawning each zombie in a wave

    [SerializeField] private int currentWave = 0;

    [SerializeField] private float waveCooldown = 10.0f; // Time in seconds between waves

    private bool inCooldown;
    private float cooldownCounter = 0; // Used for testing and UI

    private List<Enemy> currentMonstersAlive = new List<Enemy>();

    [SerializeField] private GameObject zombiePrefab;

    [SerializeField] private GameObject cyberMonsterPrefab;

    private void Start()
    {
        currentMonstersPerWave = initialMonstersPerWave;
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
            cooldownCounter += Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }
    }

    private void StartNextWave()
    {
        currentMonstersAlive.Clear();
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentMonstersPerWave; i++)
        {
            // Generate a random offset within a specified range
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Instantiate a Monster
            int randomNumber = Random.Range(0, 2);
            GameObject monster;
            if (randomNumber == 0)
            {
                monster = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                monster = Instantiate(cyberMonsterPrefab, spawnPosition, Quaternion.identity);
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
        cooldownCounter = 0; // Reset the counter when cooldown starts
        yield return new WaitForSeconds(waveCooldown);
        inCooldown = false;
        currentMonstersPerWave *= 2; // Double the number of monsters for the next wave
        StartNextWave();
    }
}
