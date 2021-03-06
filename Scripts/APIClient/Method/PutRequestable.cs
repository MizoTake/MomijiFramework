﻿using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
	public abstract class PutRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable where Res : IResponsible
	{
		protected override UnityWebRequest UpdateRequest (Param param)
		{
			var body = System.Text.Encoding.UTF8.GetBytes ("{}");
			Uri uri = new Uri (HostName + Path);
			if (param is IPathParameterizable)
			{
				uri = new Uri (uri, ((IPathParameterizable) param).QueryPath ());
			}
			if (param is IBodyParameterizable)
			{

			}
			var data = UnityWebRequest.Put (uri, body);
			Header?.ForEach (x =>
			{
				data.SetRequestHeader (x.Key, x.Value);
			});
			return data;
		}

		public IObservable<Res> Put => this.ResponseData ();
	}
}