using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerDeath : Node
{
    public override NodeState Evaluate()
    {
        var player = FindObjectOfType<PlayerController>();
        return player == null ? NodeState.Running : NodeState.Failure;
    }
}
