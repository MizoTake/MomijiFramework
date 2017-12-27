using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Momiji
{
    public class FadeView : Singleton<FadeView>
    {

        private const float ANIM_TIME = 1.0f;
        private Sequence _show;
        private Sequence _dismiss;
        private Sequence _flow;
        private bool _fading = false;

        public static bool Fading { get { return Instance._fading; } }
        public static Sequence Show { get { return Instance._show; } }
        public static Sequence Dismiss { get { return Instance._dismiss; } }

        void Awake()
        {
            var canvasObj = new GameObject("FadeCanvas");
            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;
            transform.SetParent(canvas.transform);
            DontDestroyOnLoad(transform.parent);

            var panel = gameObject.AddComponent<Image>();
            panel.color = new Color(1, 1, 1, 0);
            var rect = panel.transform.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero;
            _show = DOTween.Sequence();
            _dismiss = DOTween.Sequence();

            _show
                .AppendCallback(() => _fading = true)
                .Append(DOTween.ToAlpha(() => panel.color,
                                        _ => panel.color = _,
                                        1.0f,
                                        ANIM_TIME))
                .AppendCallback(() => _fading = false)
                .SetAutoKill(false);

            _dismiss
                .AppendCallback(() => _fading = true)
                .Append(DOTween.ToAlpha(() => panel.color,
                                        _ => panel.color = _,
                                        0.0f,
                                        ANIM_TIME))
                .AppendCallback(() => _fading = false)
                .SetAutoKill(false);
        }

        public static void ShowPlay()
        {
            Instance._show.Restart();
        }

        public static void DismissPlay()
        {
            Instance._dismiss.Restart();
        }

        public static void Play(float interval = 0.0f)
        {
            DOTween.Sequence()
                .Append(Show)
                .AppendInterval(interval)
                .Append(Dismiss)
                .Restart();
        }
    }
}
