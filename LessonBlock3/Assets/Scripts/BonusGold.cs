using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGold : Bonus
{
    [SerializeField] private int gold = 1;

    protected override void SetBonus()
    {
        base.SetBonus();
        Debug.Log($"Added {gold} gold");
        GameController.Gold += gold;
    }
}
