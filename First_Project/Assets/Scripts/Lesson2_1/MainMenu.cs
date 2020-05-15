using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        GameManager.SetGameState(GameState.MainMenu);
    }

    public void LoadLevel(string level)
    {
        SceneLoader.LoadLevel(level);
    }
    
}
