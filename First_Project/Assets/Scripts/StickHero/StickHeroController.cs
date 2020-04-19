using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StickHeroController : MonoBehaviour
{
    [SerializeField] private StickHeroStick m_Stick;
    [SerializeField] private StickHeroPlayer m_Player;
    [SerializeField] private List<StickHeroPlatform> m_Platforms;
    [SerializeField] private StickHeroPlatformSpawner m_Spawner;

    private int counter; //platform counter
    
    public enum EGameState
    {
        Wait,
        Scaling,
        Rotate,
        Movement,
        Defeat,
        
    }

    private EGameState currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = EGameState.Wait;
        counter = 0;
        
        m_Stick.ResetStick(m_Platforms[0].GetStickPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButtonDown(0))
            return;
        switch (currentState)
        {
            //Если не осуществлен старт игры
            case EGameState.Wait:
                currentState = EGameState.Scaling;
                m_Stick.StartScaling();
                break;
            //Если стик увеличивается - прерываем увеличение и поворачиваем
            case EGameState.Scaling:
                currentState = EGameState.Rotate;
                m_Stick.StopScaling();
                break;
            //Ничего не делаем
            case EGameState.Rotate:
                
                break;
            //Ничего не делаем
            case EGameState.Movement:
                
                break;
            
            case EGameState.Defeat:
                print("Defeat");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void StopStickScale()
    {
        currentState = EGameState.Rotate;
        m_Stick.StartRotate();
    }

    public void StopStickRotate()
    {
        currentState = EGameState.Movement;
        
    }

    public void StartPlayerMovement(float length)
    {
        currentState = EGameState.Movement;
        StickHeroPlatform currentPlatform = m_Platforms[counter];
        StickHeroPlatform nextPlatform = m_Platforms[counter + 1];

        Debug.Log($"current + {currentPlatform.name}");
        Debug.Log($"next + {nextPlatform.name}");

        float targetLength = 
            nextPlatform.transform.position.x - m_Stick.transform.position.x;

        float platformSize = nextPlatform.GetPlatformSize();
        
        //Находим минимальную длину стика
        float min = targetLength - platformSize * 0.5f;
        min -= m_Player.transform.localScale.x * 0.9f;
        
        //Находим максимальную длину стика
        float max = targetLength + platformSize * 0.5f;

        //спавним новую платформу
        Vector3 spawnPosition = new Vector3(currentPlatform.transform.position.x + UnityEngine.Random.Range(min, max) + 2f,
            currentPlatform.transform.position.y,currentPlatform.transform.position.z);
        m_Platforms.Add(m_Spawner.SpawnPlatform(spawnPosition));

        //при успехе переходим в центр следующей платформы, иначе падаем

        if (length < min || length > max)
        {
            //будем падать
            float targetPosition = 
                m_Stick.transform.position.x + length + m_Player.transform.localScale.x;
            
            m_Player.StartMovement(targetPosition, true);
        }
        else
        {
            float targetPosition = nextPlatform.transform.position.x;
            m_Player.StartMovement(targetPosition, false);
        }
    }

    public void StopPlayerMovement()
    {
        currentState = EGameState.Wait;
        counter++;
        
        m_Stick.ResetStick(m_Platforms[counter].GetStickPosition());
    }

    public void ShowScores()
    {
        currentState = EGameState.Defeat;
        print($"Game over at {counter}");
    }
}
