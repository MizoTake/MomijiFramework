using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momiji
{
    public static class Vector3Extension
    {

        public static Vector3 RandomRange(this Vector3 vec, float min, float max)
        {
            var value = Random.Range(min, max);
            return new Vector3(value, value, value);
        }
    }
}