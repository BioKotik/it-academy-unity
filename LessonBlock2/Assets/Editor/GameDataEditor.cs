using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameDataEditor : EditorWindow
{
    public GameData gameData;
    private string gameDataFilePath = "/Data/GameData.json";

    [MenuItem("Tools/Game Data Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(GameDataEditor)).Show();
    }

    private void OnGUI()
    {
        if(gameData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save Data"))
            {
                SaveGameData();
            }
        }

        if(GUILayout.Button("Load Data"))
        {
            LoadGameData();
        }
    }

    private void LoadGameData()
    {
        var path = Application.dataPath + gameDataFilePath;
        if (File.Exists(path))
        {
            var dataAsJson = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
        }
    }

    private void SaveGameData()
    {
        var dataAsJson = JsonUtility.ToJson(gameData);
        var path = Application.dataPath + gameDataFilePath;
        File.WriteAllText(path, dataAsJson);
        AssetDatabase.Refresh();
    }
}
