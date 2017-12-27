using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
    public class SampleRequest : API<SampleRequest>
    {

        private const string path = "sample/get";

        /// <summary>
        /// GET APIを投げるサンプル
        /// </summary>
        /// <param name="onSuccess"> レスポンスに対しての処理を無名関数等で記述する </param>
        /// <param name="onError"> 通信中にエラーが発生した際の処理 </param>
        public static void Get(System.Action<SampleResponse> onSuccess, System.Action<string> onError = null)
        {
            RequestData<SampleResponse> request = new RequestData<SampleResponse>(UnityWebRequest.Get(Path.Combine(URL, path)));
            request.onComplete = onSuccess;
            Instance.Send<SampleResponse>(request);
        }

        /// <summary>
        /// Post APIを投げるサンプル
        /// </summary>
        /// <param name="onSuccess"> レスポンスに対しての処理を無名関数等で記述する </param>
        /// <param name="onError"> 通信中にエラーが発生した際の処理 </param>
        public static void Post(string sample, System.Action<SampleResponse> onSuccess = null, System.Action<string> onError = null)
        {
            WWWForm form = new WWWForm();
            form.AddField("Sample", sample);
            RequestData<SampleResponse> request = new RequestData<SampleResponse>(UnityWebRequest.Post(URL, form));
            request.onComplete = onSuccess;
            Instance.Send<SampleResponse>(request);
        }
    }

    public class SampleResponse
    {
        public string data = "";
    }
}