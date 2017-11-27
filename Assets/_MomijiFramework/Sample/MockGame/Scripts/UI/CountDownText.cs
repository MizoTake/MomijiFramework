using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CountDownText : MonoBehaviour
{

    private Text _text;

    private int _count = 3;

    public Sequence CountDownStart;
    public Text ViewText => _text;

    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>();

        CountDownStart = DOTween.Sequence();
        CountDownStart
            .AppendInterval(5.0f);
        for (var i = _count; i > 0; i--)
        {
            CountDownStart
                .Append(_text.DOText(i.ToString(), 0.3f))
                .AppendInterval(1.0f);
        }
        CountDownStart
            .Append(_text.DOText("Start", 0.1f))
            .AppendInterval(1.0f);
    }
}
