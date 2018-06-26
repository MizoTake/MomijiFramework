using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Momiji
{
	public class SampleMockRequest : MockRequestable, IMockSendRequest
	{
		public Requestable Request => this;
		public bool ArrayResponse => this.ArrayResponse;
		public Task Send () => this.Send ();
	}
}