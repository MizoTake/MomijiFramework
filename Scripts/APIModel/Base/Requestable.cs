using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
    public abstract class Requestable<Param, Res>
    {
        protected UnityWebRequest data;
        protected Task core;
        protected string arrayName = "model";
        protected bool array = false;

        protected string HostName { get; set; } = "";
        protected string Path { get; set; } = "";
        protected Dictionary<string, string> Header { get; set; } = new Dictionary<string, string> () { { "Content-Type", "application/json" } };

        protected virtual void UpdateRequest (Param param) { }

        public async void Dispatch (Param param)
        {
            UpdateRequest (param);

            Debug.Log ("hajimari");
            var context = TaskScheduler.FromCurrentSynchronizationContext ();
            var isDone = data.isDone;
            await Task.Run (() =>
            {
                Debug.Log ("core start");
                if (!isDone)
                {
                    core.Wait ();
                }
                core.Start (context);
                // core.RunSynchronously (context);
                Debug.Log ("wait no owari");
            });
            core.Wait ();
            Debug.Log ("owari");
        }

        protected IObservable<Res> ResponseData ()
        {
            return Observable.Create<Res> (_ =>
            {
                Debug.Log ("set core");
                core = new Task (async () =>
                {
                    Debug.Log ("calling api: " + data.url);

                    await data.SendWebRequest ();

                    // 通信エラーチェック
                    if (data.isNetworkError)
                    {
                        Debug.Log (data.error);
                        _.OnError (new Exception (data.error));
                    }
                    else
                    {
                        // UTF8文字列として取得する
                        string text = data.downloadHandler.text;
                        if (array)
                        {
                            // Responseで指定した配列で取得する(Default: model)
                            text = "{ \"" + arrayName + "\": " + text + "}";
                        }
                        Debug.Log (text);
                        _.OnNext (JsonUtility.FromJson<Res> (text));
                        _.OnCompleted ();
                    }
                });
                core.ConfigureAwait (false);
                return Disposable.Create (() => { });
            });
        }

        //BOM有無の判定
        private bool HasBomWithText (byte[] bytes)
        {
            return bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
        }
        //BOM消し
        private string GetDeletedBomText (string text)
        {
            return text.Remove (0, 1);
        }
    }
}