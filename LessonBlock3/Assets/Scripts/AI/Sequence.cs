using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    [SerializeField] protected Node[] nodes;
    public override NodeState Evaluate()
    {
        var anyChildRunning = false;

        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Success:
                    continue;

                case NodeState.Failure:
                    NodeState = NodeState.Failure;
                    return NodeState;

                case NodeState.Running:
                    anyChildRunning = true;
                    continue;
                    
            }
        }

        NodeState = anyChildRunning ? NodeState.Running : NodeState.Success;
        return NodeState;
    }

}
