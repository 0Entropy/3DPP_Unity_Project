using UnityEngine;
using System.Collections;

//[System.Serializable]
//public class BoneSegment {
////	public Transform firstTransform;
//	public Transform lastTransform;
//	public float thresholdAngleDifference = 0;
//	public float bendingMultiplier = 0.6f;
//	public float maxAngleDifference = 30;
//	public float maxBendingAngle = 80;
//	public float responsiveness = 5;
//	internal float angleH;
//	internal float angleV;
//	internal Vector3 dirUp;
//	internal Vector3 referenceLookDir;
//	internal Vector3 referenceUpDir;
//	internal int chainLength;
//	internal Quaternion[] origRotations;
//}

[System.Serializable]
public class HandlerSegment {

}

public class _PostureController : MonoBehaviour {

	Animator animator;

	public _SkeletonHandler skeletonHandler;

	public Transform humanBone;

	void OnEnable(){
//		_BasicInputManager.OnFocus += HandleOnFocus;
	}

	void OnDisable(){
//		_BasicInputManager.OnFocus -= HandleOnFocus;
	}

//	void HandleOnFocus (GameObject g)
//	{
////		Debug.Log (g.layer + ", " + LayerMask.NameToLayer ("Skeleton Layer"));
//		if(g.layer != LayerMask.NameToLayer ("Skeleton Layer"))
//			return;
////		_SkeletonHandler handler = skeletonHandler.Spawn(g.transform.position, g.transform.rotation);
////		Bounds bounds = g.collider.bounds;
//		CapsuleCollider collider = g.GetComponent<CapsuleCollider>();
////		Vector3 size = bounds.size;
////		float radius = collider.radius;
////		float scale = Mathf.Max (collider.height * 0.5F, collider.radius) * 0.5F;
////		Vector3 deltaPos = g.collider.bounds.center;
////		Vector3 size = g.collider.bounds.size;
//
//		skeletonHandler.transform.position = g.transform.position;
//		skeletonHandler.transform.rotation = g.transform.rotation;
//		skeletonHandler.transform.localScale = Vector3.one * Mathf.Clamp(collider.radius, 0.9F, 1.8F);
//		skeletonHandler.Target = g.transform;
//		Debug.Log("" + g.name);
//
////		HumanBone humanBune = HumanTrait.
////		GetComponent<_BasicViewpointController>().isPause = true;
//
////		humanBone = animator.GetBoneTransform(HumanBodyBones.Spine);
////		Debug.Log("" + humanBone.gameObject.name);
////		skeletonHandler.Target = humanBone;
////		skeletonHandler.transform.position = humanBone.position;
////		skeletonHandler.transform.rotation = humanBone.rotation;
//
//	}

	// Use this for initialization
	void Start () {

//		skeletonHandler.CreatePool();

		animator = GetComponent<Animator>();

		Debug.Log ("--- bone ---");
		string[] boneName = HumanTrait.BoneName;
		int i = 0;
		while (i < HumanTrait.BoneCount) {
			Debug.Log(boneName[i]);
			i++;
		}

		Debug.Log ("--- muscle ---");
		string[] muscleName = HumanTrait.MuscleName;
		i = 0;
		while (i < HumanTrait.MuscleCount) {
			Debug.Log(muscleName[i]);
			i++;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
