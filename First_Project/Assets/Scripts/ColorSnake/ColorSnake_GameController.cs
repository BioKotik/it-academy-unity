using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSnake_GameController : MonoBehaviour
{
    public class CameraBounds
    {
        public float Left;//левая граница экрана от позиции камеры
        public float Right;//правая граница экрана от позиции камеры
        public float Up;//верхняя граница экрана от позиции камеры
        public float Down;//нижняя границия экрана от позиции камеры
    }

    public bool isNeedSpawn;

    [SerializeField] private Camera m_MainCamera;
    public Camera MainCamera { get { return m_MainCamera; } }
    [SerializeField] private ColorSnake_Snake m_Snake;

    [SerializeField] private ColorSnake_Types m_Types;
    public ColorSnake_Types Types { get { return m_Types; } }

    private CameraBounds bounds;
    public CameraBounds Bounds
    {
        get { return bounds; }
        private set { bounds = value; }
    }
    private void Awake()
    {
        Vector2 minScreen = m_MainCamera.ScreenToWorldPoint(Vector3.zero);

        bounds = new CameraBounds
        {
            Left = minScreen.x,
            Right = Mathf.Abs(minScreen.x),
            Up = Mathf.Abs(minScreen.y),
            Down = minScreen.y
        };
    }

    private void Update()
    {
        m_MainCamera.transform.Translate(Vector3.up * 0.03f);
        m_Snake.transform.Translate(Vector3.up * 0.03f);
    }
}

