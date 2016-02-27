using UnityEngine;
using System.IO;
using System.Collections;

public class _DownLoadTest : MonoBehaviour {

	public string jsonPath = "http://blog.paultondeur.com/files/2010/UnityExternalJSONXML/books.json";
	public string textPath = "http://120.24.226.67:8080/statics/attachment/model/testuijpg/201506161842201153.png";
	public Renderer mSourceRenderer;
	public Renderer mDestRenderer;
//	public _InternalDataCodec mCodec;

	string localPath;

	// Use this for initialization
	void Start () {

		StartCoroutine(new WWWProxy(){url = textPath, feedback = DisplayAndSaveText}.WaitForFinished());
		StartCoroutine(new WWWProxy(){url = jsonPath, feedback = www => Debug.Log(www.text)}.WaitForFinished());

	}

	void DisplayAndSaveText(WWW www){

		mSourceRenderer.material.mainTexture = www.texture;
		localPath = Path.Combine(Application.dataPath ,"_Temp_Textures/something.png");
		_InternalDataCodec.SaveTexture(localPath, www.texture, true);

	}

	public void DeletaTex(){
		_InternalDataCodec.DeleteLocalData(localPath);
	}

	public void ShowLocalTex(){
//		_InternalDataCodec.DeleteLocalData(localPath);
		mDestRenderer.material.mainTexture = _InternalDataCodec.LoadTexture(localPath);
	}

	// Update is called once per frame
	void Update () {
	
	}
}