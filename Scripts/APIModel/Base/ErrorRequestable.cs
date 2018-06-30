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
		public IObservable<Res> ErrorResponseData ()
		{
			return Observable.Create<Res> (_ =>
			{
				core = new Task (() =>
				{
					_.OnError (new Exception ("Mock Error"));
				});

				return Disposable.Create (() => { });
			});
		}
	}
}