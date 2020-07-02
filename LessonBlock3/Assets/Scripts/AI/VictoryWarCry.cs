using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryWarCry : Node
{
    [SerializeField] private Animator animator;

    private enum MyState
    {
        Wait,
        WarCry,
        Complete,

    }

    private MyState myState;
    private Coroutine WarCryCoroutine;

    public override NodeState Evaluate()
    {
        switch (myState)
        {
            case MyState.Wait:
                myState = MyState.WarCry;
                WarCryCoroutine = StartCoroutine(victoryWarCryCoroutine());
                return NodeState.Running;

            case MyState.WarCry:
                if (WarCryCoroutine != null)
                {
                    return NodeState.Running;
                }
                else
                {
                    myState = MyState.Complete;
                }
                return NodeState.Success;

            case MyState.Complete:
                return NodeState.Failure;

            default:
                throw new System.Exception();
        }
    }

    private IEnumerator victoryWarCryCoroutine()
    {
        animator.SetTrigger("VictoryWarCry");
        yield return new WaitForSeconds(1.5f);
        WarCryCoroutine = null;
    }
}
