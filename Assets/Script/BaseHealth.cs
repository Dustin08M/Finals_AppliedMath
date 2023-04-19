using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int baseHp;

    private void Start()
    {
        baseHp = health;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
            Debug.Log($"Health Remaining:{baseHp}");
            Destroy(other.gameObject);
        }
;
    }
    private void TakeDamage()
    {
        int damage = 1;
        health -= damage;
        baseHp = health;
    }
}
