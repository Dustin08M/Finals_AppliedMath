using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    public bool beingPlaced = false;

    public Transform target;
    public float range = 14f;
    public float fireRate = 1f;
    private float fireTime = 0f;

    public string EnemyTag = "Enemy";

    public float TurnSpeed = 10f;
    public Transform Cannon;
    public Transform Base;

    public GameObject bulletPrefab;
    public Transform firepoint;

    void OnEnable()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && closestDistance <= range)
        {
            target = closestEnemy.transform;
        }
        else { 
            target = null; 
        }
    }

    private void Update()
    {
        if (!beingPlaced)
        {
            if (target == null)
                return;

            Vector3 direction = target.position - transform.position;
            float LookDirection = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion LookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(Cannon.rotation, LookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
            Cannon.rotation = Quaternion.Lerp(Cannon.rotation, LookRotation, Time.deltaTime * TurnSpeed);

            float moveRotation = Mathf.Lerp(transform.eulerAngles.y, LookDirection, Time.deltaTime * TurnSpeed);
            Base.transform.eulerAngles = new Vector3(0f, LookDirection, 0f);

            if (fireTime <= 0f)
            {
                Shoot();
                fireTime = 1f / fireRate;
            }

            fireTime -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject shotBullet = (GameObject)Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        BulletBehaviour bullet = shotBullet.GetComponent<BulletBehaviour>();

        if (bullet != null)
            bullet.chase(target);
    }


    void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    
}
