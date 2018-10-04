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

        private Task runing;

        protected IObserver<Res> notify;
        protected string arrayName = "model";
        protected bool array = false;

        protected string HostName { get; set; } = "";
        protected string Path { get; set; } = "";
        protected Dictionary<string, string> Header { get; set; } = new Dictionary<string, string> () { { "Content-Type", "application/json; charset=UTF-8" } };

        protected virtual UnityWebRequest UpdateRequest (Param param) => new UnityWebRequest ();

        public async void Dispatch (Param param)
        {
            var data = UpdateRequest (param);

            var task = new Task (async () =>
            {
                Debug.Log ("calling api: " + data.url);

                await data.SendWebRequest ();

                // 通信エラーチェック
                if (data.isNetworkError)
                {
                    Debug.Log (data.error);
                    notify.OnError (new Exception (data.error));
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
                    Debug.Log (data.uri.AbsoluteUri + ": " + text);
                    notify.OnNext (JsonUtility.FromJson<Res> (text));
                }
            });
            if (runing == null)
            {
                runing = task;
            }
            else
            {
                await runing;
                runing = task;
            }
            task.Start (TaskScheduler.FromCurrentSynchronizationContext ());
            await task;
        }

        protected IObservable<Res> ResponseData ()
        {
            return Observable.Create<Res> (_ =>
            {
                notify = _;
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