//using UnityEditor;
using UnityEngine;
using System;
using System.Collections;

class ManagedAssetBundleExample : MonoBehaviour {
	public string url;
	public int version;
	AssetBundle bundle;

	void OnGUI (){
		if (GUILayout.Button ("Download bundle")){
			bundle = _AssetBundleManager.getAssetBundle (url, version);
			if(!bundle)
				StartCoroutine (DownloadAB());
		}
	}

	IEnumerator DownloadAB (){
		yield return StartCoroutine(_AssetBundleManager.downloadAssetBundle (url, version));
		bundle = _AssetBundleManager.getAssetBundle (url, version);
	}

	void OnDisable (){
		_AssetBundleManager.Unload (url, version, true);
	}

}