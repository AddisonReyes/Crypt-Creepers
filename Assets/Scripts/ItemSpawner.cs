using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkPointPrefab;
    [SerializeField] int SpawnDelay = 10;
    [SerializeField] float spawnRadius = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCheckPointRutine());
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
}
