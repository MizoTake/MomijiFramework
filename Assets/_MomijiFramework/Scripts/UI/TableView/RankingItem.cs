using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class RankingItem : MonoBehaviour {

	[SerializeField]
	private Text _rank;
	[SerializeField]
	private Text _name;
	[SerializeField]
	private Text _score;
	private Image _panel;
	private Sequence _flashing;

	public void SetItem(string rank, string name, string score, bool myScore) {
		if(_flashing != null) _flashing.Kill();
		_panel = GetComponent<Image>();
		_flashing = DOTween.Sequence();
		_flashing
			.Append(DOTween.To(() => _panel.color,
								_ => _panel.color = _,
								new Color(0, 1, 0, 0.5f),
								0.6f))
			.Append(DOTween.To(() => _panel.color,
								_ => _panel.color = _,
								new Color(1, 1, 1, 0.5f),
								0.6f))
			.SetLoops(-1);
		_rank.text = rank;
		_name.text = name;
		_score.text = score;
		if(myScore){
			_panel.color = Color.green;
			//_panel.ObserveEveryValueChanged(c => _panel.color)
			//	.TimeInterval(Timestamped.Create(Time.deltaTime));
			_flashing.Play();
		}
		_panel.color = new Color(1, 1, 1, 0.5f);
	}
}
