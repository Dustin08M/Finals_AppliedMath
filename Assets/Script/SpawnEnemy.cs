using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyprefab;
    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnEnemy",0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawnEnemy()
    {
        Transform spawnlocation = Random.Range(0,2) == 0 ? spawn1:spawn2;
        Instantiate(enemyprefab, transform.position, Quaternion.identity);
    }
}
