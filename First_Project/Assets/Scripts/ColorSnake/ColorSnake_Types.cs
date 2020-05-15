using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorSnake_Types : MonoBehaviour
{
    //Цвета объектов
    [Serializable]
    public struct ColorType
    {
        public string Name;
        public int Id;
        public Color Color;
    }

    //Типы объектов
    [Serializable]
    public struct ObjectType
    {
        public string Name;
        public int Id;
        public GameObject Object;
    }

    [Serializable]
    public struct TemplatesTypes
    {
        public string Name;
        public int Id;
        public Transform[] Points;
    }

    [SerializeField] private ColorType[] m_Colors;
    [SerializeField] private ObjectType[] m_Objects;
    [SerializeField] private TemplatesTypes[] m_Templates;

    public ColorType GetRandomColorType()
    {
        int rand = UnityEngine.Random.Range(0, m_Colors.Length);
        return m_Colors[rand];
    }

    public ObjectType GetRandomObjectType()
    {
        int rand = UnityEngine.Random.Range(0, m_Objects.Length-1);
        return m_Objects[rand];
    }

    public void SpawnLine(Vector3 pos)
    {
        var obj = m_Objects[m_Objects.Length-1].Object;
        var colorType = GetRandomColorType();
        obj.GetComponent<SpriteRenderer>().color = colorType.Color;
        var obstacleController = obj.AddComponent<ColorSnake_Obstacle>();
        obstacleController.ColorId = colorType.Id;
        Instantiate(obj, pos, Quaternion.identity);
    }

    public TemplatesTypes GetRandomTemplate()
    {
        int rand = UnityEngine.Random.Range(0, m_Templates.Length);
        return m_Templates[rand];
    }

    public ColorType GetColorType(int Id)
    {
        return m_Colors.FirstOrDefault(c => c.Id == Id);
    }
    public ObjectType GetObjectType(int Id)
    {
        return m_Objects.FirstOrDefault(c => c.Id == Id);
    }
}

