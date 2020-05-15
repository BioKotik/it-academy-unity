using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer, IHitBox
{
    [SerializeField]
    private int health = 1;
    public int Health
    {
        get => health;
        private set
        {
            health = value;
            if(health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        print("Player is died!");
    }

    public void Hit(int damage)
    {
        Health -= damage;
    }

    public void RegisterPlayer()
    {
        GameManager manager = FindObjectOfType<GameManager>();

        if(manager.Player == null)
        {
            manager.Player = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        RegisterPlayer();
    }
}
