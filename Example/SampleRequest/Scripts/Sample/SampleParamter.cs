using System.Collections;
using System.Collections.Generic;
using Momiji;
using UnityEngine;

namespace Momiji.Sample
{
	public struct SampleParamter : IPathParameterizable
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
}