using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Momiji
{
	public class MomijPostprocesser : AssetPostprocessor
	{

		static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			// Enum<EditorExtensionConst.AutoFileName>
			// EditorExtensionConst.AutoFileName.ForEach ();
			var file = AssetDatabase.LoadAssetAtPath ("", typeof (TextAsset));
		}
	}
}