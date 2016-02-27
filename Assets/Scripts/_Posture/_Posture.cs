using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// _ posture.
/// void UpdatePosture(_SourceItemData, bool, bool) : 更新当前姿势 _PostureData
/// void SetCurPosture(GameObject) : 将当前姿势赋予对象
/// void Back_T_Posture(GameObject) : 将对象回复至 T Pose
/// </summary>
public class _Posture : MonoBehaviour {

	public TextAsset tPostTxt;
    _PostureData _defaultPose;

    Dictionary<_SourceItemData, _PostureData> poseTable;

    public _PostureData initialPose{set; get;}
	public _PostureData currentPose{set; get;}

    
	_PostureData defaultPose
    {
        get
        {
            if(_defaultPose == null)
                _defaultPose = JsonFx.Json.JsonReader.Deserialize<_PostureData>(tPostTxt.text);
            return _defaultPose;
        }
    }


    public GameObject Target { set; get; }

    _PostureData GetPostureData(_SourceItemData item){
		_PostureData postureData;
		if(!poseTable.ContainsKey(item)){
			AssetBundle ab = AssetBundle.CreateFromFile(item.isTempLoaded ? item.TempLocalPath : item.DecorationLocalPath);
			TextAsset ta = ab.Load("pose") as TextAsset;

			postureData = JsonFx.Json.JsonReader.Deserialize<_PostureData>(ta.text);
			/*Debug.Log ("######### posture Data count : " + postureData.data.Count);*/
			if(postureData != null)
				poseTable.Add(item, postureData);

			ab.Unload(false);
		}else{
			postureData = poseTable[item];
		}
		return postureData;
	}

	public void UpdatePosture(_SourceItemData item, bool toSelect, bool isInitial){

		_PostureData postureData = GetPostureData(item);

		if(postureData == null)
			return;

        if (isInitial)
        {
            initialPose = postureData;
            /*Debug.Log (item.title + "Set Posture to default");*/
        }

        ///Show
        if (toSelect){
			/*Debug.Log ("Accessory Selected !");*/
			if(postureData == currentPose){
				Debug.Log ("There must be something wrong!");
				return;
			}else{
                
                currentPose = postureData;

			}
			
		///Hide
		}else{
			/*Debug.Log ("Accessory Reselesed !");*/
			if(postureData == currentPose){
				currentPose = initialPose;
			}else{
				Debug.Log ("There must be something else wrong!");
			}
		}
		
	}

	public void SetCurPosture(){
        if (!Target)
            return;

		SetCurPosture(Target, currentPose);
	}

	public void Back_T_Pose(){
        if (!Target)
            return;

        /*currentPose = defaultPose;*/
        SetCurPosture(Target, defaultPose);
	}

	void SetCurPosture(GameObject root, _PostureData postureData){

        if (root == null)
        {
            /*Debug.LogError("ROOT IS NULL!");*/
            return;
        }

        if (postureData == null)
        {
            /*Debug.LogError("DATA IS NULL!");*/
            postureData = initialPose != null ? initialPose : defaultPose;
        }
        
		List<Transform> children = new List<Transform>();
		root.GetComponentsInChildren<Transform>(children);
		
		foreach(Transform child in children){

			_SkeletonData dest = postureData.data.Find(x => x.name == child.name);
			if(dest != null){

				child.position = new Vector3(dest.positionF[0] , dest.positionF[1], dest.positionF[2]);
				child.eulerAngles = new Vector3(dest.eulerAnglesF[0], dest.eulerAnglesF[1], dest.eulerAnglesF[2]);

				/*Debug.Log(string.Format("{0} : pos : {1}, angle : {2}",child.name, child.position, child.eulerAngles));*/
			}
		}
	}

    public void OnDefault()
    {
        
        initialPose = currentPose = defaultPose;
        Back_T_Pose();
        /**/
    }

    // Use this for initialization
    void Awake () {
		poseTable = new Dictionary<_SourceItemData, _PostureData>();
        OnDefault();
	}


}
