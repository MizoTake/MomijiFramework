using System;
using UnityEngine.Networking;

namespace Momiji
{
	public abstract class PostRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable where Res : IResponsible
	{
		public IObservable<Res> Post ()
		{
			Uri uri = new Uri (HostName + Path);
			data = UnityWebRequest.Post (uri, "");
			Header?.ForEach (_ =>
			{
				data.SetRequestHeader (_.Key, _.Value);
			});
			return this.ResponseData ();
		}
	}
}