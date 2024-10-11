using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] public GameObject[] enemyPrefab;
    [SerializeField] public bool canSpawn = true;
    [SerializeField] public float spawnRate;

    private Coroutine spawnStop;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;
            int rand = Random.Range(0, enemyPrefab.Length);
            GameObject enemytoSpawn = enemyPrefab[rand];

            Instantiate(enemytoSpawn, transform.position, Quaternion.identity);
        }
    }
}

