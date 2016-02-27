using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _PostureAnimationHandler : MonoBehaviour {

	public AnimationClipEx currentPoseClip{set; get;}
	public AnimationClipEx initialPoseClip{set; get;}
	public AnimationClipEx defaultPoseClip{set; get;}

	AnimatorOverrideController overrideConttroller;

	Dictionary<_SourceItemData, AnimationClipEx> poseTable;
	
	AnimationClipEx GetPostureData(_SourceItemData item){

		AnimationClipEx postureClip = null;
		if(!poseTable.ContainsKey(item)){
			AssetBundle ab = AssetBundle.CreateFromFile(item.isTempLoaded ? item.TempLocalPath : item.DecorationLocalPath);

			postureClip = ab.Load("anime") as AnimationClipEx;

			if(postureClip != null){
				poseTable.Add(item, postureClip);
				Debug.Log ("Clip is " + (postureClip.animationClip == null) +"?\n" + postureClip.ToString());
			}else{
				Debug.Log ( "AssetBundle Load \"anime\" is back NULL!!!");
			}

			ab.Unload(false);
		}else{
			postureClip = poseTable[item];
		}
		return postureClip;
	}
	
	public void UpdatePosture(_SourceItemData item, bool toSelect, bool isInitial){
		
		AnimationClipEx postureData = GetPostureData(item);
		
		if(postureData == null){
			Debug.Log ("The AnimationClipEx is NULL!!!");
			return;
		}

		Debug.Log ("Clip is " + (postureData.animationClip == null) +"?\n" + postureData.ToString());

		///Show
		if(toSelect){
//			Debug.Log ("Accessory Selected !");
			if(postureData == currentPoseClip){
//				Debug.Log ("There must be something wrong!");
				return;
			}else{

				if(isInitial){
					initialPoseClip = postureData;
//					Debug.Log (item.title + "Set Posture to default");
				}

				currentPoseClip = postureData;
				
			}
			
		///Hide
		}else{
//			Debug.Log ("Accessory Reselesed !");
			if(postureData == currentPoseClip){
				currentPoseClip = initialPoseClip;
			}else{
//				Debug.Log ("There must be something else wrong!");
			}
		}

//		UpdateAnimationClip(currentPoseClip);
	}

	public void OnInit(){

		GetComponent<Animator>().runtimeAnimatorController = overrideConttroller;
		UpdateAnimationClip(defaultPoseClip);

	}

	public void OnRelease(){

	}

	public void SetCurPosture(bool toSelect){
		UpdateAnimationClip(currentPoseClip);

		if(toSelect){
			GetComponent<_StageAnimationHandler>().isAcceChanged = true;
		}
	}

	void UpdateAnimationClip(AnimationClipEx clipEx){

		if(GetComponent<Animator>() == null)
			return;

		if(clipEx == null || clipEx.animationClip == null)
			clipEx = defaultPoseClip;

		overrideConttroller["postureAnim"] = clipEx;

		Debug.Log("------------------ After ------------------");
		LogOverrideController();

	}

	void LogOverrideController(){
		AnimationClipPair[] clipPair = overrideConttroller.clips;

		Debug.Log ("ClipPair's Length :" + clipPair.Length);
		
		int i=0;
		while(i<clipPair.Length){
			if(clipPair[i].originalClip != null)
				Debug.Log (string.Format("Original Clip {0} : {1}", i, clipPair[i].originalClip.name));
			if(clipPair[i].overrideClip != null)
				Debug.Log (string.Format("Override Clip {0} : {1}",  i, clipPair[i].overrideClip.name));
			i++;
		}

	}



	void initialControllor(){

		poseTable = new Dictionary<_SourceItemData, AnimationClipEx>();

		AnimationClip clip = Resources.Load("PostureAnimation/postureAnim") as AnimationClip;
		AnimationClipEx clipEx = ScriptableObject.CreateInstance<AnimationClipEx>();
		clipEx.SetAnimationClip(clip);
		initialPoseClip = defaultPoseClip = clipEx;

//		avatorAnimator = GetComponent<Animator>();
		
		RuntimeAnimatorController runtimeController = GetComponent<Animator>().runtimeAnimatorController;
		overrideConttroller = new AnimatorOverrideController();
		overrideConttroller.name = "Zero Override Animator";
		overrideConttroller.runtimeAnimatorController = runtimeController;

//		avatorAnimator.runtimeAnimatorController = overrideConttroller;

//		UpdateAnimationClip(defaultPoseClip);

	}

	void Awake(){
		initialControllor();
	}
}
