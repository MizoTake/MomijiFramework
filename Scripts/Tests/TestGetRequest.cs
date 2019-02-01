using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Momiji.Sample
{
    namespace Tests
    {
        public partial class TestGetRequest
        {
            SampleRequest requester;
            SampleMockRequest mockRequester;
            SampleListMockRequest mockListRequester;
            SampleErrorRequest errorRequester;
            CompositeDisposable dispose = new CompositeDisposable ();

            ~TestGetRequest ()
            {
                Dispose ();
            }

            [SetUp]
            public void SetUp ()
            {
                requester = new SampleRequest ();
                mockRequester = new SampleMockRequest ();
                mockListRequester = new SampleListMockRequest ();
                errorRequester = new SampleErrorRequest ();
            }

            [TearDown]
            public void TearDown ()
            {

            }

            [Test]
            public void WebRequest ()
            {
                requester.Get
                    .Subscribe (x =>
                    {
                        Debug.Log (x.title);
                        Assert.AreEqual (x.title, "東京都 東京 の天気");
                    })
                    .AddTo (dispose);

                var param = new SampleParamter (city: 130010);
                requester.Dispatch (param);
            }

            [Test]
            public void MockRequest ()
            {
                mockRequester.Get
                    .Subscribe (x =>
                    {
                        Debug.Log (x.title);
                        Assert.AreEqual (x.title, "Test Response");
                    })
                    .AddTo (dispose);

                var param = new SampleParamter (city: 130010);
                mockRequester.Dispatch (param);
            }

            [Test]
            public void MockListRequest ()
            {
                mockListRequester.Get
                    .Subscribe (x =>
                    {
                        x.ForEach (xx =>
                            Debug.Log (xx.title)
                        );
                        Assert.AreEqual (x[0].title, "Array index zero");
                        Assert.AreEqual (x[1].title, "Array index one");
                    })
                    .AddTo (dispose);

                var param = new SampleParamter (city: 130010);
                mockListRequester.Dispatch (param);
            }

            [Test]
            public void ErrorRequest ()
            {
                errorRequester.Get
                    .Subscribe (onNext: x =>
                        {
                            Debug.Log (x.title);
                            // エラーに通るはずがないため
                            Assert.IsTrue (false);
                        },
                        onError : x =>
                        {
                            Assert.IsTrue (x is System.Exception);
                        })
                    .AddTo (dispose);

                var param = new SampleParamter (city: 130010);
                errorRequester.Dispatch (param);
            }
        }

        public partial class TestGetRequest : System.IDisposable
        {
            public void Dispose ()
            {
                dispose.Clear ();
            }
        }
    }
}