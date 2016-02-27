using UnityEngine;
using System.Collections;

public class _RotatController : MonoBehaviour {

//	private float speedRotation = 2.5F;
	private float sensitivity_rot = .25F;

	
	public bool isRotatable{ set; get; }

	private Quaternion _initRot = Quaternion.identity;

//	private Quaternion _viewpointRot;
	
	void Start () {
		sensitivity_rot *= 1080.0f /(float)Screen.width ;
		BackToInitPoint();
	}
	
	void OnEnable() {
		_BasicInputManager.OnMove += HandleOnRotate;
	}
	
	void OnDisable() {
		_BasicInputManager.OnMove -= HandleOnRotate;
	}

	
	
	void HandleOnRotate(Vector2 delta){
		
		if (!isRotatable)
			return;
		//Debug.LogError("" + delta);
		float delta_x = -delta.x * sensitivity_rot;
		float delta_y = delta.y * sensitivity_rot;
		if(Mathf.Abs(delta_x) > Mathf.Abs(delta_y)){
			//		Vector3 dir_Y = transform.InverseTransformDirection(stereoCamera.transform.right);
			Vector3 dir_X = transform.InverseTransformDirection(Vector3.up);
			transform.rotation *= Quaternion.AngleAxis(delta_x, dir_X);
		}
		//transform.rotation = _viewpointRot;//Quaternion.Lerp(transform.rotation, _viewpointRot, Time.deltaTime * speedRotation);
	}
	
//	void Update () {
//		//transform.rotation = Quaternion.Lerp(transform.rotation, _viewpointRot, Time.deltaTime * speedRotation);
//	}
	
	public void BackToInitPoint() {

		isRotatable = true;
		transform.rotation = _initRot;
//		_viewpointRot = transform.rotation = _initRot;
	}
}
