using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

//jenkinsで呼ぶとき： -quit -batchmode -executeMethod BuildBatch.使うメソッド

public class BuildBatch
{
    private struct BuildParameter
    {
        public string outputFileName;
        public BuildTarget targetPlatform;
        public BuildParameter(string outputFileName, BuildTarget targetPlatform)
        {
            this.outputFileName = outputFileName;
            this.targetPlatform = targetPlatform;
        }
    }

    private static string outputDirectory = "";

    private static void BuildByParameter(BuildParameter param)
    {
        string outputFileName = param.outputFileName;
        BuildTarget targetPlatform = param.targetPlatform;

        List<string> allScene = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                allScene.Add(scene.path);
            }
        }

        PlayerSettings.statusBarHidden = true;

        // 実行
        string errorMessage = BuildPipeline.BuildPlayer(
                allScene.ToArray(),
                outputFileName,
                targetPlatform,
                BuildOptions.None
        );

        // 結果出力
        if (!string.IsNullOrEmpty(errorMessage))
        {
            Debug.LogError("Error!");
            Debug.LogError(errorMessage);
            throw new Exception();
        }

        Debug.Log("Complete!");
    }

    public static void BuildApk()
    {
        BuildParameter param = new BuildParameter(outputDirectory + @"\android\" + PlayerSettings.productName + "Dev.apk", BuildTarget.Android);
        BuildByParameter(param);
    }

    public static void BuildExe()
    {
        BuildParameter param = new BuildParameter(outputDirectory + @"\exe\ " + PlayerSettings.productName + "Dev.exe", BuildTarget.StandaloneWindows);
        BuildByParameter(param);
    }

    public static void BuildWebGL()
    {
        BuildParameter param = new BuildParameter(outputDirectory + @"\webgl\" + PlayerSettings.productName + "Dev", BuildTarget.WebGL);
        BuildByParameter(param);
    }
}