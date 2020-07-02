using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float ricochetePower;
    private LineRenderer lineRenderer;
    private Rigidbody rigidbody;
    private TrajectoryCurve trajectoryCurve;
    private float iteration = 0f;
    private float bulletSpeed = 1f;
    private bool isMove;
    private Vector3 previousPos;
    private Vector3 movementDirection;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        trajectoryCurve = Component.FindObjectOfType<TrajectoryCurve>();
        lineRenderer = new LineRenderer(); 
        var positions = new Vector3[trajectoryCurve.lineRenderer.positionCount];
        trajectoryCurve.lineRenderer.GetPositions(positions);
        lineRenderer.SetPositions(positions);
        rigidbody = GetComponent<Rigidbody>();
        isMove = true;
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            movementDirection = transform.position - previousPos;
            previousPos = transform.position;
            Move();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        string compareTag = collision.gameObject.tag;
        print("Collision");

        switch (compareTag)
        {
            case "Enemy":
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);
                break;

            case "Obstacle":
                gameObject.SetActive(false);
                break;

            case "Ricochete":
                Vector3 inNormal = collision.contacts[0].normal;
                //Ricochete(inNormal);
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rigidbody.velocity 
            = movementDirection;
    }

    //private void Ricochete(Vector3 inNormal)
    //{
    //    isMove = false;
    //    //rigidbody.isKinematic = false;
    //    var direction = Vector3.Reflect(currentPos - previousPos, inNormal);
    //    rigidbody.velocity = direction * ricochetePower;
    //}

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            lineRenderer.GetPosition(index), bulletSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, lineRenderer.GetPosition(index)) < 0.1f)
        {
            index++;            
        }

    }
}
