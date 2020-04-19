using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopTrack : MonoBehaviour
{
    [SerializeField] private HopPlatform m_Platform;
    private List<GameObject> platforms = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {


        platforms.Add(m_Platform.gameObject);
        for (int i = 0; i < 25; i++)
        {
            var obj = Instantiate(m_Platform.gameObject, transform);
            var pos = Vector3.zero;
            pos.z = 2 * (i + 1);
            pos.x = Random.Range(-1, 2);
            obj.transform.position = pos;

            obj.name = $"Platform {i}";

            platforms.Add(obj);
        }
    }

    public bool IsBallOnPlatform(Vector3 position)
    {
        position.y = 0f;
        
        var nearestPlatform = platforms[0];

        for (int i = 0; i < platforms.Count; i++)
        {
            if(platforms[i].transform.position.z + 0.5f < position.z)
            {
                continue;
            }
            if(platforms[i].transform.position.z - position.z > 1f)
            {
                continue;
            }

            nearestPlatform = platforms[i];
            break;
        }
        
        float minX = nearestPlatform.transform.position.x - 0.5f;
        float maxX = nearestPlatform.transform.position.x + 0.5f;

        bool isDone = position.x > minX && position.x < maxX;

        if(isDone)
        {
            HopPlatform platform = nearestPlatform.GetComponent<HopPlatform>();
            platform.SetUpDone();
        }

        return isDone;
    }

}
