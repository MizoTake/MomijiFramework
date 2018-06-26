using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Momiji
{
	public interface ISendRequest
	{
		Requestable Request { get; }
		bool ArrayResponse { get; }
		Task Send ();
	}

	public static class ISendRequestExtensions
	{
		static async Task Send (this ISendRequest send) => await SendAsync (send);

		static bool ArrayResponse { get; } = false;

		static async Task SendAsync (this ISendRequest send, bool array = false)
		{
			Debug.Log ("calling api: " + send.Request.data.url);
			await send.Request.data.SendWebRequest ();

			// 通信エラーチェック
			if (send.Request.data.isNetworkError)
			{
				send.Request.error = send.Request.data.error;
				send.Request.OnError ();
				Debug.Log (send.Request.data.error);
			}
			else
			{
				// UTF8文字列として取得する
				string text = send.Request.data.downloadHandler.text;
				if (array)
				{
					// Responseで指定した配列で取得する(Default: model)
					text = "{ \"" + send.Request.arrayName + "\": " + text + "}";
				}
				send.Request.json = text;
				send.Request.response = JsonUtility.FromJson<IResponsible> (text);
				send.Request.OnComplete ();
				Debug.Log (text);
			}
		}
	}
}