// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using UniRx;
// using UnityEngine;

// namespace Momiji
// {
// 	public interface ISendRequest<Param, Res> where Param : IParameterizable where Res : IResponsible
// 	{
// 		Requestable Request { get; }
// 	}

// 	public static class ISendRequestExtensions
// 	{
// 		public static IObservable<Res> Send<Param, Res> (this ISendRequest<Param, Res> send) where Param : IParameterizable where Res : IResponsible => SendAsync<Param, Res> (send);

// 		static IObservable<Res> SendAsync<Param, Res> (this ISendRequest<Param, Res> send) where Param : IParameterizable where Res : IResponsible
// 		{
// 			return Observable.Create<Res> (_ =>
// 			{
// 				var result = APIRequest<Param, Res> (send).Result;
// 				if (send.Request.data.isNetworkError)
// 				{
// 					_.OnNext (result);
// 					_.OnCompleted ();
// 				}
// 				else
// 				{
// 					_.OnError (new Exception (send.Request.error));
// 				}
// 				return Disposable.Create (() => { });
// 			});
// 		}

// 		static async Task<Res> APIRequest<Param, Res> (this ISendRequest<Param, Res> send) where Param : IParameterizable where Res : IResponsible
// 		{
// 			var result = default (Res);

// 			Debug.Log ("calling api: " + send.Request.data.url);
// 			await send.Request.data.SendWebRequest ();

// 			// 通信エラーチェック
// 			if (send.Request.data.isNetworkError)
// 			{
// 				send.Request.error = send.Request.data.error;
// 				Debug.Log (send.Request.data.error);
// 			}
// 			else
// 			{
// 				// UTF8文字列として取得する
// 				string text = send.Request.data.downloadHandler.text;
// 				if (send.Request.array)
// 				{
// 					// Responseで指定した配列で取得する(Default: model)
// 					text = "{ \"" + send.Request.arrayName + "\": " + text + "}";
// 				}
// 				send.Request.json = text;
// 				result = JsonUtility.FromJson<Res> (text);
// 				Debug.Log (text);
// 			}
// 			return result;
// 		}
// 	}
// }