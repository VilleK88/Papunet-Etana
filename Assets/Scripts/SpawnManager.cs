using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField] Transform spawnPoint;
    float startDelay = 1;
    float spawnInterval = 3;

    private void Start()
    {
        InvokeRepeating("SpawnRocksAndStrawberries", startDelay, spawnInterval);
    }

    void SpawnRocksAndStrawberries()
    {
        int index = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[index], spawnPoint.position, prefabs[index].transform.rotation);
    }
}