using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutOfField : Node
{   
    public override NodeState Evaluate()
    {
        return transform.position.z > 3.5f ? NodeState.Running : NodeState.Failure;
    }
}
