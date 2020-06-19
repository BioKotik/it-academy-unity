using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private static int coins;

    public static int Coins
    {
        get => coins;
        set
        {
            coins = value;
            Debug.Log($"Coins count: {coins}");
        }
    }
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

#if UNITY_EDITOR
    [ContextMenu("Hide all characters")]
    public void HideAllCharacters()
    {
        var characters = FindCharacters();

        foreach (var character in characters)
        {
            character.SetActive(false);
        }
    }

    [ContextMenu("Show all characters")]
    public void ShowAllCharacters()
    {
        var characters = FindCharacters();

        foreach (var character in characters)
        {
            character.SetActive(true);
        }
    }

    private List<GameObject> FindCharacters()
    {
        var characters = Resources.FindObjectsOfTypeAll<Enemy>().
            Select(e => e.gameObject).ToList();

        var players = Resources.FindObjectsOfTypeAll<Player>().
            Select(e => e.gameObject).ToList();

        characters.AddRange(players);

        return characters;
    }
#endif
}
