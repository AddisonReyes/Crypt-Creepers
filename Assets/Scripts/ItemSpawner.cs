using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkPointPrefab;
    [SerializeField] GameObject[] powerUpPrefab;
    [SerializeField] int SpawnDelay = 3;
    [SerializeField] int PowerUpSpawnDelay = 3;
    [SerializeField] float spawnRadius = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCheckPointRutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCheckPointRutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;

            Instantiate(checkPointPrefab, randomPosition, Quaternion.identity);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(PowerUpSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;

            int random = Random.Range(0, powerUpPrefab.Length);

            Instantiate(powerUpPrefab[random], randomPosition, Quaternion.identity);
        }
    }
}
