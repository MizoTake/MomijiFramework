using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditorInternal;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace Momiji
{
    [InitializeOnLoad]
    public class SceneInfoCreator
    {
        private const string SCENE_ENUM_HASH_KEY = "Scene_Info_Hash";

        [MenuItem("Assets/Creator/SceneInfoCreator")]
        public static void _sceneNameCreator()
        {
            if (EditorApplication.isPlaying || Application.isPlaying)
                return;

            EditorApplication.delayCall += BuildSceneName;
        }

        static SceneInfoCreator()
        {
            if (EditorApplication.isPlaying || Application.isPlaying)
                return;

            EditorApplication.delayCall += BuildSceneName;
        }

        static void BuildSceneName()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            builder = WriteTagManagerClass(builder);

            string text = builder.ToString().Replace(",}", "}");
            string assetPath = Application.dataPath + EditorExtensionConst.SAVE_FILE_POINT + "SceneInfo.cs";

            Directory.CreateDirectory(Application.dataPath + EditorExtensionConst.SAVE_FILE_POINT);

            if (AssetDatabase.LoadAssetAtPath(assetPath.Replace("/Editor/..", ""), typeof(UnityEngine.Object)) != null && EditorPrefs.GetInt(SCENE_ENUM_HASH_KEY, 0) == text.GetHashCode())
                return;

            System.IO.File.WriteAllText(assetPath, text);
            EditorPrefs.SetInt(SCENE_ENUM_HASH_KEY, text.GetHashCode());
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
            EditorApplication.delayCall -= BuildSceneName;
        }

        static System.Text.StringBuilder WriteTagManagerClass(System.Text.StringBuilder builder)
        {
            var sceneNames = new List<string>();
            EditorBuildSettings.scenes
                        .Where(_ => _.enabled)
                        .Select(_ => Path.GetFileNameWithoutExtension(Path.Combine(Directory.GetCurrentDirectory(), _.path)))
                        .Where(_ => !string.IsNullOrEmpty(_))
                        .ForEach(_ => sceneNames.Add(_));
            builder.AppendLine("/// <summary>");
            builder.AppendFormat("/// Access Scene Class").AppendLine();
            builder.AppendLine("/// </summary>");
            /// 
            builder.Append("public sealed class SceneInfo").AppendLine();
            builder.AppendLine("{");
            WriteSceneEnum(builder, sceneNames);
            WriteSceneNameArray(builder, sceneNames);
            builder.AppendLine("}");
            return builder;
        }

        static void WriteSceneEnum(System.Text.StringBuilder builder, List<string> sceneNames)
        {
            builder.Append("\t").AppendLine("/// <summary>");
            builder.Append("\t").AppendFormat("/// Access Scene Number Enum").AppendLine();
            builder.Append("\t").AppendLine("/// </summary>");
            builder.Append("\t").Append("public enum SceneEnum").AppendLine();
            builder.Append("\t").AppendLine("{");
            sceneNames.ForEach((sceneName, i) =>
            {
                var comma = (i == sceneNames.Count() - 1) ? "" : ",";
                builder.Append("\t").Append("\t").AppendFormat("{0} = {1}", sceneName.SymbolReplace(), i + comma).AppendLine();
            });
            builder.Append("\t").AppendLine("};");
        }

        static void WriteSceneNameArray(System.Text.StringBuilder builder, List<string> sceneNames)
        {
            builder.Append("\t").AppendLine("/// <summary>");
            builder.Append("\t").AppendFormat("/// Access Scene Name Array").AppendLine();
            builder.Append("\t").AppendLine("/// </summary>");
            builder.Append("\t").Append("public static readonly string[] SceneNames = new string[] {");
            sceneNames.ForEach((sceneName, i) =>
            {
                var comma = (i == sceneNames.Count() - 1) ? "" : ",";
                builder.AppendFormat(@" ""{0}""" + comma, sceneName);
            });
            builder.AppendLine(" }; ");
        }
    }
}