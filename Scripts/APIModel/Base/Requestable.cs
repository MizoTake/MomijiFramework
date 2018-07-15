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
        protected UnityWebRequest data;
        protected Task core;
        protected string arrayName = "model";
        protected bool array = false;

        protected string HostName { get; set; } = "";
        protected string Path { get; set; } = "";
        protected Dictionary<string, string> Header { get; set; }

        public virtual void Dispatch(Param param)
        {
            Uri uri = new Uri(HostName + Path);
            if (param is IPathParameterizable)
            {
                uri = new Uri(uri, ((IPathParameterizable)param).QueryPath());
            }
            data = UnityWebRequest.Get(uri);
            Header?.ForEach(_ =>
           {
               data.SetRequestHeader(_.Key, _.Value);
           });
            core.Start(TaskScheduler.FromCurrentSynchronizationContext());
        }

        protected IObservable<Res> ResponseData()
        {
            return Observable.Create<Res>(_ =>
           {
               core = new Task(async () =>
               {
                   Debug.Log("calling api: " + data.url);

                   await data.SendWebRequest();

                   // 通信エラーチェック
                   if (data.isNetworkError)
                   {
                       Debug.Log(data.error);
                       _.OnError(new Exception(data.error));
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
                       Debug.Log(text);
                       _.OnNext(JsonUtility.FromJson<Res>(text));
                       _.OnCompleted();
                   }
               });
               return Disposable.Create(() => { });
           });
        }

        //BOM有無の判定
        private bool HasBomWithText(byte[] bytes)
        {
            return bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
        }
        //BOM消し
        private string GetDeletedBomText(string text)
        {
            return text.Remove(0, 1);
        }
    }
}