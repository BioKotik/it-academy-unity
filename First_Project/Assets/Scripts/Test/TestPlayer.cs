using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayer : MonoBehaviour
{
    private int scores = 0;
    [SerializeField] private int m_Health = 3;
    [SerializeField] private AnimationCurve m_JumpCurve;
    [SerializeField] private float m_JumpDistance = 2f;
    [SerializeField] private TestUi m_Ui;

    [SerializeField] private float m_BallSpeed = 1f;
    [SerializeField] private TestInput m_Input;
    [SerializeField] private TestTrack m_Track;

    private float iteration;
    private float startZ;

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        //смещение
        pos.x = Mathf.Lerp(pos.x, m_Input.Strafe, Time.deltaTime * 5f);
        //прыжок
        pos.y = m_JumpCurve.Evaluate(iteration * 1f);
        //движение вперед
        pos.z = startZ + iteration * m_JumpDistance;

        transform.position = pos;
        //увеличиваем счетчик прыжка
        iteration += Time.deltaTime * m_BallSpeed;

        if(iteration < 1f)
        {
            return;
        }

        iteration = 0f;
        startZ += m_JumpDistance;

        if (m_Track.IsBallOnPlatform(transform.position))
        {
            scores++;
            m_Ui.UpdateScore(scores);
        }

        if (!m_Track.IsBallOnPlatform(transform.position))
        {
            m_Health--;
            m_Ui.UpdateHealth(m_Health);
        }

        if(scores > 9)
        {
            Time.timeScale = 0f;
            Debug.Log("You win!!!");
        }

        if(m_Health < 1)
        {
            Time.timeScale = 0f;
            m_Ui.LoseGame();
        }
        Debug.Log(m_Health);

        
    }
}
