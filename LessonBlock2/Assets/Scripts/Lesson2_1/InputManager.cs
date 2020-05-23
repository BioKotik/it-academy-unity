using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float HorizontalAxis;

    public static event Action<float> JumpAction;
    public static event Action<string> FireAction;

    private float jumpTimer;
    private Coroutine waitJumpCoroutine;

    private void Start()
    {
        HorizontalAxis = 0;
    }

    private void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            if(waitJumpCoroutine == null)
            {
                waitJumpCoroutine = StartCoroutine(WaitJump());
                return;
            }

            jumpTimer = Time.time;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            HorizontalAxis += HorizontalAxis > 0 ? 0.5f : -0.5f;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            FireAction?.Invoke("Fire1");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            FireAction?.Invoke("Fire2");
        }

        if(Input.GetButtonDown("Fire3"))
        {
            FireAction?.Invoke("Fire3");
        }
    }

    private IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(0.2f);

        if(JumpAction != null)
        {
            var force = Time.time - jumpTimer <= 0.2f ? 1.25f : 1f; //TODO убрать константы
            
            JumpAction.Invoke(force);
        }

        waitJumpCoroutine = null;
    }
}
