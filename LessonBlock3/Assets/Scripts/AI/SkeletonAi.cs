using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAi : MonoBehaviour
{
    [SerializeField] private Node rootNode;

    private void Update()
    {
        rootNode.Evaluate();
    }
}
