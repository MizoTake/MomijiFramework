using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

/// <summary>
/// APIオブジェクト
/// </summary>
public class MockAPI<T> : Singleton<T> where T : Singleton<T>
{

    private static readonly string DIRECTORY_NAME = "/MockAPI/";

    protected static string URL
    {
        get
        {
#if UNITY_IPHONE
            return Application.dataPath + "/Raw" + DIRECTORY_NAME;
#elif UNITY_ANDROID
            return "jar:file://" + Application.dataPath + "!/assets" + DIRECTORY_NAME;
#else
            return Application.streamingAssetsPath + DIRECTORY_NAME;
#endif
        }
    }

    /// <summary>
    /// MockAPIコール
    /// </summary>
    /// <param name="request">Request.</param>
    public void Send<T>(RequestData<T> request, bool array = false)
    {
        Instance.StartCoroutine(_Send<T>(request, array));
    }

    /// <summary>
    /// MockAPIコール（コア実装）
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="array">配列かどうか</param>
    private IEnumerator _Send<T>(RequestData<T> data, bool array = false)
    {
        var directoryPath = data.request.url;
        if (APIConst.URL.Length > 0)
        {
            directoryPath = data.request.url.ReplaceDelete(APIConst.URL);
        }
#if UNITY_ANDROID || UNITY_IPHONE
        var path = directoryPath;
#else
        // http://localhost を削除
        var path = directoryPath.Remove(0, 16);
#endif
        Debug.Log("reading json file: " + path);

#if UNITY_ANDROID
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        string text = reader.text;
        if (HasBomWithText(reader.bytes)) text = GetDeletedBomText(reader.text);
#else
        StreamReader reader = new StreamReader(path, Encoding.Default);
        yield return new WaitForSeconds(1.0f);
        // UTF8文字列として取得する
        string text = reader.ReadToEnd();
#endif
        using (TextReader stream = new StringReader(text))
        {
            text = stream.ReadToEnd();
            if (array)
            {
                // Responseにmodel変数定義で配列の取得が可能にする
                text = "{ \"model\": " + text + "}";
            }
            Debug.Log("json : " + text);
            data.response = JsonUtility.FromJson<T>(text);
            data.OnComplete();
            yield return 0;
        }
    }

    //BOM有無の判定
    static bool HasBomWithText(byte[] bytes)
    {
        return bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
    }
    //BOM消し
    static string GetDeletedBomText(string text)
    {
        return text.Remove(0, 1);
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
