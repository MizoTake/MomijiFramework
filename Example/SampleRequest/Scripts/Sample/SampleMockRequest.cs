using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Momiji.Sample
{
	public class SampleMockRequest : MockRequestable<SampleParamter, SampleResponse>, ISampleRequest
	{
		public SampleMockRequest ()
		{
			Path = "sample.json";
		}
		public IObservable<SampleResponse> Get (SampleParamter param) => this.MockResponseData ();
	}

	public class SampleErrorRequest : ErrorRequestable<SampleParamter, SampleResponse>, ISampleRequest
	{
		public IObservable<SampleResponse> Get (SampleParamter param) => this.ErrorResponseData ();
	}
}