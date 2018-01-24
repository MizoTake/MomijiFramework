using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Enum<T> where T : struct, IConvertible
{
    /// <summary>
    /// Enumの要素数を取得する
    /// </summary>
    /// <returns>要素数</returns>
    public static int Count
    {
        get
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return Enum.GetNames(typeof(T)).Length;
        }
    }
}
