using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Momiji.Sample
{
    public class SampleListMockRequest : MockRequestable<SampleParamter, SampleResponse[]>, IListSampleRequest
    {
        public SampleListMockRequest ()
        {
            Path = "ListResponse.json";
        }
        public IObservable<SampleResponse[]> Get => MockResponseData ();
    }

    public class SampleMockRequest : MockRequestable<SampleParamter, SampleResponse>, ISampleRequest
    {
        public SampleMockRequest ()
        {
            Path = "Response.json";
        }
        public IObservable<SampleResponse> Get => MockResponseData ();
    }

    public class SampleErrorRequest : ErrorRequestable<SampleParamter, SampleResponse>, ISampleRequest
    {
        public IObservable<SampleResponse> Get => ErrorResponseData ();
    }
}