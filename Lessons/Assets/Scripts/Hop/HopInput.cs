using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopInput : MonoBehaviour
{
    private float strafe;
    public float Strafe { get { return strafe; } }

    private float screenCenter;
    // Start is called before the first frame update
    void Start()
    {
        screenCenter = Screen.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        float mousePos = Input.mousePosition.x;
        if(mousePos > screenCenter)
        {
            strafe = (mousePos - screenCenter) / screenCenter;
        }
        else
        {
            strafe = 1 - mousePos / screenCenter;
            strafe *= -1f;
        }
    }
}
