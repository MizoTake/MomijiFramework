using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Momiji.Sample
{
    namespace Tests
    {
        public class TestGetRequest
        {
            ISampleRequest requester;

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
                // requester.Get
                //     .Subscribe (x =>
                //     {
                //         Debug.Log (x.title);
                //     })
                //     .AddTo (this);
                Assert.AreEqual (2, 1 + 1);
            }

            [UnityTest]
            public IEnumerator EnumeratorPasses ()
            {
                yield return null;
                Assert.AreEqual (2, 1 + 1);
            }
        }
    }
}