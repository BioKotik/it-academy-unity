using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private GameObject[] healthObjects;

    public event Action OnDieAction;
    void Start()
    {
        SetupHealthObjects();
    }

    public virtual void SetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            return;
        }

        SetupHealthObjects();
    }

    protected void SetupHealthObjects()
    {
        var nm = Mathf.Clamp(health - 1, 0, healthObjects.Length);
        for (int i = 0; i < healthObjects.Length; i++)
        {
            healthObjects[i].SetActive(i == nm);
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
        OnDieAction?.Invoke();
    }
}
