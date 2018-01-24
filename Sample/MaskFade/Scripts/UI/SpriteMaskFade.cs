using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

public class SpriteMaskFade : MonoBehaviour
{

    [SerializeField]
    private Transform _view;
    [SerializeField]
    private SpriteMask _mask;

    // Use this for initialization
    void Start()
    {
        var maskFade = DOTween.Sequence()
            .Append(_mask.transform.DOScale((_view.localScale.x > _view.localScale.y) ? _view.localScale.x : _view.localScale.y, 1.0f))
            .AppendInterval(1.0f)
            .Append(_mask.transform.DOScale(Vector3.zero, 1.0f))
            .SetEase(Ease.Flash);

        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0)
                        && !maskFade.IsPlaying())
            .Select(_ => Input.mousePosition)
            .Subscribe(_ =>
            {
                _.z = 10.0f;
                _mask.transform.position = Camera.main.ScreenToWorldPoint(_);
                maskFade.Restart();
            })
            .AddTo(this);
    }
}
