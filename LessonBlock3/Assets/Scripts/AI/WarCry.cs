using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarCry : Node
{
    [SerializeField] private Animator animator;

    private enum MyState
    {
        Wait,
        WarCry,
        Complete,

    }

    private MyState state;
    private Coroutine warCryCoroutine;

    public override NodeState Evaluate()
    {
        switch (state)
        {
            case MyState.Wait:
                state = MyState.WarCry;
                warCryCoroutine = StartCoroutine(WaitWarCry());
                return NodeState.Running;

            case MyState.WarCry:
                if (warCryCoroutine != null)
                {
                    return NodeState.Running;
                }
                else
                {
                    state = MyState.Complete;
                }
                return NodeState.Success;

            case MyState.Complete:

                return NodeState.Failure;

            default:
                throw new System.Exception();
        }
    }

    private IEnumerator WaitWarCry()
    {
        animator.SetTrigger("WarCry");
        yield return new WaitForSeconds(1.5f);
        warCryCoroutine = null;
    }
}