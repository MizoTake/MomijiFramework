using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momiji
{
    public static class VectorExtension
    {
        public static Vector3 EnumToNormalize(this Vector selectNor, bool random = false)
        {
            var count = 1;
            var result = Vector3.zero;
            var normalizes = new List<Vector> { selectNor };
            if (random)
            {
                count = Random.Range(1, 4);
            }
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    normalizes.Add(Enum<Vector>.Random);
                }
                switch (normalizes[i])
                {
                    case Vector.Up:
                        result += Vector3.up;
                        break;
                    case Vector.Down:
                        result += Vector3.down;
                        break;
                    case Vector.Left:
                        result += Vector3.left;
                        break;
                    case Vector.Right:
                        result += Vector3.right;
                        break;
                    case Vector.Forward:
                        result += Vector3.forward;
                        break;
                    case Vector.Back:
                        result += Vector3.back;
                        break;
                    default:
                        result += Vector3.zero;
                        break;
                }
            }
            return result;
        }

    }

    public enum Vector
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back
    }
}