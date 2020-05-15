using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    [SerializeField] private float m_Distance = 2f;
    [SerializeField] private float m_Height = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = Mathf.Lerp(a: transform.position.z, b: m_Target.position.z - m_Distance, t: Time.deltaTime * 5f);

        var pos = new Vector3(x:0f, y:m_Height, z);

        transform.position = pos;

    }
}
