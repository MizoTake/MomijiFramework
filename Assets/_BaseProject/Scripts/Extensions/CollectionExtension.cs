using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リストの拡張メソッド
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// コレクション内に要素があるか
    /// </summary>
    /// <returns><c>true</c> if is any the specified collection; otherwise, <c>false</c>.</returns>
    /// <param name="collection">Collection.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static bool IsAny<T>(this IEnumerable<T> collection)
    {
        return (collection != null && collection.Any());
    }

    /// <summary>
    /// 簡易foreach
    /// </summary>
    /// <param name="collection">Collection.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (T t in collection)
        {
            action(t);
        }
    }

    /// <summary>
    /// 簡易foreach(index付き)
    /// </summary>
    /// <param name="collection">Collection.</param>
    /// <param name="action">Action.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
    {
        int i = 0;
        foreach (T t in collection)
        {
            action(t, i++);
        }
    }

    /// <summary>
    /// 配列の中身をシャッフル
    /// </summary>
    /// <param name="collection">Collection.</param>
    /// <returns>Collection Shuffle</returns>
    public static T[] ShuffleArray<T>(this IEnumerable<T> collection)
    {
        var array = collection.ToArray();
        for (int t = 0; t < array.Length; t++)
        {
            var tmp = array[t];
            int r = UnityEngine.Random.Range(t, array.Length);
            array[t] = array[r];
            array[r] = tmp;
        }
        return array;
    }

    /// <summary>
    /// 配列の中身をシャッフル
    /// </summary>
    /// <param name="collection">Collection.</param>
    /// <returns>Collection Shuffle</returns>
    public static List<T> ShuffleList<T>(this IEnumerable<T> collection)
    {
        var array = collection.ToList();
        for (int t = 0; t < array.Count; t++)
        {
            var tmp = array[t];
            int r = UnityEngine.Random.Range(t, array.Count);
            array[t] = array[r];
            array[r] = tmp;
        }
        return array;
    }

    /// <summary>
    /// 配列の中身をシャッフルしてForeach
    /// </summary>
    /// <param name="collection">Collection.</param>
    /// <param name="action">Action.</param>
    public static void ShuffleForeach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        var array = collection.ToArray();
        for (int t = 0; t < array.Length; t++)
        {
            var tmp = array[t];
            int r = UnityEngine.Random.Range(t, array.Length);
            array[t] = array[r];
            array[r] = tmp;
        }
        array.ForEach(_ => action(_));
    }

    /// <summary>
    /// 配列の中身をシャッフルしてForeach(index付き)
    /// </summary>
    /// <param name="collection">Collection.</param>
    /// <param name="action">Action.</param>
    public static void ShuffleForeach<T>(this IEnumerable<T> collection, Action<T, int> action)
    {
        var array = collection.ToArray();
        for (int t = 0; t < array.Length; t++)
        {
            var tmp = array[t];
            int r = UnityEngine.Random.Range(t, array.Length);
            array[t] = array[r];
            array[r] = tmp;
        }
        array.ForEach((_, i) => action(_, i));
    }

    /// <summary>
    /// 配列からRandomに１つ値を取得する
    /// </summary>
    /// <param name="collection">Collection.</param>
    /// <returns>Random Value.</returns>
    public static T RandomValue<T>(this IEnumerable<T> collection)
    {
        var array = collection.ToArray();
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
}
