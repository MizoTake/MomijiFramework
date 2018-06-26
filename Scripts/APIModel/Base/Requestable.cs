using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
    public abstract class Requestable
    {
        public UnityWebRequest data;
        public IResponsible response;
        public string arrayName = "model";
        public bool array = false;
        public string json;
        public string error;
        public Action<IResponsible> onComplete;
        public Action<string> onError;

        protected string HostName { get; }

        public void OnComplete ()
        {
            if (onComplete != null)
            {
                onComplete (response);
            }
        }

        public void OnError ()
        {
            if (onError != null)
            {
                onError (error);
            }
        }
    }
}