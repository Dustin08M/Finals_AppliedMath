using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject[] SpawnSpots;
    [SerializeField] private GameObject Enemyprefab;
    [SerializeField] private GameObject Portal;
    public float SpawnTime = 1.3f;
    private Queue<GameObject> enemyPool;

    int RandomIndex;
    public int Poolsize = 15;
    private void Awake()
    {
        enemyPool = new Queue<GameObject>();
    }

    void Start()
    {
        SpawnSpots = GameObject.FindGameObjectsWithTag("SpawnSpot");
        InitializeEnemyPool();
        StartCoroutine(SpawnEnemies());
    }

    void InitializeEnemyPool()
    {
        for (int i = 0; i < Poolsize; i++)
        {
            GameObject enemy = Instantiate(Enemyprefab);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            RandomIndex = Random.Range(0, SpawnSpots.Length - 1);
            Vector3 SpawnPos = SpawnSpots[RandomIndex].transform.position;
            SpawnEnemyMark(SpawnPos);
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    void SpawnEnemyMark(Vector3 spawnPoint)
    {
        GameObject enemyObject = null;
        if (enemyPool.Count < Poolsize)
        {
            enemyObject = Instantiate(Enemyprefab, spawnPoint, Quaternion.identity);
        }
        else
        {
            enemyObject = enemyPool.Dequeue();
            enemyObject.SetActive(false);
            enemyObject.transform.position = spawnPoint;
            enemyObject.SetActive(true);
            enemyObject.GetComponent<NavMeshAgent>().ResetPath();
            enemyObject.GetComponent<NavMeshAgent>().SetDestination(Portal.transform.position);
        }
    
        
        enemyPool.Enqueue(enemyObject);
    }
}
