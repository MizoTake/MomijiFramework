using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class RxGesture : MonoBehaviour
{
    [System.Serializable]
    public class ClickEvent : UnityEngine.Events.UnityEvent { }
    [SerializeField]
    private ClickEvent _click = new ClickEvent();
    [System.Serializable]
    public class LfetFlickEvent : UnityEngine.Events.UnityEvent { }
    [SerializeField]
    private LfetFlickEvent _leftFlick = new LfetFlickEvent();
    [System.Serializable]
    public class RightFlickEvent : UnityEngine.Events.UnityEvent { }
    [SerializeField]
    private RightFlickEvent _rightFlick = new RightFlickEvent();
    [System.Serializable]
    public class DownFlickEvent : UnityEngine.Events.UnityEvent { }
    [SerializeField]
    private DownFlickEvent _downFlick = new DownFlickEvent();
    [System.Serializable]
    public class UpFlick : UnityEngine.Events.UnityEvent { }
    [SerializeField]
    private UpFlick _up = new UpFlick();

    void Awake()
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
                        _up.Invoke();
                    }
                }
            });
    }
}
