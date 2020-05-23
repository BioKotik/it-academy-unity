using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private bool moved;
    [SerializeField] private float distance = 6f;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool isUp = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = transform.position;
        if(!isUp)
            targetPosition.x += distance;
        else
            targetPosition.y += distance;

        if (moved)
        {
            StartCoroutine(MovementProcess());
        }
    }

    private IEnumerator MovementProcess()
    {
        var k = 0f;
        var dir = 1f;

        while (true)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, k);
            k += Time.deltaTime * dir * speed;

            if(k > 1f)
            {
                dir = -1;
                k = 1;
                yield return new WaitForSeconds(1f);
            }

            if (k < 0f)
            {
                dir = 1;
                k = 0;
                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isMovementObject = collision.transform.GetComponent<CharacterMovement>();
        if (isMovementObject)
        {
            collision.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.parent == transform)
        {
            collision.transform.parent = null;
        }
    }
}
