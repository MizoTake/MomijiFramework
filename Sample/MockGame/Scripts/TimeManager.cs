using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using Momiji;

namespace Momiji
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField]
        private CountDownText _countDownText;

        private float _timer = 0.0f;

        public static float Timer => Instance._timer;

        // Use this for initialization
        void Start()
        {
            Observable
                .EveryUpdate()
                .Where(_ => PlayGameSequence.State.Value == GameState.Game && _countDownText.CountDownStart.IsComplete())
                .Subscribe(_ =>
                {
                    _timer += Time.deltaTime;
                    _countDownText.ViewText.text = "Time: " + Mathf.Round(_timer).ToString();
                })
                .AddTo(this);
        }
    }
}