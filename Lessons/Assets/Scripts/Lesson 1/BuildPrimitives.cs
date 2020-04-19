using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPrimitives : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        cube.transform.position = new Vector3(0f,0.5f,0);
        
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(1f,4f,1f);

        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        transform.localEulerAngles = new Vector3(90f,0,0);
    }

}
