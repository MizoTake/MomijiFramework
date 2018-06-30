using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Momiji.Sample
{
	public class SampleMockRequest : Requestable<SampleParamter, SampleResponse>, ISampleRequest
	{
		public IObservable<SampleResponse> Get (SampleParamter param) => this.SendAsync ();
	}
}