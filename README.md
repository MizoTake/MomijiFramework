# Momiji Framework

## Developed Unity2018.x

Require Unity's Scripting Runtime Vertion .Net 4.x Equivalent

## Use Assets

- [UniRx](https://github.com/neuecc/UniRx)
- [Utf8Json](https://github.com/neuecc/Utf8Json)
- [DoTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
- [Zenject](https://github.com/svermeulen/Zenject)

## Setup

```
git submodule add https://github.com/MizoTake/MomijiFramework.git
```

## Web API Client

# [WIP] Swagger Codegen for Momiji

[swagger-codegen-momiji](https://github.com/MizoTake/swagger-codegen-momiji)

# Usage Example

Sample で叩いてる WebAPI: [livedoor Weather Hacks](http://weather.livedoor.com/weather_hacks/webservice)

上記の WebAPI を叩くのに必要なもの

[SampleParamter.cs](https://github.com/MizoTake/MomijiFramework/blob/master/Example/SampleRequest/Scripts/Sample/SampleParamter.cs)

```csharp:SampleParamter.cs
public class SampleParamter : IPathParameterizable
{
	public int city;

	public SampleParamter (int city)
	{
		this.city = city;
	}

	public string QueryPath ()
	{
		return this.CreatePath (nameof (city) + "=" + city.ToString ());
	}
}
```

[SampleRequest.cs](https://github.com/MizoTake/MomijiFramework/blob/master/Example/SampleRequest/Scripts/Sample/SampleRequest.cs)

```csharp:SampleRequest.cs
interface ISampleRequest
{
	IObservable<SampleResponse> Get;
}

public class SampleRequest : GetRequestable<SampleParamter, SampleResponse>, ISampleRequest
{
	public SampleRequest ()
	{
		HostName = "http://weather.livedoor.com/forecast/webservice/json/v1";
	}
}
```

ルート配列対応時

[SampleMockRequest.cs](https://github.com/MizoTake/MomijiFramework/blob/master/Example/SampleRequest/Scripts/Sample/SampleMockRequest.cs)

```csharp:SampleMockRequest.cs
public class SampleMockRequest : MockRequestable<SampleParamter, SampleResponse[]>, IListSampleRequest
    {
        public SampleMockRequest ()
        {
            Path = "sample.json";
        }
        public IObservable<SampleResponse[]> Get => MockResponseData ();
    }
```

[SampleResponse.cs](https://github.com/MizoTake/MomijiFramework/blob/master/Example/SampleRequest/Scripts/Sample/SampleResponse.cs)

```csharp:SampleParamter.cs
public class SampleResponse  : IResponsible
{
	public string title;
}
```

Requester は実際に API を叩いて結果を得る実装

[Requester.cs](https://github.com/MizoTake/MomijiFramework/blob/master/Example/SampleRequest/Scripts/Requester.cs)

```csharp:SampleParamter.cs
public class Requester : MonoBehaviour
{

	void Start ()
	{
		var request = new SampleRequest ();

		request.Get
			.Subscribe (x =>
			{
				Debug.Log (x.title);
			})
			.AddTo (this);

		//root array response
		mockRequest.Get
			.Subscribe (x =>
			{
				x.ForEach (xx => Debug.Log (xx.title));
			})
			.AddTo (this);

		/ ..省略.. /

		var param = new SampleParamter (city: 130010);

		request.Dispatch (param);

		mockRequest.Dispatch (param);

		/ ..省略.. /
	}
}
```

Request クラスで実装した API メソッドに合うメソッドを購読

Dispach で API を叩きにいく処理

## License

MIT
