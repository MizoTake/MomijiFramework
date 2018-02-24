using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// リクエストデータ
/// </summary>
public class RequestData<T>
{
    /// <summary>
    /// リクエスト
    /// </summary>
    public UnityWebRequest request;

    /// <summary>
    /// レスポンス
    /// </summary>
    public T response;

    public string json;

    public string error;

    /// <summary>
    /// レスポンス受け取り通知
    /// </summary>
    public Action<T> onComplete;

    /// <summary>
    /// エラー通知
    /// </summary>
    public Action<string> onError;

    public RequestData(UnityWebRequest request)
    {
        this.request = request;
    }

    public void OnComplete()
    {
        if (onComplete != null)
        {
            onComplete(response);
        }
    }

    public void OnError()
    {
        if (onError != null)
        {
            onError(error);
        }
    }
}
