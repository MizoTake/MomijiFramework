using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ScrollbarEnabled : MonoBehaviour {

	[SerializeField]
	private TableView _table;
	private Scrollbar bar;
	private bool call = true;

	// Use this for initialization
	void Start () {
		bar = gameObject.GetComponent<Scrollbar>();
		bar.value = 1;
	}

	void Update() {
		if(bar.value == 0) {
			if(!call) return;
			_table?.PagingGet();
			call = false;
		} else {
			call = true;
		}
	}
	
	void OnEnable() {
		if(!bar) bar = gameObject.GetComponent<Scrollbar>();
        bar.value = 1;
    }
}
