using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform Base;
    [SerializeField] private int MaxHP = 100;
    private int _currentHp;

    void Start()
    {
        _currentHp = MaxHP;
        var unit = GetComponent<NavMeshAgent>();
        unit.SetDestination(Base.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        MaxHP -= damage;
        _currentHp = MaxHP;
        if (_currentHp <= 0)
        {
            Debug.Log($"Damage took per shot: {damage}");
            Destroy(gameObject);
        }
    }
}
