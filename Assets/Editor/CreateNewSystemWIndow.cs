using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateNewSystemWindow : EditorWindow
{
    private string systemName = "NewSystem";
    private bool useBurstCompile = false;

    public static void ShowWindow()
    {
        GetWindow<CreateNewSystemWindow>("Create New System");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New System", EditorStyles.boldLabel);

        systemName = EditorGUILayout.TextField("System Name", systemName);
        useBurstCompile = EditorGUILayout.Toggle("Use Burst Compile", useBurstCompile);

        if (GUILayout.Button("Create System"))
        {
            CreateSystem();
        }
    }

    private void CreateSystem()
    {
        string systemPath = $"Assets/Scripts/Systems/{systemName}.cs";
        if (!File.Exists(systemPath))
        {
            using (StreamWriter writer = new StreamWriter(systemPath))
            {
                writer.WriteLine("using Unity.Entities;");
                if (useBurstCompile)
                {
                    writer.WriteLine("using Unity.Burst;");
                }
                writer.WriteLine();
                writer.WriteLine("public partial struct " + systemName + " : ISystem");
                writer.WriteLine("{");
                if (useBurstCompile)
                {
                    writer.WriteLine("    [BurstCompile]");
                }
                writer.WriteLine("    public void OnUpdate(ref SystemState state)");
                writer.WriteLine("    {");
                writer.WriteLine("        // TODO: Add your system logic here");
                writer.WriteLine("    }");
                writer.WriteLine("}");
            }
            Debug.Log($"Created {systemName}.cs");
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.Log($"{systemName}.cs already exists");
        }
    }
}
