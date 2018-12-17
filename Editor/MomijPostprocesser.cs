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
			foreach(var name in Enum.GetNames(typeof(EditorExtensionConst.AutoFileName))) {
				var file = AssetDatabase.LoadAssetAtPath (EditorExtensionConst.SAVE_FILE_POINT + name + ".cs", typeof (TextAsset));
			}
			
		}
	}
}
