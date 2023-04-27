using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    [SerializeField] private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
            Debug.Log($"Health Remaining:{currentHealth}");
            other.gameObject.SetActive(false);
        }
    }
    private void TakeDamage()
    {
        int damage = 20;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
