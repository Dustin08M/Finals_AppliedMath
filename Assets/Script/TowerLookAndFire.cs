using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLookAndFire : MonoBehaviour
{
    [SerializeField] Transform enemy;
    [SerializeField] private float TowerRange;
    [SerializeField] private int FireRate;
    [SerializeField] private int towerDamage = 35;
    private float lastattack = 0f;
    private float TowerLookRotateSpeed = 25f;
    private int towerRange = 15;

    // Update is called once per frame
    void Update()
    {
        /*Collider[] collider = Physics.OverlapSphere(transform.position, TowerRange);
        foreach (Collider enemies in collider)
        {
            if (enemies.gameObject.CompareTag("Enemy"))
            {
                Vector3 EnemyTarget = enemies.transform.position;
                EnemyTarget.y = transform.position.y;
                transform.LookAt(EnemyTarget);
                if (Time.time > lastattack + FireRate)
                {
                    Fire(enemies.gameObject);
                    lastattack = Time.time;
                }
            }
        }*/
        Vector3 direction = enemy.transform.position - transform.position;
        float Distance = Vector3.Distance(enemy.transform.position, transform.position);
        if (Distance <= TowerRange)
        {
            Debug.Log("DETECTED");
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            float RotateHead = TowerLookRotateSpeed * Time.deltaTime;
            float rotation = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, RotateHead);
            transform.eulerAngles = new Vector3(0f, rotation, 0f);
        }
    }
    private void Fire(GameObject enemyUnit)
    {
        Debug.Log("Firing");
        Enemy pawn = enemyUnit.GetComponent<Enemy>();
        pawn.TakeDamage(towerDamage);
    }
}
