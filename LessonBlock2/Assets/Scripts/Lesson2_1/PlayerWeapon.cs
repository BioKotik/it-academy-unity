using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IDamage
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private string axis = "Fire1";

    public int Damage => weaponData.damage;
    public string Axis => axis;

    public void SetDamage()
    {
        var target = GetTarget();
        target?.Hit(Damage);
    }

    private IHitBox GetTarget()
    {
        IHitBox target = null;

        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.right, weaponData.range);

        if(hit.collider != null)
        {
            target = hit.transform.root.GetComponent<IHitBox>();
        }

        return target;
    }
}
