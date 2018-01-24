using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using Momiji;

public class PlayGameSequence : Singleton<PlayGameSequence>
{

    [SerializeField]
    private Transform _view;
    [SerializeField]
    private SpriteMask _mask;
    [SerializeField]
    private CountDownText _countDownText;
    [SerializeField]
    private TitleUI _title;
    [SerializeField]
    private GameObject _playerObject;

    private PlayerCharacter _character;
    private ReactiveProperty<GameState> _state = new ReactiveProperty<GameState>(GameState.Title);

    public static ReactiveProperty<GameState> State => Instance._state;

    // Use this for initialization
    void Start()
    {
        var timeRectTransform = _countDownText.ViewText.transform.GetComponent<RectTransform>();

        var dismiss = DOTween.Sequence()
            .Append(_mask.transform.DOScale(2.0f * ((_view.localScale.x > _view.localScale.y) ? _view.localScale.x : _view.localScale.y), 1.0f))
            .SetEase(Ease.Flash);

        var close = DOTween.Sequence()
            .Append(_mask.transform.DOScale(Vector3.zero, 1.0f))
            .SetEase(Ease.Flash);

        Observable
            .EveryUpdate()
            .Where(_ => _state.Value == GameState.Title)
            .Where(_ => Input.GetMouseButtonDown(0) && !dismiss.IsPlaying())
            .Take(1)
            .Select(_ => Input.mousePosition)
            .Subscribe(_ =>
            {
                _.z = 10.0f;
                _mask.transform.position = Camera.main.ScreenToWorldPoint(_);
                _title.Dismiss
                    .Append(dismiss)
                    .Restart();
                CreateStage.CreateStageStart.OnNext(Unit.Default);
                _state.Value = GameState.Game;
            })
            .AddTo(this);

        CreateStage.FinishCreateStage
            .Where(_ => _)
            .Subscribe(_ =>
            {
                _character = Instantiate(_playerObject, new Vector3(4, 100, 0), Quaternion.identity).GetComponent<PlayerCharacter>();
                _countDownText.CountDownStart.Play();
            })
            .AddTo(this);

        Observable
            .EveryUpdate()
            .Where(_ => _state.Value == GameState.Game && _countDownText.CountDownStart.IsComplete())
            .Take(1)
            .Subscribe(_ =>
            {
                CreateStage.RemoveRigidBody();
                PlayerCharacter.RemoveRigidBody();
            })
            .AddTo(this);

        Observable
            .EveryUpdate()
                .Where(_ => _state.Value == GameState.Game && _character?.BlockPosY == 0)
                .Subscribe(_ =>
                {
                    _state.Value = GameState.Result;
                })
                .AddTo(this);

        _state
            .Where(_ => _ == GameState.Result)
            .Subscribe(_ =>
            {
                _countDownText.ViewText.text += " sec\n\nTouch to Title";
                timeRectTransform.DOMoveY(Screen.height / 2.0f, 1.0f).Play();
                close.Restart();
            })
            .AddTo(this);

        Observable
            .EveryUpdate()
                .Where(_ => _state.Value == GameState.Result && Input.GetMouseButtonDown(0) && close.IsComplete())
                .Subscribe(_ =>
                {
                    DOTween.Sequence()
                        .Append(_countDownText.ViewText.DOFade(0, 0.3f))
                        .AppendInterval(0.5f)
                        .AppendCallback(() => TransSceneManager.ReloadScene())
                        .Play();
                })
                .AddTo(this);
    }
}

public enum GameState
{
    Title,
    Game,
    Result
}