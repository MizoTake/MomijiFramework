using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditorInternal;
using System.Collections.Generic;
using System;

[InitializeOnLoad]
public class LayerEnumCreator
{
    private const string LAYER_ENUM_HASH_KEY = "Layer_Enum_Hash";

    [MenuItem("Assets/Creator/LayerEnumCreator")]
    public static void _layerNameCreator()
    {
        if (EditorApplication.isPlaying || Application.isPlaying)
            return;

        EditorApplication.delayCall += BuildlayerName;
    }

    static LayerEnumCreator()
    {
        if (EditorApplication.isPlaying || Application.isPlaying)
            return;

        EditorApplication.delayCall += BuildlayerName;
    }

    static void BuildlayerName()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        builder = WriteTagManagerClass(builder);


        string text = builder.ToString().Replace(",}", "}");
        string assetPath = Application.dataPath + EditorExtensionConst.SAVE_FILE_POINT + "LayerEnum.cs";

        if (AssetDatabase.LoadAssetAtPath(assetPath.Replace("/Editor/..", ""), typeof(UnityEngine.Object)) != null && EditorPrefs.GetInt(LAYER_ENUM_HASH_KEY, 0) == text.GetHashCode())
            return;

        System.IO.File.WriteAllText(assetPath, text);
        EditorPrefs.SetInt(LAYER_ENUM_HASH_KEY, text.GetHashCode());
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
        EditorApplication.delayCall -= BuildlayerName;
    }

    static System.Text.StringBuilder WriteTagManagerClass(System.Text.StringBuilder builder)
    {
        List<string> layerNames = InternalEditorUtility.layers.ToList();
        WriteLayerEnum(builder, layerNames);
        return builder;
    }

    static void WriteLayerEnum(System.Text.StringBuilder builder, List<string> layerNames)
    {
        builder.AppendLine("/// <summary>");
        builder.AppendFormat("/// Access Layer Number Enum").AppendLine();
        builder.AppendLine("/// </summary>");
        builder.Append("public enum LayerEnum").AppendLine();
        builder.AppendLine("{");
        layerNames.ForEach((layerName, i) =>
        {
            var comma = (i == layerNames.Count() - 1) ? "" : ",";
            if (i >= 3)
            {
                i += 1;
                if (i >= 6)
                {
                    i += 2;
                }
            }
            builder.Append("\t").AppendFormat(@"{0} = {1}", layerName.SymbolReplace(), i + comma).AppendLine();
        });
        builder.AppendLine("};");
    }
}