using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayRun : MonoBehaviour
{
    public static void Execute(Action callback, float timer, GameObject targetObject)
    {
        var runComponent = targetObject.AddComponent<DelayRun>();
        runComponent.StartExecute(callback, timer);
    }

    private void StartExecute(Action callback, float timer)
    {
        StartCoroutine(WaitAndExecute(callback, timer));
    }

    private IEnumerator WaitAndExecute(Action callback, float timer)
    {
        yield return new WaitForSeconds(timer);
        callback?.Invoke();
        Destroy(this);
    }
}
