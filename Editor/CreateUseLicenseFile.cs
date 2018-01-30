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
    public static class CreateUseLicenseFile
    {

        private const string USE_LICENSE_FILE = "used_license_file";
        private const string fileName = "USE_LICENSE";

        [MenuItem("Assets/Create/Used LICENSE File")]
        static void CreateUsedLICENSEFile()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            string[] files;
            string assetPath = Application.dataPath + "/" + fileName;

            AssetDatabase.MoveAssetToTrash(assetPath);

            files = AssetDatabase.FindAssets("LICENSE");
            foreach (var guid in files)
            {
                if (Path.GetFileName((AssetDatabase.GUIDToAssetPath(guid))) == "CreateUseLicenseFile.cs" ||
                    Path.GetFileName((AssetDatabase.GUIDToAssetPath(guid))) == fileName) continue;
                var path = AssetDatabase.GUIDToAssetPath(guid).Substring("Assets".Length);
                StreamReader reader = new StreamReader(Application.dataPath + path);
                builder.Append(reader.ReadToEnd());
                builder.Append("\t").Append("\t");
                reader.Close();
            }

            string text = builder.ToString();

            if (AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)) != null && EditorPrefs.GetInt(USE_LICENSE_FILE, 0) == text.GetHashCode())
                return;

            System.IO.File.WriteAllText(assetPath, text);
            EditorPrefs.SetInt(USE_LICENSE_FILE, text.GetHashCode());
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
        }
    }
}
