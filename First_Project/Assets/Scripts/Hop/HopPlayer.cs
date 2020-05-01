using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HopPlayer : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_JumpCurve;
    [SerializeField] private float m_JumpHeight = 1f;
    [SerializeField] private float m_JumpDistance = 2f;

    [SerializeField] private float m_BallSpeed = 1f;
    [SerializeField] private HopInput m_Input;
    [SerializeField] private HopTrack m_Track;

    private float iteration;
    private float startZ;

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        //смещение
        pos.x = Mathf.Lerp(pos.x, m_Input.Strafe, Time.deltaTime * 5f);
        //прыжок
        pos.y = m_JumpCurve.Evaluate(iteration * m_JumpHeight);
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

        if(m_Track.IsBallOnPlatform(transform.position))
        {
            if (transform.position.x > 0.5f)
            {
                m_JumpCurve.MoveKey(1, new Keyframe(0.5f, 2f));
                m_BallSpeed = 1.3f;
            }

            else if (transform.position.x < -0.5f)
            {
                m_JumpCurve.MoveKey(1, new Keyframe(0.5f, 0.5f));
                m_BallSpeed = 0.8f;
            }

            else
            {
                m_JumpCurve.MoveKey(1, new Keyframe(0.5f, 1f));
                m_BallSpeed = 1f;
            }

            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
