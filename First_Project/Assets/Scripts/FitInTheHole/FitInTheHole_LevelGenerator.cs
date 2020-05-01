using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitInTheHole_LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject m_CubePrefab;
    [SerializeField] private float m_BaseSpeed = 2f;
    [SerializeField] private float m_WallDistance = 35f;

    private float speed;
    private FitInTheHole_Wall wall;

    [SerializeField] private FitInTheHole_Template[] m_Templates;
    [SerializeField] private Transform m_FigurePoint;

    private FitInTheHole_Template[] templates;
    private FitInTheHole_Template figure;

    private void Start()
    {
        templates = new FitInTheHole_Template[m_Templates.Length];

        for (int i = 0; i < m_Templates.Length; i++)
        {
            templates[i] = Instantiate(m_Templates[i]);
            templates[i].gameObject.SetActive(false);
            templates[i].transform.position = m_FigurePoint.position;
        }

        wall = new FitInTheHole_Wall(5, 5, m_CubePrefab);
        SetUpTemplate();
        wall.SetUpWall(figure, m_WallDistance);
        speed = m_BaseSpeed;
    }

    private void SetUpTemplate()
    {
        if(figure != null)
        {
            figure.gameObject.SetActive(false);
        }

        var rand = Random.Range(0, templates.Length);
        figure = templates[rand];
        figure.gameObject.SetActive(true);
        figure.SetUpRandomFigure();
    }

    private void Update()
    {
        wall.Parent.transform.Translate(Vector3.back * (Time.deltaTime * speed));

        if(wall.Parent.transform.position.z > m_WallDistance * -1)
        {
            return;
        }

        wall.SetUpWall(figure, m_WallDistance);

    }
}
