using UnityEngine;
using System.Collections;

/// <summary>
/// _ stage animation handler.
/// 
/// </summary>
/// 
/// 

public class _StageAnimationHandler : MonoBehaviour {

	float idleTime = 0;
	float durationTime = 10;

	int[] idleIndecies = new int[]{1, 4, 5};
	
	int[] acceIndecies = new int[]{2, 3};

	public bool isAcceChanged {set; get;}


	
	int animIndex{
		set{
			GetComponent<Animator>().SetInteger("anim_index", value);
		}

		get{
			return GetComponent<Animator>().GetInteger("anim_index");
		}
	}

	public void StageAnimFinishEvent(int index){
		Debug.Log ("Stage " + index + " is finished.");
//		GetComponent<Animator>().SetInteger("anim_index", 0);
		animIndex = 0;
		idleTime = 0;
		isAcceChanged = false;
	}


//	void PlayIdleAnim(){
//		int randow = Random.Range(0, 3);
//		Debug.Log ("randow : " + randow + "; Play Idle Anim " + idleIndecies[randow]);
//		GetComponent<Animator>().SetInteger("anim_index", idleIndecies[randow]);
//	}

//	void PlayAcceAnim(){
////		int randow = Random.Range(0, 2);
//		int index = acceIndecies[Random.Range(0, 2)];
////		Debug.Log ("randow : " + randow + "; Play Acce Anim " + index);
////		GetComponent<Animator>().SetInteger("anim_index", index);
//	}

	// Use this for initialization
	void Start () {

//		StageAnimFinishEvent(1);
	}

	void FixedUpdate(){

/*		Debug.Log ("Animator'Index = " + animIndex);*/

		if(animIndex != 0)
			return;

		if(_UIController.Instance.IsAcceView && isAcceChanged){
			int randow = Random.Range(0, 2);
			Debug.Log ("randow : " + randow + "; Play Idle Anim " +  acceIndecies[randow]);
			animIndex = acceIndecies[randow];
			isAcceChanged = false;
			return;
		}

		if(_UIController.Instance.IsAcceView && idleTime > durationTime){
			int randow = Random.Range(0, 3);
			Debug.Log ("randow : " + randow + "; Play Idle Anim " + idleIndecies[randow]);
			animIndex = idleIndecies[randow];
			idleTime = 0;
		}

		idleTime += Time.deltaTime;

	}

	void HandleOnUpdateAccessory (_SourceItemData item, bool toSelect, bool isPlayable)
	{
		if(toSelect)
			isAcceChanged = true;
//			animIndex =  acceIndecies[Random.Range(0, 2)];
//			PlayAcceAnim();
		
	}

	void OnEnable(){
		//		_UIController.OnUpdateView += HandleOnUpdateView;
        _UIController.OnUpdateAccessory += HandleOnUpdateAccessory;
	}

	void OnDisable(){
//		_UIController.OnUpdateView -= HandleOnUpdateView;
        _UIController.OnUpdateAccessory -= HandleOnUpdateAccessory;
	}
}
