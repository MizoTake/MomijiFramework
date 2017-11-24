using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SampleMockRequest : MockAPI<SampleMockRequest>
{

    private const string path = "sample.json";

    /// <summary>
    /// Mock GET APIを投げるサンプル
    /// </summary>
    /// <param name="onSuccess"> レスポンスに対しての処理を無名関数等で記述する </param>
    /// <param name="onError"> 通信中にエラーが発生した際の処理 </param>
    public static void Get(System.Action<SampleMockResponse> onSuccess, System.Action<string> onError = null)
    {
        RequestData<SampleMockResponse> request = new RequestData<SampleMockResponse>(UnityWebRequest.Get(Path.Combine(URL, path)));
        request.onComplete = onSuccess;
        request.onError = onError;
        Instance.Send<SampleMockResponse>(request);
    }

    /// <summary>
    /// Mock Post APIを投げるサンプル
    /// </summary>
    /// <param name="onSuccess"> レスポンスに対しての処理を無名関数等で記述する </param>
    /// <param name="onError"> 通信中にエラーが発生した際の処理 </param>
    public static void Post(string sample, System.Action<SampleMockResponse> onSuccess = null, System.Action<string> onError = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("Sample", sample);
        //postでも、Mockはひとまず この書き方
        RequestData<SampleMockResponse> request = new RequestData<SampleMockResponse>(UnityWebRequest.Get(Path.Combine(URL, path)));
        request.onComplete = onSuccess;
        request.onError = onError;
        Instance.Send<SampleMockResponse>(request);
    }
}

public class SampleMockResponse
{
    public string data = "";
}