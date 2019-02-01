using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Momiji.Sample
{
	public interface ISampleRequest
	{
		IObservable<SampleResponse> Get { get; }
	}

	public interface IListSampleRequest
	{
		IObservable<SampleResponse[]> Get { get; }
	}

	public class SampleRequest : GetRequestable<SampleParamter, SampleResponse>, ISampleRequest
	{
		public SampleRequest ()
		{
			HostName = "http://weather.livedoor.com/forecast/webservice/json/v1";
		}
	}
}