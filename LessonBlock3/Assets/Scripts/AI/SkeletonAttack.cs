using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : Node
{
    [SerializeField] private Animator animator;
    [SerializeField] private int enemyHealth = 3;
    private Coroutine attackCoroutine;
    public override NodeState Evaluate()
    {
        if (attackCoroutine != null)
        {
            return NodeState.Running;
        }

        var player = FindObjectOfType<PlayerController>();
        
        if (player == null)
        {           
            return NodeState.Failure;
        }

        if (Vector3.Distance(transform.position, player.transform.position) > 1f)
        {
            return NodeState.Failure;
        }

        attackCoroutine = StartCoroutine(AttackProcess());

        if (enemyHealth > 0)
        {
            return NodeState.Running;
        }

        if(enemyHealth <= 0)
        {
            player.gameObject.SetActive(false);
        }

        return NodeState.Success;
    }

    private IEnumerator AttackProcess()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        enemyHealth -= 1;
        attackCoroutine = null;
    }
}
