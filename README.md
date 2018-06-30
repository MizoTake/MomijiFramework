# Momiji Framework

## Developed Unity2018.x

## Use Assets
- UniRx
- DoTween
- Zenject

## Setup

```
git submodule add https://github.com/MizoTake/MomijiFramework.git
```

## Web API Client

Sampleで叩いてるWebAPI: [livedoor Weather Hacks](http://weather.livedoor.com/weather_hacks/webservice)

上記のWebAPIを叩くのに必要なもの

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
		IObservable<SampleResponse> Get (SampleParamter param);
	}

	public class SampleRequest : GetRequestable<SampleParamter, SampleResponse>, ISampleRequest
	{
		public SampleRequest ()
		{
			HostName = "http://weather.livedoor.com/forecast/webservice/json/v1";
		}
	}
```

[SampleResponse.cs](https://github.com/MizoTake/MomijiFramework/blob/master/Example/SampleRequest/Scripts/Sample/SampleResponse.cs)
```csharp:SampleParamter.cs
    public class SampleResponse  : IResponsible
	{
		public string title;
	}
```

Requesterは実際にAPIを叩いて結果を得る実装

[Requester.cs](https://github.com/MizoTake/MomijiFramework/blob/master/Example/SampleRequest/Scripts/Requester.cs)
```csharp:SampleParamter.cs
    public class Requester : MonoBehaviour
    {

        void Start ()
        {
            var param = new SampleParamter (city: 130010);
            var request = new SampleRequest ();

            request.Get (param)
                .Subscribe (_ =>
                {
                    Debug.Log (_.title);
                })
                .AddTo (this);

            / ..省略.. /

            request.Dispatch ();
            
            / ..省略.. /
        }
    }
```

Requestクラスで実装したAPIメソッドに合うメソッドを購読
DispachでAPIを叩きにいく処理

## License
MIT
