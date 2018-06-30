using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Momiji.Sample
{
	interface ISampleRequest
	{
		IObservable<SampleResponse> Get (SampleParamter param);
	}

	public class SampleGetRequest : GetRequestable<SampleParamter, SampleResponse>, ISampleRequest
	{
		public SampleGetRequest ()
		{
			HostName = "http://weather.livedoor.com/forecast/webservice/json/v1";
		}
	}
}