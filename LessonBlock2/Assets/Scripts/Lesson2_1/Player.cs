using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer, IHitBox
{
    [SerializeField]
    private PlayerWeapon[] weapons;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private Camera camera;
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

    private void Start()
    {
        weapons = GetComponents<PlayerWeapon>();
        InputManager.FireAction += OnAttack;
    }

    private void OnDestroy()
    {
        InputManager.FireAction -= OnAttack;
    }

    private void Awake()
    {
        RegisterPlayer();
    }

    private void OnAttack(string axis)
    {
        foreach (var weapon in weapons)
        {
            //if (weapon.Axis == "Fire3")
            //{
            //    StartCoroutine(RangeAttack(weapon));
            //    break;
            //}

            if (weapon.Axis == axis)
            {
                weapon.SetDamage();
                animator.SetTrigger("Attack");
            }
        }
    }

    //private IEnumerator RangeAttack(PlayerWeapon weapon)
    //{
    //    weaponAnimator.SetTrigger("Fly");

    //    yield return new WaitForSeconds(.5f);

    //    weapon.SetDamage();
    //}
}
