using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BonusContainer : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject[] bonus;

    private void Reset()
    {
        health = GetComponent<Health>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (health != null)
        {
            health.OnDieAction += OnDie;
            return;
        }

        Destroy(this);
    }

    private void OnDie()
    {
        if (bonus != null)
        {
            Instantiate(bonus[Random.Range(0, bonus.Length - 1)], transform.position, transform.rotation);
        }
    }

}
