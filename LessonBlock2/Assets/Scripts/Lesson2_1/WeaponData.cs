using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Objects/Weapon object", order = 0)]
public class WeaponData : ScriptableObject
{
    public string weaponName = "WeaponName";
    public int damage = 1;
    public float range = 1;
    public float fireRate = 1f;
    public GameObject bullet;
    public float bulletSpeed;
}
