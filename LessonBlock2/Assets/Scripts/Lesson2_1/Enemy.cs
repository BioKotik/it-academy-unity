using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy, IHitBox
{
    [SerializeField]
    private int health = 1;
    [SerializeField] private Animator animator;

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
        animator.SetTrigger("Die");
        Destroy(gameObject, 0.5f);
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
