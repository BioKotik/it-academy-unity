using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTracking : MonoBehaviour
{
    public enum Target
    {
        Player,
        Ball,

    }

    [SerializeField] private Target target;
    [SerializeField] private Vector3 angleFix = new Vector3(180f, 0f, 90f);
    Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        if (target == Target.Player)
        {
            targetTransform = FindObjectOfType<PlayerController>().transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(targetTransform.position - transform.position);
        transform.Rotate(angleFix);
    }
}
