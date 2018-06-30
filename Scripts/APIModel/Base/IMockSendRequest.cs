// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using UniRx;
// using UnityEngine;
// using UnityEngine.Networking;

// namespace Momiji
// {
// 	public interface IMockSendRequest<Param, Res> : ISendRequest<Param, Res> where Param : IParameterizable where Res : IResponsible { }

// 	public static class IMockSendRequestExtensions
// 	{
// 		public static IObservable<Res> Send<Param, Res> (this IMockSendRequest<IParameterizable, IResponsible> send) where Param : IParameterizable where Res : IResponsible => SendAsync<Param, Res> (send);

// 		private static IObservable<Res> SendAsync<Param, Res> (this IMockSendRequest<IParameterizable, IResponsible> send) where Param : IParameterizable where Res : IResponsible
// 		{
// 			return Observable.Create<Res> (_ =>
// 			{
// 				var result = MockAPIRequest<Param, Res> (send).Result;
// 				_.OnNext (result);
// 				_.OnCompleted ();
// 				// TODO: Error処理も出来るようにいずれ拡張
// 				return Disposable.Create (() => { });
// 			});
// 		}

// 		static async Task<Res> MockAPIRequest<Param, Res> (this ISendRequest<IParameterizable, IResponsible> send) where Param : IParameterizable where Res : IResponsible
// 		{
// 			var result = default (Res);
// #if UNITY_ANDROID || UNITY_IPHONE
// 			var path = send.Request.data.url;
// #else
// 			// http://localhost を削除
// 			var path = send.Request.data.url.Remove (0, 16);
// #endif
// 			Debug.Log ("reading json file: " + path);

// #if UNITY_ANDROID
// 			WWW reader = new WWW (path);
// 			while (!reader.isDone) { }
// 			string text = reader.text;
// 			if (HasBomWithText (reader.bytes)) text = GetDeletedBomText (reader.text);
// #else
// 			StreamReader reader = new StreamReader (path, Encoding.Default);
// 			// UTF8文字列として取得する
// 			string text = reader.ReadToEnd ();
// #endif

// 			using (TextReader stream = new StringReader (text))
// 			{
// 				text = stream.ReadToEnd ();
// 				if (send.Request.array)
// 				{
// 					// Responseで指定した配列で取得する(Default: model)
// 					text = "{ \"" + send.Request.arrayName + "\": " + text + "}";
// 				}
// 				Debug.Log ("json : " + text);
// 				result = JsonUtility.FromJson<Res> (text);
// 				return result;
// 			}
// 		}

// 		//BOM有無の判定
// 		private static bool HasBomWithText (byte[] bytes)
// 		{
// 			return bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
// 		}
// 		//BOM消し
// 		private static string GetDeletedBomText (string text)
// 		{
// 			return text.Remove (0, 1);
// 		}
// 	}
// }