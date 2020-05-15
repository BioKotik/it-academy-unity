using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestUi : MonoBehaviour
{
    [SerializeField] private Text m_HealthText;
    [SerializeField] private Text m_ScoreText;
    [SerializeField] private UnityEngine.UI.Button m_Button;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        m_Button.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateHealth(int health)
    {
        m_HealthText.text = $"Health = {health}";
    }

    public void UpdateScore(int score)
    {
        m_ScoreText.text = $"CurrentGate = {score}";
    }
}
