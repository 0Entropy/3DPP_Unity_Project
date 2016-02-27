using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Due to the animation file cannot download from server, so Animator controller use local animation file.
/// </summary>
public class _PostureLocalAnimHandler : MonoBehaviour {

	public static Dictionary<string, int> poseTable;// = new Dictionary<string, int>();

	int curtPoseIndex 	= 0;
	int initPoseIndex 	= 0;
	int defaPoseIndex 	= 0;

	int  GetPostureData(_SourceItemData item){
		
        if(poseTable == null)
        {
            FillPostureTable();
        }
//		AnimationClipEx postureClip = null;
		if(!poseTable.ContainsKey(item.title)){
//			AssetBundle ab = AssetBundle.CreateFromFile(item.isTempLoaded ? item.TempLocalPath : item.DecorationLocalPath);
			
//			postureClip = ab.Load("anime") as AnimationClipEx;
//			
//			if(postureClip != null){
//				poseTable.Add(item, postureClip);
//				Debug.Log ("Clip is " + (postureClip.animationClip == null) +"?\n" + postureClip.ToString());
//			}else{
//				Debug.Log ( "AssetBundle Load \"anime\" is back NULL!!!");
//			}
//			
//			ab.Unload(false);

			Debug.LogError ("Local Pose Table donot contain " + item.title);
			return 0;
		}else{
            Debug.LogError("Local Pose Table contains " + item.title);
            return  poseTable[item.title];
		}
		return 0;
	}

	public void UpdatePosture(_SourceItemData item, bool toSelect, bool isInitial){
//		AnimationClipEx postureData = GetPostureData(item);

		int poseData = GetPostureData(item);
		
//		if(postureData == null){
//			Debug.Log ("The AnimationClipEx is NULL!!!");
//			return;
//		}
		
//		Debug.Log ("Clip is " + (postureData.animationClip == null) +"?\n" + postureData.ToString());
		
		///Show
		if(toSelect){
			//			Debug.Log ("Accessory Selected !");
			if(curtPoseIndex == poseData){
				//				Debug.Log ("There must be something wrong!");
				return;
			}else{
				if(isInitial){
					initPoseIndex = poseData;
				}
				curtPoseIndex = poseData;
				
			}
			
			///Hide
		}else{
			//			Debug.Log ("Accessory Reselesed !");
			if(curtPoseIndex == poseData){

				curtPoseIndex = initPoseIndex;

			}else{
				//				Debug.Log ("There must be something else wrong!");
			}
		}
		
//		UpdateAnimationClip(currentPoseClip);
		UpdatePosture(curtPoseIndex);
	}

	void UpdatePosture(int index){
		GetComponent<Animator>().SetInteger("pose_index", index);
	}

	public void BackTPose(){

	}

	public void SetCurPosture(bool toSelect){
		UpdatePosture(curtPoseIndex);
		
		if(toSelect){
			GetComponent<_StageAnimationHandler>().isAcceChanged = true;
		}
	}

	public void OnRelease(){
//		poseTable.Clear();
	}

	public void OnInit(){
		/*FillPostureTable();*/

	}

	void FillPostureTable(){
        // 		if(poseTable == null){
        // 			poseTable = new Dictionary<string, int>();
        // 		}else{
        // 			poseTable.Clear();
        // 		}
        poseTable = new Dictionary<string, int>();
        poseTable.Add ("StartHands", 0);
		poseTable.Add ("KanekiHandOne", 1);
		poseTable.Add ("KanekiHandTwo", 2);//KanekiHandTwo
		poseTable.Add ("KiseRyotaRightHand", 3);//KiseRyotaRightHand
		poseTable.Add ("KiseLeftTwo", 4);
		poseTable.Add ("KiseRightHandThree", 5);

	}
}
