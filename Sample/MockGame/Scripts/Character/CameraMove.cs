using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace Momiji
{
    public class CameraMove : MonoBehaviour
    {

        private const float OFFSET = 200.0f;

        // Use this for initialization
        void Start()
        {
            // PlayGameSequence.State
            //     .Where(_ => _ == GameState.Game)
            //     .Subscribe(_ =>
            //     {
            //         DOTween.Sequence()
            //             .AppendInterval(1.0f)
            //             .Append(transform.DOMoveY(1, 5.0f))
            //             .Play();
            //     })
            //     .AddTo(this);
        }
    }
}