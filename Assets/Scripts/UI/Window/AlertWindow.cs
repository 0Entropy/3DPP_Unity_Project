using UnityEngine;
using UnityEngine.UI;
using System;

public class AlertWindow : MonoBehaviour {

//     public InputField inputName;
//     public PopWindow popWindow;
//     public AlertWindow alertWindow;

    Action finishAction;

    public void HandleOnConfirm()
    {
        OnHide();
        /*_Hero.Instance.SaveToFactory(() => ToNext(), true);*/
        _Hero.Instance.SaveToFactory(finishAction, true);
    }

    public void HandleOnCancle()
    {
        OnHide();
        /*ToNext();*/
        if (finishAction != null)
        {
            finishAction();
        }
    }

    public void OnShow(Action act)
    {
        finishAction = act;
        OnShow();
    }

    void OnShow()
    {
        _UIController.Instance.BlockTouch();
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        _UIController.Instance.UnblockTouch();
        gameObject.SetActive(false);

    }
}
