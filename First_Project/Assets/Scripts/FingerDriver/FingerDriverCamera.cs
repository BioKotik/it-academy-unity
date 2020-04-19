using UnityEngine;


public class FingerDriverCamera : MonoBehaviour
{
    [SerializeField] private Transform m_CarTransform;

    private float camZ;

    private void Start()
    {
        camZ = transform.position.z;
    }

    private void Update()
    {
        Vector3 pos = m_CarTransform.position;
        pos.z = camZ;
        transform.position = pos;
    }
}
