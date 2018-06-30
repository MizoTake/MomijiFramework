using System.Collections;
using System.Collections.Generic;
using Momiji.Sample;
using UniRx;
using UnityEngine;

public class Requester : MonoBehaviour
{

	void Start ()
	{
		var param = new SampleParamter (city: 130010);
		var request = new SampleRequest ();

		request.Get (param)
			.Subscribe (_ =>
			{
				Debug.Log (_.title);
			})
			.AddTo (this);

		var mockRequest = new SampleMockRequest ();

		mockRequest.Get (param)
			.Subscribe (_ =>
			{
				Debug.Log (_.title);
			})
			.AddTo (this);

		var errorRequest = new SampleErrorRequest ();

		errorRequest.Get (param)
			.Subscribe (_ =>
			{
				Debug.Log (_.title);
			})
			.AddTo (this);

		request.Dispatch ();
		mockRequest.Dispatch ();
		errorRequest.Dispatch ();
	}
}