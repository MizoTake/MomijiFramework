using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momiji
{
    public static class VectorExtension
    {

        public static Vector3 EnumToNormalize(this Vector selectNor)
        {
            switch (selectNor)
            {
                case Vector.Up:
                    return Vector3.up;
                case Vector.Down:
                    return Vector3.down;
                case Vector.Left:
                    return Vector3.left;
                case Vector.Right:
                    return Vector3.right;
                case Vector.Forward:
                    return Vector3.forward;
                case Vector.Back:
                    return Vector3.back;
                default:
                    return Vector3.zero;
            }
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