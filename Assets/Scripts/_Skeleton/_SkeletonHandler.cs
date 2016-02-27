using UnityEngine;
using System.Collections;

//[System.Serializable]
//public class BendingSegment {
//	public Transform firstTransform;
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

public class _SkeletonHandler : MonoBehaviour {
//	[System.Serializable]
//	public BendingSegment segments;

	private Transform _target;
	public Transform Target{
		set{

//			if(value == null)
//				gameObject.SetActive(false);
//			else if(!gameObject.activeSelf)
//				gameObject.SetActive(true);
			foreach(Transform child in transform){
				if(!child.gameObject.activeSelf)
					child.gameObject.SetActive(true);
			}
			_target = value;
		}
		get{
			return _target;
		}
	}

	public Axis axis{set; get; }

	public GameObject X_Axis, Y_Axis, Z_Axis;
	public enum Axis{NULL, X_Axis, Y_Axis, Z_Axis}

//	public Camera camera;

	void OnEnable(){
		_BasicInputManager.OnDragStart += HandleOnDragStart;
		_BasicInputManager.OnDragEnd += HandleOnDragEnd;
		_BasicInputManager.OnDrag += HandleOnDrag;
	}

	
	void OnDisable(){
		_BasicInputManager.OnDragStart -= HandleOnDragStart;
		_BasicInputManager.OnDragEnd -= HandleOnDragEnd;
		_BasicInputManager.OnDrag -= HandleOnDrag;
	}
	
	void HandleOnDrag (GameObject obj, Vector2 delta)
	{
		if(obj != X_Axis && obj != Y_Axis && obj != Z_Axis){
			return;
		}

		Vector3 cameraDir = - Camera.main.transform.forward;
		Vector3 deltaDir = - Camera.main.transform.forward + (Vector3)delta * Time.deltaTime;
		switch(axis){
		case Axis.X_Axis:
			transform.Rotate(Vector3.right * AngleAroundAxis(cameraDir, deltaDir, transform.right));
			break;
		case Axis.Y_Axis:
			transform.Rotate(Vector3.up * AngleAroundAxis(cameraDir, deltaDir, transform.up));
			break;
		case Axis.Z_Axis:
			transform.Rotate(Vector3.forward * AngleAroundAxis(cameraDir, deltaDir, transform.forward));
			break;
		default:
			break;
		}

		if(Target != null)
		Target.rotation = transform.rotation;
//		Transform t = segments.lastTransform;
//		t.rotation = transform.rotation;
	}

	void HandleOnDragEnd ()
	{
		axis = Axis.NULL;
//		Target = null;
		Debug.Log ("Drag End");
		foreach(Transform child in transform){
			if(!child.gameObject.activeSelf)
				child.gameObject.SetActive(true);
		}
	}

	void HandleOnDragStart (GameObject obj)
	{
		if(obj == X_Axis){
			
			axis = Axis.X_Axis;
			Y_Axis.SetActive(false);
			Z_Axis.SetActive(false);
		}else if(obj == Y_Axis){
			
			axis = Axis.Y_Axis;
			Z_Axis.SetActive(false);
			X_Axis.SetActive(false);
		}else if(obj == Z_Axis){
			
			axis = Axis.Z_Axis;
			
			Y_Axis.SetActive(false);
			X_Axis.SetActive(false);
			
		}
	}

	void Start(){
//		camera = Camera.main;
//		transform.rotation = Target.rotation;
	}

	// The angle between dirA and dirB around axis
	public static float AngleAroundAxis (Vector3 dirA, Vector3 dirB, Vector3 axis) {
		// Project A and B onto the plane orthogonal target axis
		dirA = dirA - Vector3.Project(dirA, axis);//减去投射在轴上的分量
		dirB = dirB - Vector3.Project(dirB, axis);//减去投射在轴上的分量
		
		// Find (positive) angle between A and B
		float angle = Vector3.Angle(dirA, dirB);//求角
		
		// Return angle multiplied with 1 or -1
		//从dirA至dirB右手法则求 公垂线 与 轴 axis 是否 同方向；
		//同方向则 正值 旋转；
		//不同方向则 负值 旋转。
		return angle * (Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
	}

}
