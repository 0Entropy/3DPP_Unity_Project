using UnityEngine;
using System.Collections;

public class _TestPostureController : MonoBehaviour {

	Animator mAnimator;

	// Use this for initialization
	void Start () {
	
		mAnimator = GetComponent<Animator>();

		RuntimeAnimatorController runtimeController = mAnimator.runtimeAnimatorController;
		AnimatorOverrideController overrideConttroller = new AnimatorOverrideController();
		overrideConttroller.name = "Zero Override Animator";
		overrideConttroller.runtimeAnimatorController = runtimeController;

		AnimationClipPair[] clipPair = overrideConttroller.clips;

		Debug.Log ("ClipPair's Length :" + clipPair.Length);

		int i=0;
		while(i<clipPair.Length){
			if(clipPair[i].originalClip != null)
				Debug.Log ("Original Clip " + i + " : " + clipPair[i].originalClip.name);
			if(clipPair[i].overrideClip != null)
				Debug.Log ("Override Clip " + i + " : " + clipPair[i].overrideClip.name);
			i++;
		}

		#region Edit runtime animation clip
//		Transform UpperArm_L = mAnimator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
		//overrideConttroller["T_Pose"] = ???;
//		AnimationCurve curve = AnimationCurve.EaseInOut(0, UpperArm_L.localPosition.x, 1, 12);
//		curve.AddKey(new Keyframe(0, -45));
//		curve.AddKey(new Keyframe(1, 45));

//		AnimationClip clip = new AnimationClip();

//		clip.name = "Take 001 Override";
//		clip.wrapMode = WrapMode.Loop;
//		clip.add
//		clip.SetCurve(GetRelativePath(UpperArm_L), typeof(Transform), "localPosition.x", curve);
//		clip.SetCurve("actual avator/Root_M/BackA_M/Back_B_M/CHest_M.Scapula_L/Shoulder_L", typeof(Transform), "localPosition.x", curve);
//		clip.SetCurve("actual avator/Root_M/BackA_M/Back_B_M/CHest_M.Scapula_L/Shoulder_L", typeof(Transform), "localPosition.y", curve);
//		clip.SetCurve("actual avator/Root_M/BackA_M/Back_B_M/CHest_M.Scapula_L/Shoulder_L", typeof(Transform), "localPosition.z", curve);

		AnimationClip clip = Resources.Load("Take 001") as AnimationClip;

		overrideConttroller["T_Pose"] = clip;

		mAnimator.runtimeAnimatorController = overrideConttroller;


//		AnimatorStateInfo layerInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
//		mAnimator.Play (layerInfo.nameHash);
////
//		mAnimator.Update(0);


		#endregion


		Avatar mAvatar = mAnimator.avatar;

		Debug.Log (mAvatar.name + " is Human? : \n" + mAvatar.isHuman);
		Debug.Log (mAnimator.name + " is Human? : \n" + mAnimator.isHuman);
		Transform hipTrans = mAnimator.GetBoneTransform(HumanBodyBones.Neck);
		Debug.Log (hipTrans.position + ", " + hipTrans.eulerAngles);
//		Debug.Log ("--- bone ---");
//		string[] boneName = HumanTrait.BoneName;
//		int i = 0;
//		while (i < HumanTrait.BoneCount) {
//			Debug.Log(boneName[i]);
//			i++;
//		}
//		
//		Debug.Log ("--- muscle ---");
//		string[] muscleName = HumanTrait.MuscleName;
//		i = 0;
//		while (i < HumanTrait.MuscleCount) {
//			Debug.Log(muscleName[i]);
//			i++;
//		}
	}

	string GetRelativePath (  Transform tras  ){
		if ( tras != null )
		{
			string objectRelativePath = tras.name;
			Transform curObject = tras.transform;
			while ( curObject.parent != null )
			{
				if (curObject.parent.parent  != null)
				{
					objectRelativePath = curObject.parent.name + "/" + objectRelativePath;
				}
				curObject = curObject.parent;
			}    
			return objectRelativePath;
		}
		else
		{
			Debug.Log("You asked me to get a relative path for a NULL object!");
		}
		return "";
	}

	// Update is called once per frame
	void Update () {
//		Transform hipTrans = mAnimator.GetBoneTransform(HumanBodyBones.Neck);
//		Debug.Log (hipTrans.localPosition + ", " + hipTrans.localEulerAngles);
	}
}
