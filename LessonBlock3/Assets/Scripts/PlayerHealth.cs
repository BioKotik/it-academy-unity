using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private static PlayerHealth instance;
    public static PlayerHealth Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            var obj = new GameObject("Player Health");
            instance = obj.AddComponent<PlayerHealth>();
            return instance;
        }
    }

    private GameObject playerHealth;

    public GameObject GetObject(GameObject prefab)
    {
        if (playerHealth == null)
        {
            playerHealth = prefab;
            return playerHealth;
        }
       
        return playerHealth;
    }

}
