using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

public class Pool<T> : ObjectPool<T> where T : UnityEngine.Component
{

    protected readonly T _prefab;
    protected readonly Transform _parent;

    public Pool(T prefab, Transform parent)
    {
        _parent = parent;
        _prefab = prefab;
    }

    protected override T CreateInstance()
    {
        var newObj = GameObject.Instantiate(_prefab);
        newObj.transform.SetParent(_parent);
        newObj.transform.localScale = Vector3.one;
        return newObj;
    }
}