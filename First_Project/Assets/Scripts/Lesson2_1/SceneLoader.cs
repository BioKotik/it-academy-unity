using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string nextLevel;
    public static void LoadLevel(string level)
    {
        nextLevel = level;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator Start()
    {
        GameManager.SetGameState(GameState.Loading);

        yield return new WaitForSeconds(1f);

        if(string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene("MainMenuScene");
            yield break;
        }
        AsyncOperation loading = null;

        loading = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);

        while(!loading.isDone)
        {
            yield return null;
        }

        nextLevel = null;
        SceneManager.UnloadSceneAsync("LoadingScene");
    }
}
