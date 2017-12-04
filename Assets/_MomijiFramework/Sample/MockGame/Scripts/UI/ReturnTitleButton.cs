using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class ReturnTitleButton : MonoBehaviour
{

    [SerializeField]
    private Text _text;

    // Use this for initialization
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            TransSceneManager.ReloadScene();
        });

        var image = GetComponent<Image>();
        PlayGameSequence.State
            .Where(_ => _ == GameState.Game)
            .Subscribe(_ =>
            {
                image.DOFade(1, 0.3f).Play();
                _text.DOFade(1, 0.3f).Play();
            })
            .AddTo(this);

        PlayGameSequence.State
            .Where(_ => _ != GameState.Game)
            .Subscribe(_ =>
            {
                image.DOFade(0, 0.3f).Play();
                _text.DOFade(0, 0.3f).Play();
            })
            .AddTo(this);
    }
}
