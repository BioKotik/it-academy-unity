using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    public bool IsFrizing;
    public abstract void Move(Vector2 direction);
    public abstract void Stop(float time);
    public abstract void Jump(float force);
}
