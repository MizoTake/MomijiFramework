using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;
using Utf8Json;

namespace Momiji
{
    public abstract class Requestable<Param, Res> where Param : IParameterizable
    {
        protected IObserver<Res> notify;
        protected string HostName { get; set; } = "";
        protected string Path { get; set; } = "";
        protected Dictionary<string, string> Header { get; set; } = new Dictionary<string, string> () { { "Content-Type", "application/json; charset=UTF-8" } };

        protected virtual UnityWebRequest UpdateRequest (Param param) => new UnityWebRequest ();

        public async void Dispatch (Param param)
        {
            var data = UpdateRequest (param);

            var task = new UniTask (async () =>
            {
                Debug.Log ("calling api: " + data.url);

                await data.SendWebRequest ();

                if (data.isNetworkError)
                {
                    Debug.Log (data.error);
                    notify.OnError (new Exception (data.error));
                }
                else
                {
                    var text = data.downloadHandler.text;
                    Debug.Log (data.uri.AbsoluteUri + ": " + text);
                    notify.OnNext (JsonSerializer.Deserialize<Res> (text));
                }
            });
            await task;
        }

        protected IObservable<Res> ResponseData ()
        {
            return Observable.Create<Res> (x =>
            {
                notify = x;
                return Disposable.Create (() => { });
            });
        }
    }
}
