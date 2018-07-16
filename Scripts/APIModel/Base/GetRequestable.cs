using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
	public abstract class GetRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable where Res : IResponsible
	{

		protected override UnityWebRequest UpdateRequest (Param param)
		{
			Uri uri = new Uri (HostName + Path);
			if (param is IPathParameterizable)
			{
				uri = new Uri (uri, ((IPathParameterizable) param).QueryPath ());
			}
			var data = UnityWebRequest.Get (uri);
			Header?.ForEach (_ =>
			{
				data.SetRequestHeader (_.Key, _.Value);
			});
			return data;
		}

		public IObservable<Res> Get => this.ResponseData ();
	}
}