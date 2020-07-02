using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMove : Node
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform root;
    [SerializeField] private int speed = 1;
    
    public override NodeState Evaluate()
    {
        animator.SetInteger("Movement", speed);
        root.Translate(Time.deltaTime * speed * Vector3.back, Space.World);
        return NodeState.Success;
    }
}
