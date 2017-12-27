using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Momiji
{
    public class RxGesture : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.Events.UnityEvent _click = new UnityEngine.Events.UnityEvent();
        [SerializeField]
        private UnityEngine.Events.UnityEvent _leftFlick = new UnityEngine.Events.UnityEvent();
        [SerializeField]
        private UnityEngine.Events.UnityEvent _rightFlick = new UnityEngine.Events.UnityEvent();
        [SerializeField]
        private UnityEngine.Events.UnityEvent _downFlick = new UnityEngine.Events.UnityEvent();
        [SerializeField]
        private UnityEngine.Events.UnityEvent _upFlick = new UnityEngine.Events.UnityEvent();

        public UnityEngine.Events.UnityAction Click { set { _click.AddListener(() => value()); } }
        public UnityEngine.Events.UnityAction LeftFlick { set { _leftFlick.AddListener(() => value()); } }
        public UnityEngine.Events.UnityAction RightFlick { set { _rightFlick.AddListener(() => value()); } }
        public UnityEngine.Events.UnityAction DownFlick { set { _downFlick.AddListener(() => value()); } }
        public UnityEngine.Events.UnityAction UpFlick { set { _upFlick.AddListener(() => value()); } }

        public override void OnStateBegin()
        {
            var start = this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Select(_ => Input.mousePosition);
            var end = this.UpdateAsObservable()
                .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(1)))
                .Where(_ => Input.GetMouseButtonUp(0))
                .Select(_ => Input.mousePosition)
                .Take(1);

            start.SelectMany(startPos => end.Select(endPos => startPos - endPos))
                .Subscribe(v =>
                {
                    if (v.magnitude < 50)
                    {
                        _click.Invoke();
                    }
                    else if (Math.Abs(v.x) > Mathf.Abs(v.y))
                    {
                        if (v.x > 0)
                        {
                            _leftFlick.Invoke();
                        }
                        else
                        {
                            _rightFlick.Invoke();
                        }
                    }
                    else
                    {
                        if (v.y > 0)
                        {
                            _downFlick.Invoke();
                        }
                        else
                        {
                            _upFlick.Invoke();
                        }
                    }
                })
                .AddTo(this);
        }
    }
}