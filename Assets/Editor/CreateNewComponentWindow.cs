using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CreateNewComponentWindow : EditorWindow
{
    private string componentName = "NewComponent";
    private List<FieldData> fields = new List<FieldData>();
    private string[] fieldTypes = {
        "Boolean", "String", "Integer", "Float", "Float3"
    };
    private int selectedTypeIndex = 0;
    private bool hasInvalidFieldNames = false;

    public static void ShowWindow()
    {
        GetWindow<CreateNewComponentWindow>("Create New Component");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Component", EditorStyles.boldLabel);

        componentName = EditorGUILayout.TextField("Component Name", componentName);

        GUILayout.Label("Fields", EditorStyles.boldLabel);

        hasInvalidFieldNames = false;
        for (int i = 0; i < fields.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            fields[i].name = EditorGUILayout.TextField(fields[i].name);
            fields[i].type = EditorGUILayout.Popup(fields[i].type, fieldTypes);
            if (GUILayout.Button("Remove"))
            {
                fields.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();

            if (!Regex.IsMatch(fields[i].name, @"^[a-zA-Z]"))
            {
                hasInvalidFieldNames = true;
                EditorGUILayout.HelpBox($"Field name '{fields[i].name}' is invalid. It must start with a letter.", MessageType.Error);
            }
        }

        if (GUILayout.Button("Add Field"))
        {
            fields.Add(new FieldData() { name = "NewField", type = selectedTypeIndex });
        }

        GUI.enabled = !hasInvalidFieldNames;
        if (GUILayout.Button("Create Component"))
        {
            CreateComponent();
        }
        GUI.enabled = true;
    }

    private void CreateComponent()
    {
        string componentPath = $"Assets/Scripts/Components/{componentName}.cs";
        if (!File.Exists(componentPath))
        {
            using (StreamWriter writer = new StreamWriter(componentPath))
            {
                writer.WriteLine("using Unity.Entities;");
                writer.WriteLine("using Unity.Mathematics;");
                writer.WriteLine();
                // writer.WriteLine("[GenerateAuthoringComponent]");
                writer.WriteLine($"public struct {componentName} : IComponentData");
                writer.WriteLine("{");
                foreach (var field in fields)
                {
                    writer.WriteLine($"    public {GetFieldType(field.type)} {field.name};");
                }
                writer.WriteLine("}");
            }
            Debug.Log($"Created {componentName}.cs");
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.Log($"{componentName}.cs already exists");
        }
    }

    private string GetFieldType(int typeIndex)
    {
        switch (typeIndex)
        {
            case 0: return "bool";
            case 1: return "string";
            case 2: return "int";
            case 3: return "float";
            case 4: return "float3";
            default: return "int";
        }
    }
}
