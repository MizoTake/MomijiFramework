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
        private Task runing;

        protected IObserver<Res> notify;

        protected string HostName { get; set; } = "";
        protected string Path { get; set; } = "";
        protected Dictionary<string, string> Header { get; set; } = new Dictionary<string, string> () { { "Content-Type", "application/json; charset=UTF-8" } };

        protected virtual UnityWebRequest UpdateRequest (Param param) => new UnityWebRequest ();

        public async void Dispatch (Param param)
        {
            if (!(typeof (Res) is IResponsible || typeof (Res) is IList<IResponsible>))
            {
                new Exception ("Momiji Error: Res is not a specified type");
            }
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
                    Res response;
                    string text = data.downloadHandler.text;
                    if (typeof (Res) is IList<IResponsible>)
                    {
                        text = "{ \" array \": " + text + "}";
                        // response = JsonSerializer.Deserialize<dynamic> (text);
                        // response = response["array"];
                    }
                    else
                    {
                        response = JsonSerializer.Deserialize<Res> (text);
                    }
                    Debug.Log (data.uri.AbsoluteUri + ": " + text);
                    notify.OnNext (JsonSerializer.Deserialize<Res> (text));
                }
            });
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
    }
}