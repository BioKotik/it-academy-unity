using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Loading,
    Game,
    GamePause,

}

public class GameManager : MonoBehaviour
{
    private static GameState currentGameState;
    public static GameState CurrentGameState => currentGameState;

    public static Action<GameState> GameStateAction;

    public IPlayer Player;
    public List<IEnemy> Enemies = new List<IEnemy>();

    public static void SetGameState(GameState state)
    {
        currentGameState = state;
        GameStateAction?.Invoke(state);
    }

    private void Start()
    {
        SetGameState(GameState.Game);
        print(Player);
        print($"{Enemies.Count} - enemies");
    }
}
