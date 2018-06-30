using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
    public abstract class Requestable<Param, Res>
    {
        public UnityWebRequest data;
        public string arrayName = "model";
        public bool array = false;
        public string json;
        public string error;

        protected string HostName { get; set; } = "";
        protected string Path { get; set; } = "";
        protected Dictionary<string, string> Header { get; set; }
    }

    public static class RequestableExtensions
    {
        public static IObservable<Res> SendAsync<Param, Res> (this Requestable<Param, Res> requestable) where Param : IParameterizable where Res : IResponsible
        {
            return Observable.Create<Res> (_ =>
                {

                    Debug.Log ("calling api: " + requestable.data.url);
                    var task = new Task (async () =>
                    {
                        await requestable.data.SendWebRequest ();

                        // 通信エラーチェック
                        if (requestable.data.isNetworkError)
                        {
                            requestable.error = requestable.data.error;
                            Debug.Log (requestable.data.error);
                            _.OnError (new Exception (requestable.error));
                        }
                        else
                        {
                            // UTF8文字列として取得する
                            string text = requestable.data.downloadHandler.text;
                            if (requestable.array)
                            {
                                // Responseで指定した配列で取得する(Default: model)
                                text = "{ \"" + requestable.arrayName + "\": " + text + "}";
                            }
                            requestable.json = text;
                            Debug.Log (text);
                            _.OnNext (JsonUtility.FromJson<Res> (text));
                            _.OnCompleted ();
                        }
                    });

                    task.Start (TaskScheduler.FromCurrentSynchronizationContext ());
                    return Disposable.Create (() => { });
                })
                .SubscribeOnMainThread ()
                .ObserveOnMainThread ();
        }

        public static IObservable<Res> MockSendAsync<Param, Res> (this Requestable<IParameterizable, IResponsible> requestable) where Param : IParameterizable where Res : IResponsible
        {
            return Observable.Create<Res> (_ =>
            {
                var result = MockAPIRequest<Param, Res> (requestable).Result;
                _.OnNext (result);
                _.OnCompleted ();
                // TODO: Error処理も出来るようにいずれ拡張
                return Disposable.Create (() => { });
            });
        }

        private static async Task<Res> MockAPIRequest<Param, Res> (this Requestable<IParameterizable, IResponsible> requestable) where Param : IParameterizable where Res : IResponsible
        {
            var result = default (Res);
#if UNITY_ANDROID || UNITY_IPHONE
            var path = send.Request.data.url;
#else
            // http://localhost を削除
            var path = requestable.data.url.Remove (0, 16);
#endif
            Debug.Log ("reading json file: " + path);

#if UNITY_ANDROID
            WWW reader = new WWW (path);
            while (!reader.isDone) { }
            string text = reader.text;
            if (HasBomWithText (reader.bytes)) text = GetDeletedBomText (reader.text);
#else
            StreamReader reader = new StreamReader (path, Encoding.Default);
            // UTF8文字列として取得する
            string text = reader.ReadToEnd ();
#endif

            using (TextReader stream = new StringReader (text))
            {
                text = stream.ReadToEnd ();
                if (requestable.array)
                {
                    // Responseで指定した配列で取得する(Default: model)
                    text = "{ \"" + requestable.arrayName + "\": " + text + "}";
                }
                Debug.Log ("json : " + text);
                result = JsonUtility.FromJson<Res> (text);
                return result;
            }
        }

        //BOM有無の判定
        private static bool HasBomWithText (byte[] bytes)
        {
            return bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
        }
        //BOM消し
        private static string GetDeletedBomText (string text)
        {
            return text.Remove (0, 1);
        }
    }

    public abstract class GetRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable where Res : IResponsible
    {
        public IObservable<Res> Get (Param param)
        {
            Uri uri = new Uri (HostName + Path);
            if (param is IPathParameterizable)
            {
                uri = new Uri (uri, (param as IPathParameterizable).QueryPath ());
            }
            data = UnityWebRequest.Get (uri);
            Header?.ForEach (_ =>
            {
                data.SetRequestHeader (_.Key, _.Value);
            });
            return this.SendAsync ();
        }
    }
}