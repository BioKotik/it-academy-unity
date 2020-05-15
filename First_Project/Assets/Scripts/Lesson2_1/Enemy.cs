using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy, IHitBox
{
    [SerializeField]
    private int health = 1;
    public int Health
    {
        get => health;
        private set
        {
            health = value;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        print("Enemy is died!");
    }

    public void Hit(int damage)
    {
        Health -= damage;
    }
    public void RegisterEnemy()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        manager.Enemies.Add(this);

    }

    private void Awake()
    {
        RegisterEnemy();
    }
}
