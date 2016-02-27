using UnityEngine;
using System;
using System.Collections;

public class _BasicViewpointController : MonoBehaviour {

	private float speedPosition = 5F;
	private float sensitivity_z = 0.5F;
	private float sensitivity_y = 0.25F;

	private float speedRotation = 2.5F;
	private float sensitivity_rot = .25F;

	private Vector3 _initPos;
	private Quaternion _initRot;

	private Vector3 _viewpointPos;
	private Quaternion _viewpointRot;

	private float _min_z;
	private float _max_z;

	private float _init_z;
	private float _init_y;

	private float gestureDegreen = 20F;

	public Camera stereoCamera;

	void Start () {
		OnInit ();
		isPause = false;
	}

	public void OnInit(){

//		isPause = false;

//		_depth = _initDepth = transform.localPosition.z;
		_init_z = transform.localPosition.z;
		_init_y = transform.localPosition.y;
//		_min_y = _init_y - 5;
//		_max_y = _init_y + 5;
		_min_z = _init_z * 0.5F;
		_max_z = _init_z * 1.5F;
		_viewpointPos = _initPos = transform.localPosition;
		_viewpointRot = _initRot = transform.rotation;
	}

	void OnEnable() {
		_BasicInputManager.OnMove += HandleOnMove;
		_BasicInputManager.OnScale += HandleOnScale;
	}

	void OnDisable() {
		_BasicInputManager.OnMove -= HandleOnMove;
		_BasicInputManager.OnScale -= HandleOnScale;
	}

	public bool isPause{ set; get; }


	void HandleOnMove(Vector2 delta){

		if (isPause)
						return;
		//Debug.LogError("" + delta);
		float delta_x = -delta.x * sensitivity_rot;
		float delta_y = delta.y * sensitivity_rot;
		if(Mathf.Abs(delta_x) > Mathf.Abs(delta_y)){
//		Vector3 dir_Y = transform.InverseTransformDirection(stereoCamera.transform.right);
			Vector3 dir_X = transform.InverseTransformDirection(stereoCamera.transform.up);
			_viewpointRot *= Quaternion.AngleAxis(delta_x, dir_X);
		}else{
			float y = _viewpointPos.y;
			float factor = _viewpointPos.z / _max_z ;
			float deg_y =  _viewpointPos.z * 0.25F;
			y += delta_y * sensitivity_y * factor;
			y = Mathf.Clamp(y, _init_y - deg_y, _init_y + deg_y);
			_viewpointPos = new Vector3(_viewpointPos.x, y, _viewpointPos.z);
		}

	}

	//Transform
	void HandleOnScale(float delta){
		if (isPause)
			return;
		float z = _viewpointPos.z;
		z += delta > 0 ? -sensitivity_z : sensitivity_z;
		z = Mathf.Clamp(z, _min_z, _max_z);

		float y = _viewpointPos.y;
		float factor = z / _max_z ;
		float deg_y = z * 0.25F;
		y = Mathf.Clamp(y, _init_y - deg_y + 2.5F, _init_y + deg_y);

		_viewpointPos = new Vector3(_viewpointPos.x, y, z);
	}

	void FixedUpdate () {

		transform.localPosition = Vector3.Lerp(transform.localPosition, _viewpointPos, Time.deltaTime * speedPosition);
		transform.rotation = Quaternion.Lerp(transform.rotation, _viewpointRot, Time.deltaTime * speedRotation);
	}

	public void BackHome() {
//		_depth = _init_z;
		_viewpointPos = _initPos;//Vector3.zero;
		_viewpointRot = _initRot;//Quaternion.identity;
	}
}
