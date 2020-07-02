﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    [SerializeField] protected Node[] nodes;
    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Success:
                    NodeState = NodeState.Success;
                    return NodeState;

                case NodeState.Failure:
                    continue;

                case NodeState.Running:
                    NodeState = NodeState.Running;
                    return NodeState;
            }
        }

        NodeState = NodeState.Failure;
        return NodeState;
    }

}
