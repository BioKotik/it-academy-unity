using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float HorizontalAxis;

    private void Start()
    {
        HorizontalAxis = 0;
    }

    private void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");
    }
}
