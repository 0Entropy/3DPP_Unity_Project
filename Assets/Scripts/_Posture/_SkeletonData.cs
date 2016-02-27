using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class _AnimationData{

	public List<_PostureData> data{set; get;}
}

[Serializable]
public class _PostureData : ScriptableObject
{
	public List<_SkeletonData> data{set; get;}

//	public void GetTransform(string name){
//		if(data == null || data.Count == 0)
//			return ;
//		_SkeletonData sd = data.Find( x => x.name == name);
//
//	}

}

[Serializable]
public class _SkeletonData {

	public string name{set; get;}
	public float[] positionF{set; get;}
	public float[] eulerAnglesF{set; get;}
}
