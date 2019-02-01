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

		request.Get
			.Subscribe (x =>
			{
				Debug.Log (x.title);
			})
			.AddTo (this);

		var mockRequest = new SampleMockRequest ();

		mockRequest.Get
			.Subscribe (x =>
			{
				Debug.Log (x.title);
			})
			.AddTo (this);

		var mockListRequest = new SampleListMockRequest ();

		mockListRequest.Get
			.Subscribe (x =>
			{
				x.ForEach (xx => Debug.Log (xx.title));
			})
			.AddTo (this);

		var errorRequest = new SampleErrorRequest ();

		errorRequest.Get
			.Subscribe (x =>
			{
				Debug.Log (x.title);
			})
			.AddTo (this);

		var param = new SampleParamter (city: 130010);

		request.Dispatch (param);
		mockRequest.Dispatch (param);
		mockListRequest.Dispatch (param);
		errorRequest.Dispatch (param);
	}
}