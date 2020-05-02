using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitInTheHole_Wall
{
    private List<Transform> m_Cubes;
    private Transform m_Parent;
    public Transform Parent => m_Parent;

    public FitInTheHole_Wall(int sizeX, int sizeY, GameObject prefab)
    {
        GenerateWall(sizeX, sizeY, prefab);
    }

    public void SetupWall(FitInTheHole_Template template, float position)
    {
        m_Parent.transform.position = new Vector3(0f, 0.5f, position);
        foreach (var cube in m_Cubes)
        {
            cube.gameObject.SetActive(true);
        }
        var figure = template.GetFigure();

        for (int f = 0; f < figure.Length; f++)
        {
            for (int c = 0; c < m_Cubes.Count; c++)
            {
                if(figure[f] == null || m_Cubes[c] == null)
                {
                    continue;
                }

                if(!Mathf.Approximately(figure[f].position.x, m_Cubes[c].position.x))
                {
                    continue;
                }

                if (!Mathf.Approximately(figure[f].position.y, m_Cubes[c].position.y))
                {
                    continue;
                }

                m_Cubes[c].gameObject.SetActive(false);
            }
        }
    }

    private void GenerateWall(int sizeX, int sizeY, GameObject prefab)
    {
        m_Cubes = new List<Transform>();
        m_Parent = new GameObject("Wall").transform;

        for (int x = -sizeX + 1; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                var obj = Object.Instantiate(prefab, new Vector3(x, y, 0f), Quaternion.identity);
                obj.transform.parent = m_Parent;
                m_Cubes.Add(obj.transform);
            }
        }

        m_Parent.position = new Vector3(0f, 0.5f, 0f);
    }
}
