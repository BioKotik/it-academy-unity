using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNode : Node
{
    public override NodeState Evaluate()
    {
        var player = FindObjectOfType<PlayerController>();

        return player == null ? NodeState.Success : NodeState.Failure;
    }
}
