using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace Momiji
{
    public class Popup : BasePopupView
    {

        [SerializeField]
        private GameObject _single;
        [SerializeField]
        private GameObject _double;
        [SerializeField]
        private Button _singleButton;
        [SerializeField]
        private Button _leftButton;
        [SerializeField]
        private Button _rightButton;
        [SerializeField]
        private InputField _field;

        public bool InputFieldEnabled { set { _field.enabled = value; } }

        void Start()
        {
            SelectButton(PopupSelect.Dismiss);

            _leftButton.interactable = false;
            _field.onValueChange
                .AsObservable()
                .Select(_ => _.Length != 0)
                .Subscribe(_ =>
                {
                    _leftButton.interactable = _;
                })
                .AddTo(this);
        }

        public void Show(string message, System.Action single)
        {
            ShowProcess(message, PopupSelect.Single);
            _singleButton.onClick.AsObservable()
                .Take(1)
                .Subscribe(_ => single())
                .AddTo(this);
        }

        public void Show(string message, System.Action leftAction, System.Action rightAction)
        {
            ShowProcess(message, PopupSelect.Double);
            _leftButton.onClick.AsObservable()
                .Take(1)
                .Subscribe(_ => leftAction())
                .AddTo(this);
            _rightButton.onClick.AsObservable()
                .Take(1)
                .Subscribe(_ => rightAction())
                .AddTo(this);
        }

        public override void SelectButton(PopupSelect select, bool active = false)
        {
            switch (select)
            {
                case PopupSelect.Dismiss:
                    _single.SetActive(active);
                    _double.SetActive(active);
                    break;
                case PopupSelect.Single:
                    _single.SetActive(active);
                    break;
                case PopupSelect.Double:
                    _double.SetActive(active);
                    break;
            }
        }
    }
}