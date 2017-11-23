using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

public class RankingItemPool : ObjectPool<RankingItem> {

	private readonly RankingItem _prefab;
	private readonly Transform _parent;
	
	public RankingItemPool(RankingItem prefab, Transform parent) {
		_parent = parent;
		_prefab = prefab;
	}

	protected override RankingItem CreateInstance() {
		var newObj = GameObject.Instantiate(_prefab);
		newObj.transform.SetParent(_parent);
		newObj.transform.localScale = Vector3.one;
		return newObj;
	}
}
