using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BasePopupView : MonoBehaviour
{

    private const float ANIM_TIME = 0.3f;
    [SerializeField]
    protected Text _message;
    void Awake()
    {
        transform.DOScale(0.0f, 0.0f).Play();
    }

    public void Dismiss()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(0.0f, ANIM_TIME))
            .AppendCallback(() => SelectButton(PopupSelect.Dismiss))
            .Restart();
    }

    protected void ShowProcess(string message, PopupSelect select)
    {
        _message.text = message;
        DOTween.Sequence()
            .Append(transform.DOScale(1.0f, ANIM_TIME))
            .AppendCallback(() => SelectButton(select, true))
            .Restart();
    }

    public virtual void SelectButton(PopupSelect select, bool active = false) { }

}

public enum PopupSelect
{
    Dismiss,
    Single,
    Double
}