using System;
using UnityEngine.Networking;

namespace Momiji
{
	public abstract class DeleteRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable where Res : IResponsible
	{
		public IObservable<Res> Delete ()
		{
			Uri uri = new Uri (HostName + Path);
			data = UnityWebRequest.Delete (uri);
			Header?.ForEach (_ =>
			{
				data.SetRequestHeader (_.Key, _.Value);
			});
			return this.ResponseData ();
		}
	}
}