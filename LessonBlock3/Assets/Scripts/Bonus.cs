using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Bonus : MonoBehaviour
{
    protected void Reset()
    {
        var rig = GetComponent<Rigidbody>();
        rig.isKinematic = true;

        var collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = 0.56f;
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.Translate(Vector3.back * (Time.deltaTime * 2f), Space.World);
        transform.Rotate(0f, 5f, 0f);
    }

    protected void OnMouseDown()
    {
        SetBonus();
    }

    protected virtual void SetBonus()
    {
        StartCoroutine(MoveUp());
        var collider = GetComponent<SphereCollider>();
        if(collider != null)
        {
            collider.enabled = false;
        }
    }

    protected IEnumerator MoveUp()
    {
        var height = 0f;
        while (height < 10f)
        {
            height += Time.deltaTime * 10f;
            var pos = transform.position;
            pos.y = height;
            transform.position = pos;
            yield return null;
        }

        Destroy(gameObject);
    }
}
