using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitInTheHole_Template : MonoBehaviour
{
    [SerializeField] private Transform[] m_cubes;
    [SerializeField] private Transform m_PlayerPosition;
    [SerializeField] private Transform[] m_PositionsVariants;

    private FitInTheHole_FigureTweener tweener;

    public Transform m_CurrentTarget { get; private set; }

    private int currentPosition = -1;

    public Transform[] GetFigure()
    {
        var figure = new Transform[m_cubes.Length + 1];
        m_cubes.CopyTo(figure, 0);
        figure[figure.Length - 1] = m_CurrentTarget;
        return figure;
    }

    public void SetupRandomFigure()
    {
        int rand = Random.Range(0, m_PositionsVariants.Length);

        for (int i = 0; i < m_PositionsVariants.Length; i++)
        {
            if(i == rand)
            {              
                m_CurrentTarget = m_PositionsVariants[i].transform;
                m_PositionsVariants[i].gameObject.SetActive(false);
                continue;
            }

            m_PositionsVariants[i].gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if(tweener != null)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    private void MoveLeft()
    {
        if(!IsMovementPossible(1))
        {
            return;
        }
        currentPosition += 1;
        tweener = m_PlayerPosition.gameObject.AddComponent<FitInTheHole_FigureTweener>();
        tweener.Tween(m_PlayerPosition.position, m_PositionsVariants[currentPosition].position);
    }

    private void MoveRight()
    {
        if (!IsMovementPossible(-1))
        {
            return;
        }
        currentPosition -= 1;
        tweener = m_PlayerPosition.gameObject.AddComponent<FitInTheHole_FigureTweener>();
        tweener.Tween(m_PlayerPosition.position, m_PositionsVariants[currentPosition].position);
    }

    private bool IsMovementPossible(int dir)
    {
        return currentPosition + dir >= 0 && currentPosition + dir < m_PositionsVariants.Length;
    }

    public bool IsPlayerOnTarget()
    {
        return m_PlayerPosition.position.x == m_CurrentTarget.position.x 
            && m_PlayerPosition.position.y == m_CurrentTarget.position.y;
    }
}
