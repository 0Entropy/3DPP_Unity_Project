using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PopWindow : MonoBehaviour {

	float showTime = 0;
	float showDuration = 2;

	/*public Text textValue;*/

    Action finishAction;

	public void OnShow(Action act){

        OnShow();
        /*textValue.text = value;*/
        finishAction = act;
        
		StartCoroutine(ShowTimer());
	}



	IEnumerator ShowTimer(){
		showTime = 0;
		while(showTime < showDuration){
			showTime += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
        if(finishAction != null)
        {
            finishAction();
        }
        OnHide();
		yield return null;
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
