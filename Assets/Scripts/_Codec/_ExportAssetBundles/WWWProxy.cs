using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WWWProxy {

	public static List<WWWProxy> wwwProxies = new List<WWWProxy>();
	public static bool IsDone{get{ return wwwProxies.Count == 0;}}

	public string url{set; get;}
	public Action<WWW> feedback{set; get;}
	//	public float waitTime{set; get;}

	public WWWProxy(){
		wwwProxies.Add (this);
//		Debug.Log(url + "WWW is Added !!!\n The Count = " + wwwProxies.Count);
	}

	public WWWProxy(string _url, Action<WWW> _action){
		wwwProxies.Add (this);

		url = _url;
		feedback = _action;
//		Debug.Log(url + "WWW is Added !!!\n The Count = " + wwwProxies.Count);
	}
	
	public IEnumerator WaitForFinished(){
		//		waitTime -= Time.deltaTime;
		//		if(waitTime < 0)
		//			yield return null;
		//		else{
		WWW www = new WWW(url);//WWW.LoadFromCacheOrDownload(url, 0);//
		yield return www;
		
		if (www.error == null)
		{

			if(wwwProxies.Remove(this)){
				Debug.Log (url + " WWW is removed!\n The Count = " + wwwProxies.Count);
			}

			if(feedback != null){
				feedback(www);

			}

//			Debug.Log ("WWW is Downloaded!");

			if(www.assetBundle != null)
				www.assetBundle.Unload(false);

			// Frees the memory from the web stream
			www.Dispose();
			www = null;
		}
		else
		{
			if(wwwProxies.Remove(this)){
				Debug.Log (url + " WWW is ERROR!\n The Count = " + wwwProxies.Count);
			}
			// Frees the memory from the web stream
//			www.Dispose();
//			www = null;
//			Debug.Log("WWW ERROR: " + www.error);
			yield return null;
		}
		//		}
	}
}
