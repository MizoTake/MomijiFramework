using System;
using UnityEngine.Networking;

namespace Momiji
{
	public abstract class PutRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable where Res : IResponsible
	{
		public IObservable<Res> Put ()
		{
			Uri uri = new Uri (HostName + Path);
			data = UnityWebRequest.Put (uri, "");
			Header?.ForEach (_ =>
			{
				data.SetRequestHeader (_.Key, _.Value);
			});
			return this.ResponseData ();
		}
	}
}