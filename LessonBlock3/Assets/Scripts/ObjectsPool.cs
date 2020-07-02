using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    private static ObjectsPool instance;
    public static ObjectsPool Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            var obj = new GameObject("Object Pool");
            instance = obj.AddComponent<ObjectsPool>();
            return instance;
        }
    }

    private Dictionary<GameObject, List<GameObject>> pool = 
        new Dictionary<GameObject, List<GameObject>>();

    public GameObject GetObject(GameObject prefab)
    {
        if (!pool.ContainsKey(prefab))
        {
            var obj = Instantiate(prefab);
            pool[prefab] = new List<GameObject> {obj};
            return obj;
        }

        var objects = pool[prefab];

        foreach (var object_ in objects)
        {
            if (object_ != null && !object_.activeSelf)
            {
                object_.SetActive(true);
                return object_;
            }

        }

        var newObj = Instantiate(prefab);
        objects.Add(newObj);
        return newObj;
    }

    public void PrepareObject(GameObject prefab, int count)
    {
        if (pool.ContainsKey(prefab))
        {
            if (pool[prefab].Count >= count)
            {
                return;
            }

            var newObjectsCount = count - pool[prefab].Count;
            pool[prefab].AddRange(Instantiate(prefab, newObjectsCount));
            return;
        }

        pool[prefab] = new List<GameObject>();
        pool[prefab].AddRange(Instantiate(prefab, count));
    }

    private List<GameObject> Instantiate(GameObject prefab, int count)
    {
        var objects = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            objects.Add(obj);
        }

        return objects;
    }
}
