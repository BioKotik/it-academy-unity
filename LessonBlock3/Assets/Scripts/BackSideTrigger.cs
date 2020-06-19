using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSideTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var ball = other.gameObject.GetComponent<Ball>();
        if(ball != null)
        {
            Destroy(ball.gameObject);
        }
    }
}
