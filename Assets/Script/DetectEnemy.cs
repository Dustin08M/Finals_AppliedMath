using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    [SerializeField] Transform Enemy;
    [SerializeField] Transform TowerHead;
    [SerializeField] private int DetectRange;
    private void Update()
    {
        Vector3 direction = Enemy.transform.position - transform.position.normalized;
        float distance = Vector3.Distance(Enemy.transform.position, transform.position);

        if (distance <= DetectRange)
        {
            Vector3 towerdistance = Enemy.position - TowerHead.position;
            TowerHead.transform.LookAt(Enemy.position);
        }
    }
}
