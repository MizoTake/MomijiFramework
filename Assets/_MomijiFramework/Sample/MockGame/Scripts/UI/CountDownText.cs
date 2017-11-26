using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CountDownText : MonoBehaviour
{

    private Text _text;

    public Sequence CountDownStart;
    public Text ViewText => _text;

    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>();

        CountDownStart = DOTween.Sequence();
        CountDownStart
            .AppendInterval(3.0f)
            .AppendCallback(() => _text.text = "3")
            .AppendInterval(1.0f)
            .AppendCallback(() => _text.text = "2")
            .AppendInterval(1.0f)
            .AppendCallback(() => _text.text = "1")
            .AppendInterval(1.0f)
            .AppendCallback(() => _text.text = "Start");
    }
}
