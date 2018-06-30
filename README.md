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

Sample Web API: [livedoor Weather Hacks](http://weather.livedoor.com/weather_hacks/webservice)

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

## License
MIT
