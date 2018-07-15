using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
	public abstract class DeleteRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable where Res : IResponsible
	{
		protected override void UpdateRequest (Param param)
		{
			Uri uri = new Uri (HostName + Path);
			if (param is IPathParameterizable)
			{
				uri = new Uri (uri, ((IPathParameterizable) param).QueryPath ());
			}
			data = UnityWebRequest.Put (uri, JsonUtility.ToJson (param));
			Header?.ForEach (_ =>
			{
				data.SetRequestHeader (_.Key, _.Value);
			});
		}

		public IObservable<Res> Delete () => this.ResponseData ();
	}
}