using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class StaticObject : MonoBehaviour, IHitBox
{
    [SerializeField] private LevelObjectData objectData;
    private int health = 1;
    Rigidbody2D rigidbody;

    private void Start()
    {
        health = objectData.Health;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.bodyType = objectData.isStatic ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
    }

#if UNITY_EDITOR
    [ContextMenu("Rename this")]
    private void Rename()
    {
        if (objectData != null)
        {
            gameObject.name = objectData.Name;
        }
    }

    [ContextMenu("MoveRight")]
    private void MoveRight()
    {
        Move(Vector2.right);
    }

    [ContextMenu("MoveLeft")]
    private void MoveLeft()
    {
        Move(Vector2.left);
    }

    [ContextMenu("MoveUp")]
    private void MoveUp()
    {
        Instantiate(this);
        Move(Vector2.up);
    }

    private void Move(Vector2 direction)
    {
        var collider = GetComponent<Collider2D>();
        var size = collider.bounds.size.x;

        transform.Translate(direction * size);
    }

#endif

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
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        Health -= damage;
    }
}
