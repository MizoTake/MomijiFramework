using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateScriptableObject : ScriptableObject
{

    [MenuItem("Assets/Create/ScriptableObject")]
    public static void CreateAsset()
    {
        CreateAsset<ScriptableObject>();
    }

    public static void CreateAsset<Type>() where Type : ScriptableObject
    {
        Type item = ScriptableObject.CreateInstance<Type>();

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/_BaseProject/Resources/Data/" + typeof(Type) + ".asset");

        AssetDatabase.CreateAsset(item, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }
}
