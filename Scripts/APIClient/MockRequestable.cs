using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Momiji.Sample;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
    public abstract class MockRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable
    {
        private readonly string DIRECTORY_NAME = "/MockAPI/";

        public new string HostName
        {
            get
            {
#if UNITY_IPHONE
                return Application.dataPath + "/Raw" + DIRECTORY_NAME;
#elif UNITY_ANDROID
                return "jar:file://" + Application.dataPath + "!/assets" + DIRECTORY_NAME;
#else
                return Application.dataPath + "/MomijiFramework/StreamingAssets" + DIRECTORY_NAME;
#endif
            }
        }

        public new void Dispatch (Param param)
        {
            var path = HostName + Path;
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
                Debug.Log ("json : " + text);
                notify.OnNext (JsonUtility.FromJson<Res> (text));
                notify.OnCompleted ();
            }
        }

        public IObservable<Res> MockResponseData ()
        {
            return ResponseData ();
        }
    }
}