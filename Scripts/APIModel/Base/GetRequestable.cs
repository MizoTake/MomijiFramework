using System;
using UnityEngine.Networking;

namespace Momiji
{
	public abstract class GetRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable where Res : IResponsible
	{
		public IObservable<Res> Get ()
		{
			return this.ResponseData ();
		}
	}
}