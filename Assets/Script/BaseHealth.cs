using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public GameObject EndScreen;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private string EnemyTag = "Enemy";
    [SerializeField] private float EnemyDistance = 1;
    [SerializeField] private HealthBar healthBar;
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        foreach (GameObject enemy in Enemies)
        {
            Vector3 distance = enemy.transform.position - transform.position;
            float direction = distance.magnitude;
            if (direction < EnemyDistance)
            {
                enemy.SetActive(false);
                TakeDamage();
                Debug.Log($"Health Remaining:{currentHealth}");
            }

        }
    }
    



   /* private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
            Debug.Log($"Health Remaining:{currentHealth}");
            other.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("U/nknown");
        }
    }*/
    private void TakeDamage()
    {
        
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                EndScreen.SetActive(true);
            }
            healthBar.SetHealth(currentHealth);
        }
        else if (currentHealth <= 0)
        {
            EndScreen.SetActive(true);
        }
        
    }
}
