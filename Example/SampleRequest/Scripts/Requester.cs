using System.Collections;
using System.Collections.Generic;
using Momiji.Sample;
using UniRx;
using UnityEngine;

public class Requester : MonoBehaviour
{

	void Start ()
	{
		var request = new SampleRequest ();

		request.Get ()
			.Subscribe (_ =>
			{
				Debug.Log (_.title);
			})
			.AddTo (this);

		var mockRequest = new SampleMockRequest ();

		mockRequest.Get ()
			.Subscribe (_ =>
			{
				Debug.Log (_.title);
			})
			.AddTo (this);

		var errorRequest = new SampleErrorRequest ();

		errorRequest.Get ()
			.Subscribe (_ =>
			{
				Debug.Log (_.title);
			})
			.AddTo (this);

		var param = new SampleParamter (city: 130010);

		request.Dispatch (param);
		request.Dispatch (param);
		request.Dispatch (param);
		mockRequest.Dispatch (param);
		errorRequest.Dispatch (param);
	}
}