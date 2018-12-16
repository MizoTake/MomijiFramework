using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (!typeof (T).IsEnum)
                throw new ArgumentException ("T must be an enumerated type");

            return Enum.GetNames (typeof (T)).Length;
        }
    }

    /// <summary>
    /// Enumの要素を１つ乱数で取得する
    /// </summary>
    /// <returns>要素</returns>
    public static T Random
    {
        get
        {
            if (!typeof (T).IsEnum)
                throw new ArgumentException ("T must be an enumerated type");

            // return Enum.GetNames(typeof(T)).Length;
            var rand = UnityEngine.Random.Range (0, Enum.GetNames (typeof (T)).Length);
            return (T) Enum.ToObject (typeof (T), rand);
        }
    }
}

public static class EnumExtensions
{
    /// <summary>
    /// Enumの要素をForEachで取得して処理
    /// </summary>
    public static void ForEach<T> (this Enum value, Action<T> action)
    {
        for (var i = 0; i < Enum.GetNames (typeof (T)).Length; i++)
        {
            action ((T) Enum.ToObject (typeof (T), i));
        }
    }
}