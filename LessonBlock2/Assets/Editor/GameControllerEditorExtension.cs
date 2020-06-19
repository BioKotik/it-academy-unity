using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameControllerEditorExtension : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var myScript = (GameManager)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Hide Characters", 
            GUILayout.Width(120), GUILayout.Height(20)))
        {
            myScript.HideAllCharacters();
        }

        if (GUILayout.Button("Show Characters", 
            GUILayout.Width(120), GUILayout.Height(20)))
        {
            myScript.ShowAllCharacters();
        }
        GUILayout.EndHorizontal();
    }

    [MenuItem("Tools/Main/Select Game Manager %g")]
    public static void SelectGameManager()
    {
        var manager = FindObjectOfType<GameManager>();
        if(manager != null)
        {
            Selection.objects = new Object[] { manager.gameObject };
        }
    }
}
