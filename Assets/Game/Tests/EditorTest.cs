using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert; // AssertはNUnitのではなくUnityのものを使う
using Mine.Mono;
using System.Reflection;
using UnityEngine;

namespace Mine
{
    public class EditorTest
    {
        float maxDist;
        float maxForce;

        [SetUp]
        public void Setup()
        {
            Singleton.Init();
            maxDist = Singleton.Instance.Params.neighborDistance;
            maxForce = Singleton.Instance.Params.wallReflectionForce;
        }

        [Test]
        public void IsForceClamped()
        {
            float farForce = Boid.CalculateReflectionForce(maxDist);
            float nearForce = Boid.CalculateReflectionForce(0);
            Assert.IsTrue(NearlyEqual(farForce, 0, 1));
            Assert.IsTrue(NearlyEqual(nearForce, maxForce, 1));
        }

        [Test]
        public void IsDistanceCurveOK()
        {
            float dist1 = Mathf.Lerp(0, maxDist, 0.4f);
            float dist2 = Mathf.Lerp(0, maxDist, 0.5f);
            Assert.IsTrue(Boid.CalculateReflectionForce(dist1) > Boid.CalculateReflectionForce(dist2));
        }

        [Test]
        public void IsOuterBoundOK()
        {
            float far = maxDist * 10;
            float negative = -5;
            float farForce = Boid.CalculateReflectionForce(far);
            Assert.IsTrue(NearlyEqual(farForce, 0, 1));

            float nearforce = Boid.CalculateReflectionForce(negative);
            Assert.IsTrue(nearforce > maxForce || NearlyEqual(nearforce, maxForce, 1));
        }

        [Test]
        public void IsNearlyEqualWorks()
        {
            Assert.IsTrue(NearlyEqual(1f, 0.99f, 1));
            Assert.IsTrue(NearlyEqual(0f, 0.01f, 1));
        }

        static bool NearlyEqual(float a, float b, int decimalPlace)
        {
            float factor = Mathf.Pow(10, decimalPlace);
            return Mathf.RoundToInt(a * factor) == Mathf.RoundToInt(b * factor);
        }
    }
}
