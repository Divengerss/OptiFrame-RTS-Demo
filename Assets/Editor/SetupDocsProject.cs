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

        if (GUILayout.Button("Create DOTS Project Structure"))
        {
            CreateDotsProjectStructure();
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

        CreateSampleSystem();
        CreateSampleComponent();
        AssetDatabase.Refresh();
    }

    private void CreateSampleSystem()
    {
        string systemPath = "Assets/Scripts/Systems/SampleSystem.cs";
        if (!File.Exists(systemPath))
        {
            using (StreamWriter writer = new StreamWriter(systemPath))
            {
                writer.WriteLine("using Unity.Entities;");
                writer.WriteLine();
                writer.WriteLine("public class SampleSystem : SystemBase");
                writer.WriteLine("{");
                writer.WriteLine("    protected override void OnUpdate()");
                writer.WriteLine("    {");
                writer.WriteLine("        // TODO: Add your system logic here");
                writer.WriteLine("    }");
                writer.WriteLine("}");
            }
            Debug.Log("Created SampleSystem.cs");
        }
        else
        {
            Debug.Log("SampleSystem.cs already exists");
        }
    }

    private void CreateSampleComponent()
    {
        string componentPath = "Assets/Scripts/Components/SampleComponent.cs";
        if (!File.Exists(componentPath))
        {
            using (StreamWriter writer = new StreamWriter(componentPath))
            {
                writer.WriteLine("using Unity.Entities;");
                writer.WriteLine();
                writer.WriteLine("[GenerateAuthoringComponent]");
                writer.WriteLine("public struct SampleComponent : IComponentData");
                writer.WriteLine("{");
                writer.WriteLine("    public float Value;");
                writer.WriteLine("}");
            }
            Debug.Log("Created SampleComponent.cs");
        }
        else
        {
            Debug.Log("SampleComponent.cs already exists");
        }
    }
}