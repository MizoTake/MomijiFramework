using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Momiji
{
	interface ISampleRequest
	{
		void Get ();
	}

	public class SampleGetRequest : Requestable, ISampleRequest, ISendRequest
	{
		public Requestable Request => this;
		public void Get () => this.Send ();
	}
}