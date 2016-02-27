using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

public class _MainCameraController : MonoBehaviour {

	private float sensitivity_z = 0.2F;

	private Vector3 _headPos = new Vector3(0, 4, 7);
	private Vector3 _topPos = new Vector3(0, 2, 7);
	private Vector3 _bottomPos = new Vector3(0, 1, 7);

	private Vector3 _initPos = new Vector3(0, 2.1f, 10.5f); // new Vector(0, 3, 10);
	private Vector3 _initAng = new Vector3(0, 180, 0);
	
	private Vector3 _viewpointPos;
	private Vector3 _viewpointAng;
	
	private float _min_z;
	private float _max_z;
	private float _init_z;

//	public Transform capturePoint;

	private Vector3 capturePos = new Vector3(0, 3.5f, 12);
	private Vector3 shopCartPos = new Vector3(0, 1, 10);

	public bool isScalable{ set; get; }
	
	void Start () {
		_init_z = _initPos.z;
		_min_z = _init_z * 0.75F;
		_max_z = _init_z * 1.25F;

		BackToInitPoint();

	}
	
	void OnEnable() {
//		_BasicInputManager.OnMove += HandleOnMove;
		_BasicInputManager.OnScale += HandleOnScale;
	}
	
	void OnDisable() {
//		_BasicInputManager.OnMove -= HandleOnMove;
		_BasicInputManager.OnScale -= HandleOnScale;
	}


	//Transform
	void HandleOnScale(float delta){
		if (!isScalable)
			return;
		float z = _viewpointPos.z;
		z += delta > 0 ? -sensitivity_z : sensitivity_z;
		z = Mathf.Clamp(z, _min_z, _max_z);
				
		_viewpointPos = new Vector3(_viewpointPos.x, _viewpointPos.y, z);
		transform.position = _viewpointPos;
	}

	
	public void BackToInitPoint() {
		isScalable = true;
        _Hero.Instance.GetComponent<_RotatController>().isRotatable = true;
        _viewpointPos = _initPos;
		transform.eulerAngles = _viewpointAng = _initAng;

		camera.orthographic = false;
		camera.fieldOfView = 60F;
	}



	public void GoToCapturePoint(){
		isScalable = false;
		_Hero.Instance.GetComponent<_RotatController>().BackToInitPoint();
		_Hero.Instance.GetComponent<_RotatController>().isRotatable = false;
		transform.position = capturePos;
		transform.eulerAngles = _initAng;

	}

	public void GoToShopCartPoint(){
		isScalable = false;

//		transform.position = capturePoint.position;
		_viewpointPos = shopCartPos;////capturePoint.position + new Vector3(0, 1, 0);
		transform.eulerAngles = _initAng;//capturePoint.eulerAngles;
		
		camera.orthographic = true;
		camera.orthographicSize = 7F;
	}

	public void FocusOnHead(){
//		isScalable = false;
		_viewpointPos = _headPos;
	}

	public void FocusOnUpper(){
//		isScalable = false;
		_viewpointPos = _topPos;
	}

	public void FocusOnBottom(){
//		isScalable = false;
		_viewpointPos = _bottomPos;
	}

	void Update(){
		if(transform.position != _viewpointPos){
			transform.position = Vector3.Lerp(transform.position, _viewpointPos, 0.1f);
		}
	}

	public string OnCapturePicture()
	{
		GoToCapturePoint();

		RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 8, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
		Texture2D screenShot = new Texture2D(Screen.width, Screen.width, TextureFormat.ARGB32, false);
		
		camera.targetTexture = rt;
		camera.Render();
		
		RenderTexture.active = rt;

		screenShot.ReadPixels(new Rect(0, (int)((Screen.height - Screen.width)*0.5f), Screen.width, Screen.width), 0, 0 );
		screenShot.Apply();

		string texName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";
		string texPath = Path.Combine(_InternalDataManager.Instance._CapturePicturePath, texName);
		_InternalDataCodec.SaveTexture(texPath, screenShot, false);

		Camera.main.targetTexture = null;
		RenderTexture.active = null;
		GameObject.Destroy(rt);

		BackToInitPoint();

		return texName;
	}  


}
