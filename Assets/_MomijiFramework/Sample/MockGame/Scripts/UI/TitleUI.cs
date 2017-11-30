using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleUI : MonoBehaviour
{

    [SerializeField]
    private Text _title;
    [SerializeField]
    private Text _touchToStart;

    public Sequence Dismiss;

    // Use this for initialization
    void Start()
    {
        _title.DOFade(1, 0.3f).Play();
        _touchToStart.DOFade(1, 0.3f).Play();

        Dismiss = DOTween.Sequence();
        var blink = DOTween.Sequence()
            .Append(_touchToStart.DOFade(0, 0.8f))
            .Append(_touchToStart.DOFade(1, 0.8f))
            .SetLoops(-1)
            .Play();

        Dismiss
            .AppendCallback(() => blink.Kill())
            .Append(_title.DOFade(0, 0.3f))
            .Join(_touchToStart.DOFade(0, 0.3f));
    }
}
