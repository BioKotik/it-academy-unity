using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorSnake_Snake : MonoBehaviour
{
    [SerializeField] private ColorSnake_GameController m_GameController;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Text m_ScoreText;
    private Vector3 position;
    private int currentType;
    [NonSerialized] public int score = 0;

    private void Start()
    {
        position = transform.position;

        var colorType = m_GameController.Types.GetRandomColorType();
        currentType = colorType.Id;
        m_SpriteRenderer.color = colorType.Color;
    }

    private void SetupColor(int id)
    {
        var colorType = m_GameController.Types.GetColorType(id);
        currentType = colorType.Id;
        m_SpriteRenderer.color = colorType.Color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obstacle = collision.gameObject.GetComponent<ColorSnake_Obstacle>();
        if (!obstacle)
            return;
        if (obstacle.ColorId != currentType)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            score++;
            m_ScoreText.text = $"Score = {score}";
            Destroy(obstacle.gameObject);
        }
    }

    void Update()
    {
        position = transform.position;
        if (!Input.GetMouseButton(0))
            return;

        position.x = m_GameController.MainCamera.ScreenToWorldPoint(Input.mousePosition).x;

        position.x = Mathf.Clamp(position.x, m_GameController.Bounds.Left, m_GameController.Bounds.Right);
        transform.position = position;
    }
}
