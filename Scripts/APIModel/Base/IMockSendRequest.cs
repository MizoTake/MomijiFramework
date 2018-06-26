using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
	public interface IMockSendRequest : ISendRequest { }

	public static class IMockSendRequestExtensions
	{
		static async Task Send (this IMockSendRequest send) => await SendAsync (send);

		private static async Task SendAsync (this IMockSendRequest send, bool array = false)
		{
#if UNITY_ANDROID || UNITY_IPHONE
			var path = send.Request.data.url;
#else
			// http://localhost を削除
			var path = send.Request.data.url.Remove (0, 16);
#endif
			Debug.Log ("reading json file: " + path);

#if UNITY_ANDROID
			WWW reader = new WWW (path);
			while (!reader.isDone) { }
			string text = reader.text;
			if (HasBomWithText (reader.bytes)) text = GetDeletedBomText (reader.text);
#else
			StreamReader reader = new StreamReader (path, Encoding.Default);
			await Task.Delay (1000);
			// UTF8文字列として取得する
			string text = reader.ReadToEnd ();
#endif

			await Task.Delay (1000);
			using (TextReader stream = new StringReader (text))
			{
				text = stream.ReadToEnd ();
				if (array)
				{
					// Responseで指定した配列で取得する(Default: model)
					text = "{ \"" + send.Request.arrayName + "\": " + text + "}";
				}
				Debug.Log ("json : " + text);
				send.Request.response = JsonUtility.FromJson<IResponsible> (text);
				send.Request.OnComplete ();
			}
		}

		//BOM有無の判定
		private static bool HasBomWithText (byte[] bytes)
		{
			return bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
		}
		//BOM消し
		private static string GetDeletedBomText (string text)
		{
			return text.Remove (0, 1);
		}
	}
}