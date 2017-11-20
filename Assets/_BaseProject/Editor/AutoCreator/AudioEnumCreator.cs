using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditorInternal;
using System.Collections.Generic;
using System;

[InitializeOnLoad]
public class AudioEnumCreator
{
    private const string AUDIO_ENUM_HASH_KEY = "Audio_Enum_Hash";

    [MenuItem("Assets/Creator/AudioEnumCreator")]
    public static void _AudioEnumCreator()
    {
        if (EditorApplication.isPlaying || Application.isPlaying)
            return;

        BuildAudioName();
    }

    static AudioEnumCreator()
    {
        if (EditorApplication.isPlaying || Application.isPlaying)
            return;

        EditorApplication.delayCall += BuildAudioName;
    }

    static void BuildAudioName()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        builder = WriteManagerClass(builder);

        string text = builder.ToString().Replace(",}", "}");
        string assetPath = Application.dataPath + EditorExtensionConst.SAVE_FILE_POINT + "AudioEnums.cs";

        if (AssetDatabase.LoadAssetAtPath(assetPath.Replace("/Editor/..", ""), typeof(UnityEngine.Object)) != null && EditorPrefs.GetInt(AUDIO_ENUM_HASH_KEY, 0) == text.GetHashCode())
            return;

        System.IO.File.WriteAllText(assetPath, text);
        EditorPrefs.SetInt(AUDIO_ENUM_HASH_KEY, text.GetHashCode());
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
        EditorApplication.delayCall -= BuildAudioName;
    }

    static System.Text.StringBuilder WriteManagerClass(System.Text.StringBuilder builder)
    {
        WriteAudioScript(builder);
        return builder;
    }

    static void WriteAudioScript(System.Text.StringBuilder builder)
    {
        SoundParameter source = LoadResources.ScriptableObject("SoundParameter") as SoundParameter;
        WriteAudioEnum(builder, source.BGMClip, SoundType.BGM);
        WriteAudioEnum(builder, source.SEClip, SoundType.SE);
        WriteAudioEnum(builder, source.VoiceClip, SoundType.VOICE);
    }

    static void WriteAudioEnum(System.Text.StringBuilder builder, AudioClip[] audioNames, SoundType type)
    {
        builder.AppendLine("/// <summary>");
        builder.AppendFormat("/// Access " + type.ToString() + " Enum").AppendLine();
        builder.AppendLine("/// </summary>");
        builder.Append("public enum " + type.ToString()).AppendLine();
        builder.AppendLine("{");
        audioNames.ForEach((audioName, i) =>
        {
            var comma = (i == audioNames.Count() - 1) ? "" : ",";
            builder.Append("\t").AppendFormat("{0} = {1}", audioName?.name.SymbolReplace(), i + comma).AppendLine();
        });
        builder.AppendLine("};");
    }
}

public enum SoundType
{
    BGM,
    SE,
    VOICE
}
