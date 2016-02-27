using UnityEngine;
using System.Collections;

public class Asset_WWW_Test : MonoBehaviour {

	public string path;
	// Use this for initialization
	IEnumerator Start () {
		WWW www = new WWW (path);
		yield return www;
		// Get the designated main asset and instantiate it.
		Instantiate(www.assetBundle.mainAsset);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
