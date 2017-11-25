using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class RxTouch : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var start = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Select(_ => Input.mousePosition);
        var end = this.UpdateAsObservable()
            // 一定時間以内にUpされないと検出しない
            .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(1)))
            .Where(_ => Input.GetMouseButtonUp(0))
            .Select(_ => Input.mousePosition)
            .Take(1);

        start.SelectMany(startPos => end.Select(endPos => startPos - endPos))
            .Subscribe(v =>
            {
                // 一定距離以上動かさないとクリック扱い
                if (v.magnitude < 50)
                {
                    Debug.Log("Click");
                }
                else if (Math.Abs(v.x) > Mathf.Abs(v.y))
                {
                    if (v.x > 0)
                    {
                        Debug.Log("Left");
                    }
                    else
                    {
                        Debug.Log("Right");
                    }
                }
                else
                {
                    if (v.y > 0)
                    {
                        Debug.Log("Down");
                    }
                    else
                    {
                        Debug.Log("Up");
                    }
                }
            });
    }
}
