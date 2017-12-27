using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
    /// <summary>
    /// APIオブジェクト
    /// </summary>
    public class API<T> : Singleton<T> where T : Singleton<T>
    {

        protected static string URL = "";

        /// <summary>
        /// APIコール
        /// </summary>
        /// <param name="request">Request.</param>
        public void Send<T>(RequestData<T> request, bool array = false)
        {
            Instance.StartCoroutine(_Send<T>(request, array));
        }

        /// <summary>
        /// APIコール（コア実装）
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="array">配列かどうか</param>
        private IEnumerator _Send<T>(RequestData<T> data, bool array = false)
        {
            Debug.Log("calling api: " + data.request.url);
            yield return data.request.Send();

            // 通信エラーチェック
            if (data.request.isNetworkError)
            {
                data.error = data.request.error;
                data.OnError();
                Debug.Log(data.request.error);
            }
            else
            {
                // UTF8文字列として取得する
                string text = data.request.downloadHandler.text;
                if (array)
                {
                    // Responseにmodel変数定義で配列の取得を可能にする
                    text = "{ \"model\": " + text + "}";
                }
                data.json = text;
                data.response = JsonUtility.FromJson<T>(text);
                data.OnComplete();
                Debug.Log(text);
            }
            yield return 0;
        }

        /// <summary>
        /// リクエストデータ
        /// </summary>
        public class RequestData<T>
        {
            /// <summary>
            /// リクエスト
            /// </summary>
            public UnityWebRequest request;

            /// <summary>
            /// レスポンス
            /// </summary>
            public T response;

            public string json;

            public string error;

            /// <summary>
            /// レスポンス受け取り通知
            /// </summary>
            public Action<T> onComplete;

            /// <summary>
            /// エラー通知
            /// </summary>
            public Action<string> onError;

            public RequestData(UnityWebRequest request)
            {
                this.request = request;
            }

            public void OnComplete()
            {
                if (onComplete != null)
                {
                    onComplete(response);
                }
            }

            public void OnError()
            {
                if (onError != null)
                {
                    onError(error);
                }
            }
        }
    }
}