using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Momiji
{
	public abstract class ErrorRequestable<Param, Res> : Requestable<Param, Res> where Param : IParameterizable where Res : IResponsible
	{

		public new void Dispatch (Param param) => notify.OnError (new Exception ("Mock Error"));
		public IObservable<Res> ErrorResponseData ()
		{
			return ResponseData ();
		}
	}
}