using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momiji
{
    public static class Vector3Extension
    {

        public static Vector3 RandomRange (this Vector3 vec, float min, float max)
        {
            var value = Random.Range (min, max);
            return new Vector3 (value, value, value);
        }

        public static Vector3 RandomX (this Vector3 vec, float min, float max)
        {
            var value = Random.Range (min, max);
            return new Vector3 (value, vec.y, vec.z);
        }
        public static Vector3 RandomY (this Vector3 vec, float min, float max)
        {
            var value = Random.Range (min, max);
            return new Vector3 (vec.x, value, vec.z);
        }
        public static Vector3 RandomZ (this Vector3 vec, float min, float max)
        {
            var value = Random.Range (min, max);
            return new Vector3 (vec.x, vec.y, value);
        }
    }
}