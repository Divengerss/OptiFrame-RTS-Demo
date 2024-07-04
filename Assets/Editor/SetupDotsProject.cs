using UnityEngine;
using UnityEditor;
using System.IO;

public class SetupDotsProject : EditorWindow
{
    [MenuItem("Tools/Setup DOTS Project")]
    public static void ShowWindow()
    {
        GetWindow<SetupDotsProject>("Setup DOTS Project");
    }

    private void OnGUI()
    {
        GUILayout.Label("Setup DOTS Project", EditorStyles.boldLabel);

        if (GUILayout.Button("Create DOTS Project Structure (Required)"))
        {
            CreateDotsProjectStructure();
        }

        GUILayout.Label("DOTS ECS Parameters", EditorStyles.boldLabel);

        if (GUILayout.Button("Create New Component"))
        {
            CreateNewComponentWindow.ShowWindow();
        }
        if (GUILayout.Button("Create New System"))
        {
            CreateNewSystemWindow.ShowWindow();
        }
    }

    private void CreateDotsProjectStructure()
    {
        string[] folders = {
            "Assets/Scripts",
            "Assets/Scripts/Systems",
            "Assets/Scripts/Components",
            "Assets/Scripts/Jobs",
            "Assets/Scenes",
            "Assets/Resources"
        };

        foreach (var folder in folders)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                Debug.Log("Created folder: " + folder);
            }
            else
            {
                Debug.Log("Folder already exists: " + folder);
            }
        }
        AssetDatabase.Refresh();
    }
}
