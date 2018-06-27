using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Momiji.Sample
{
	public class SampleMockRequest : MockRequestable, IMockSendRequest<SampleParamter, SampleResponse>, ISampleRequest
	{
		public Requestable Request => this;
		public IObservable<SampleResponse> Get (SampleParamter param) => this.Send ();
	}
}