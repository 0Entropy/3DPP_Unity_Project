using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class LoadingView : Singleton<LoadingView> {

	public Image bgColor;

    public Image imageAnima;

    float curIndex = 0;
//    int maxStartIndex = 15;
    int size = 17;

	public bool IsShown{set; get;}

	bool isPause = false;
//	bool isFinished = false;
//	bool isStop = false;

	public Action finishAction;
	public Action endAction;
//	public List<Action> finishActions = new List<Action>();

//	Color black = new Color(0, 0, 0, 0.6F);

	float customDuration = 9;
	float actualTime = 0;

	float pauseTime = 6;

	float flexTime = 0.2f;
//	Step curStep;

	void OnEnable(){
//		PlatformBridge.OnLoadFinish += HandleOnLoadFinish;
	}

	void OnDisable(){
//		PlatformBridge.OnLoadFinish -= HandleOnLoadFinish;
	}

	void HandleOnLoadFinish (int result)
	{
		if(result == 1 ){
			if(finishAction != null)
				finishAction();
			endAction = null;
		}else{
			if(endAction != null)
				endAction();
			finishAction = null;
		}

		OnStop();
	}

	void SetBGColor(Color color){
		bgColor.color = color;
	}

	public void OnStart( Color color, float duration = 10,  Action finishAct = null, Action endAct = null){


		gameObject.SetActive(true);
		IsShown = true;

		SetBGColor(color);

		customDuration = duration;
		if(customDuration < 2)
			customDuration = 2;

		finishAction = finishAct;
		endAction = endAct;

		curIndex = 0;
		actualTime = 0;
		
		isPause = false;
//		isFinished = false;



		StartCoroutine("LoadTimer");


//		StartCoroutine("LoadTimer");
	}

	public void OnFinish(){
//		if(!IsShown){
//			OnStop();
//			return;
//		}
		Debug.LogError("LoadView Sucess to Finished!!!");
		HandleOnLoadFinish(1);
	}

	public void OnEnd(){
//		if(!IsShown){
//			OnStop();
//			return;
//		}
		Debug.LogError("LoadView Failed to End!!!");
		HandleOnLoadFinish(0);
	}

	void OnStop(){
		finishAction = null;
		endAction = null;
		IsShown = false;
		StopCoroutine("LoadTimer");
		gameObject.SetActive(false);
	}

    


//    void  LoadingFinish()
//    {
//        Debug.Log(curStep + isPause.ToString());
//        if( curStep == Step.wait)
//        {
//            changeStep(Step.finish);
//        }
//		else
//        {
//            isPause = true;
//        }
//    }

	IEnumerator LoadTimer(){

		while(actualTime < customDuration){
			actualTime += Time.deltaTime;
//			Debug.Log ("actual Time : " + actualTime);
//			if(curIndex != (int)(actualTime * 10)){
//				curIndex = (int)(actualTime * 10) % size;
//				imageAnima.sprite = loadSprite(curIndex);
//			}

			imageAnima.sprite = loadSprite((int)curIndex);
			if(!isPause){
				if(actualTime > pauseTime)
					isPause = true;
				curIndex += 0.1f;
				//			curIndex %= size;
				if(curIndex > size - 3)
					curIndex = size - 3;
			}else{
				curIndex += 0.2f;
				//			curIndex %= size;
				if(curIndex > size - 1)
					curIndex = size - 1;
			}

//			if(isFinished){
//				if(finishAction != null)
//					finishAction();
//				OnStop();
//			}

			yield return new WaitForFixedUpdate();
		}

//		OnStop();
		OnEnd ();

		yield return null;
	}

//    void NextStatu()
//    {
//        Debug.Log("nextStatu");
//
////        isStop = true;
//
//        if (finishAction != null) finishAction();
//
//		OnHide();
//        //DestorySelf();
//    }


//    void  changeStep( Step  newStep)
//    {
//        switch (newStep)
//        {
//            case Step.start:
//                //InvokeRepeating("StartAnimation", 0, flexTime);
//                break;
//            case Step.wait:
//                waitStatu();
//                break;
//            case Step.finish:
//                InvokeRepeating("finishAnimation", 0, flexTime);
//                break;
//            case Step.next:
//                NextStatu();
//                break;
//        }
//    }


//    void StartAnimation()
//    { 
//        Sprite sprite = loadSprite(curIndex.ToString().PadLeft(2, '0'));
//
//        imageAnima.sprite = sprite;
//
//        curIndex++;
//
//        if (curIndex > maxStartIndex)
//        {
//            Debug.Log("-----");
//            CancelInvoke("StartAnimation");
//            changeStep(Step.wait);
//        }
//    }

//    void  waitStatu()
//    {
//        if (isPause)
//        {
//            changeStep(Step.finish);
//        }else
//        {
//            // nothing
//            Debug.Log("nothing----");
//
//            curStep = Step.wait;
//        }
//    }


    Sprite loadSprite(int index) 
    {
        //Debug.Log(spriteName);
		return Resources.Load<Sprite>(string.Format("LoadViewSprites/loading_{0:00}", index));//"ItemPrefab/Loading/loading_" + spriteName).GetComponent<SpriteRenderer>().sprite;
    }

//    void  finishAnimation()
//    {
//        Sprite sprite = loadSprite(curIndex.ToString().PadLeft(2, '0'));
//
//        imageAnima.sprite = sprite;
//
//        curIndex++;
//
//        if (curIndex >= maxIndex)
//        {
//            CancelInvoke("finishAnimation");
//            changeStep(Step.next);
//        }
//    }




//    public enum  Step
//    {
//        start,
//        wait,
//        finish,
//        next
//    }
}
