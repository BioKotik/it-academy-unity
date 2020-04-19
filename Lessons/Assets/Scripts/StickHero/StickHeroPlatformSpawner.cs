using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickHeroPlatformSpawner : MonoBehaviour
{
    [SerializeField] private StickHeroPlatform platform;

    public StickHeroPlatform SpawnPlatform(Vector3 spawnPosition)
    {
        StickHeroPlatform obj = platform;
        obj.transform.localScale = new Vector3(Random.Range(0.2f, 1.5f), 1f, 1f);
        return Instantiate(obj, spawnPosition, Quaternion.identity);        
    }
}
