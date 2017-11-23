using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ranking : API<Ranking>
{

    public static void Get(int start, int last, System.Action<Response> onSuccess, System.Action<string> onError = null)
    {
        RequestData<Response> request = new RequestData<Response>(UnityWebRequest.Get(URL + "?start=" + start + "&" + "last=" + last));
        request.onComplete = onSuccess;
        request.onError = onError;
        Instance.Send<Response>(request);
    }

    public static void GetUuid(System.Action<UuidResponse> onSuccess, System.Action<string> onError = null)
    {
        RequestData<UuidResponse> request = new RequestData<UuidResponse>(UnityWebRequest.Get(URL + "?uuid=uuid"));
        request.onComplete = onSuccess;
        request.onError = onError;
        Instance.Send<UuidResponse>(request);
    }

    public static void GetLastRow(string uuid, System.Action<LastRowResponse> onSuccess, System.Action<string> onError = null)
    {
        RequestData<LastRowResponse> request = new RequestData<LastRowResponse>(UnityWebRequest.Get(URL + "?lastRow=lastRow&uuid=" + uuid));
        request.onComplete = onSuccess;
        request.onError = onError;
        Instance.Send<LastRowResponse>(request);
    }

    public static void Post(string name, string score, int start, int last, System.Action<Response> onSuccess = null, System.Action<string> onError = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("uuid", UserInfo.Uuid);
        form.AddField("name", name);
        form.AddField("score", score);
        form.AddField("start", start);
        form.AddField("last", last);
        RequestData<Response> request = new RequestData<Response>(UnityWebRequest.Post(URL, form));
        request.onComplete = onSuccess;
        request.onError = onError;
        Instance.Send<Response>(request);
    }
}

[System.Serializable]
public class Response
{
    public RankingData[] result;
}

[System.Serializable]
public class RankingData
{

    public string id;
    public string name;
    public string score;

}

[System.Serializable]
public class UuidResponse
{
    public string uuid;
}

[System.Serializable]
public class LastRowResponse
{
    public string lastRow;
    public string myRank;
}