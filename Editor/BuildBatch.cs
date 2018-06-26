using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Momiji {
    //jenkinsで呼ぶとき： -quit -batchmode -executeMethod BuildBatch.使うメソッド

    public class BuildBatch {
        private struct BuildParameter {
            public string outputFileName;
            public BuildTarget targetPlatform;
            public BuildParameter (string outputFileName, BuildTarget targetPlatform) {
                this.outputFileName = outputFileName;
                this.targetPlatform = targetPlatform;
            }
        }

        private static string outputDirectory = "";

        private static void BuildByParameter (BuildParameter param) {

            string outputFileName = param.outputFileName;
            BuildTarget targetPlatform = param.targetPlatform;

            List<string> allScene = new List<string> ();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
                if (scene.enabled) {
                    allScene.Add (scene.path);
                }
            }

            PlayerSettings.statusBarHidden = true;

            // 実行
            string errorMessage = BuildPipeline.BuildPlayer(
                    allScene.ToArray(),
                    outputFileName,
                    targetPlatform,
                    BuildOptions.None
            ).ToString();

            // 結果出力
            if (!string.IsNullOrEmpty (errorMessage.ToString ())) {
                Debug.LogError ("Error!");
                Debug.LogError (errorMessage);
                throw new Exception ();
            }

            Debug.Log ("Complete!");
        }

        public static void BuildApk () {
                var param = new BuildParameter (outputDirectory + @"\android\" + PlayerSettings.productName + "Dev.apk ", BuildTarget.Android);
            BuildByParameter(param);
        }

        public static void BuildExe()
        {
            var param = new BuildParameter(outputDirectory + @"\exe\ " + PlayerSettings.productName + "Dev.exe ", BuildTarget.StandaloneWindows);
            BuildByParameter(param);
        }

        public static void BuildWebGL()
        {
            var param = new BuildParameter(outputDirectory + @"\webgl\ " + PlayerSettings.productName + "Dev ", BuildTarget.WebGL);
            BuildByParameter (param);
        }
    }
}