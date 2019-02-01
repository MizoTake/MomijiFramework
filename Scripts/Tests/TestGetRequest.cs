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
            CompositeDisposable dispose = new CompositeDisposable ();

            ~TestGetRequest ()
            {
                Dispose ();
            }

            [SetUp]
            public void SetUp ()
            {
                requester = new SampleRequest ();
            }

            [TearDown]
            public void TearDown ()
            {

            }

            [Test]
            public void SimplePasses ()
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