using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IndicatorView : MonoBehaviour {

	[SerializeField]
	private Image _image;
	[SerializeField]
	private Image _backPanel;

	private bool _loading = false;
	private Sequence _show;
	private Sequence _dismiss;

	public Sequence Show => _show;
	public Sequence Dismiss => _dismiss;

	// Use this for initialization
	void Awake () {
		_show = DOTween.Sequence();
		_dismiss = DOTween.Sequence();

		_show
			.AppendCallback(() => _loading = true)
			.AppendCallback(() => StartCoroutine("LoadingRot"))
			.Append(DOTween.ToAlpha(
				() => _image.color,
				color => _image.color = color,
				1.0f,
				0.3f
			))
			.Join(DOTween.ToAlpha(
				() => _backPanel.color,
				color => _backPanel.color = color,
				0.65f,
				0.3f
			))
			.SetAutoKill(false);

		_dismiss
			.Append(DOTween.ToAlpha(
				() => _image.color,
				color => _image.color = color,
				0.0f,
				0.3f
			))
			.Join(DOTween.ToAlpha(
				() => _backPanel.color,
				color => _backPanel.color = color,
				0.0f,
				0.3f
			))
			.AppendCallback(() => _loading = false)
			.SetAutoKill(false);

		_show.Restart();
	}

	private IEnumerator LoadingRot() {
		var time = 0;
		var speed = 5.0f;
		while(_loading) {
			_image.transform.Rotate(Vector3.forward * speed);
			yield return new WaitForSeconds(0.01f);
			time += 1;
			if(time % 30 == 0) {
				speed = Random.Range(3.0f, 10.0f);
			}
		}
	}
	
}
