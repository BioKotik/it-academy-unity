using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private float anglePerSecond = 120f;

    private float angle;

    // Update is called once per frame
    void Update()
    {
        transform.position = rotationPoint.position;
        transform.Rotate(Vector3.up, anglePerSecond * Time.deltaTime);
    }
}
