using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Momiji
{
	public class MomijPostprocesser : AssetPostprocessor
	{

		static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			foreach (var name in Enum.GetNames (typeof (EditorExtensionConst.AutoFileName)))
			{
				var file = AssetDatabase.LoadAssetAtPath (@"Assets" + EditorExtensionConst.SAVE_FILE_POINT + name + ".cs", typeof (TextAsset));
				if (!file)
				{
					var builder = new System.Text.StringBuilder ();
					var text = builder.AppendLine ("public class " + name + "{ }").ToString ();
					var assetPath = Application.dataPath + EditorExtensionConst.SAVE_FILE_POINT + name + ".cs";
					Directory.CreateDirectory (Application.dataPath + EditorExtensionConst.SAVE_FILE_POINT);
					if (AssetDatabase.LoadAssetAtPath (assetPath.Replace ("/Editor/..", ""), typeof (UnityEngine.Object)) != null)
						return;

					System.IO.File.WriteAllText (assetPath, text);
					AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
				}
			}
		}
	}
}