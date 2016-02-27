using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class _Avator : MonoBehaviour{

	public Texture2D defaultBrowTex;
	public Texture2D defaultEyesTex;
	public Texture2D defaultMouthTex;

	public _Accessory hat;
	public _Accessory eyerings;
	public _Accessory glasses;
	public _Accessory hair;

	public _Accessory leftHand;
	public _Accessory rightHand;
	public _Accessory leftFoot;
	public _Accessory rightFoot;

	public _Accessory head;
	public _Accessory upper;
	public _Accessory bottom;

	public SkinnedMeshRenderer featureRenderer;

	public GameObject ActualAvatorObj;
	
	_Posture posture;

	_FeatureTextureLoader brow;
	_FeatureTextureLoader eye;
	_FeatureTextureLoader mouth;

    /*_PostureLocalAnimHandler postureHandler;*/

    


    public List<_SourceItemData> ItemList{
		get{
			List<_SourceItemData> itemList = new List<_SourceItemData>();
			if(!hat.ItemData.IsNull())
				itemList.Add(hat.ItemData);
			if(!hair.ItemData.IsNull())
				itemList.Add (hair.ItemData);
			if(!glasses.ItemData.IsNull())
				itemList.Add(glasses.ItemData);
			if(!eyerings.ItemData.IsNull())
				itemList.Add(eyerings.ItemData);
			if(!brow.ItemData.IsNull())
				itemList.Add(brow.ItemData);
			if(!eye.ItemData.IsNull())
				itemList.Add(eye.ItemData);
			if(!mouth.ItemData.IsNull())
				itemList.Add(mouth.ItemData);
			if(!upper.ItemData.IsNull())
				itemList.Add(upper.ItemData);
			if(!rightHand.ItemData.IsNull())
				itemList.Add(rightHand.ItemData);
			if(!bottom.ItemData.IsNull())
				itemList.Add(bottom.ItemData);
			if(!rightFoot.ItemData.IsNull())
				itemList.Add(rightFoot.ItemData);
			return itemList;
		}
	}
	
	public string ItemIDs{
		get{
			StringBuilder sb = new StringBuilder("[");
			List<string> itemIds = new List<string>();

			if(hat.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",hat.ID));
			if(glasses.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",glasses.ID));
			if(eyerings.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",eyerings.ID));
			if(brow.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",brow.ID));
			if(eye.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",eye.ID));
			if(mouth.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",mouth.ID));
			if(hair.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",hair.ID));
			if(upper.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",upper.ID));
			if(rightHand.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",rightHand.ID));
			if(bottom.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",bottom.ID));
			if(rightFoot.ID != 0)
				itemIds.Add(string.Format("{{\"decoration_id\":{0}}}",rightFoot.ID));

			int size = itemIds.Count;
			for(int i=0; i<size; i++){
				sb.Append(itemIds[i]);
				if(i != size - 1){
					sb.Append(",");
				}

			}
			sb.Append("]");//}
			return sb.ToString();
		}
	}

	public float Volume{
		get{
			float surfaceArea = 0;
			surfaceArea += hat.Volume;
			surfaceArea += glasses.Volume;
			surfaceArea += eyerings.Volume;
			surfaceArea += hair.Volume;
			surfaceArea += upper.Volume;
			surfaceArea += rightHand.Volume;
			surfaceArea += bottom.Volume;
			surfaceArea += rightFoot.Volume;
			return surfaceArea * 0.2F;
		}
	}

    

    _SourceItemData lastHandsItem;

    public void StoreHandsItemAndSetToDefault()
    {
        Debug.Log("Avatar Do Default Hands.");
        lastHandsItem = rightHand.ItemData;
        OnDefaultHands();
    }

    public void ResetHandsToLastItem()
    {
        Debug.Log("Avatar Do Rest Hands.");

        if (lastHandsItem == null)
            return;

        OnUpdateHands(lastHandsItem, true, false);
        posture.SetCurPosture();

        lastHandsItem = null;
    }

    void OnUpdateHands(_SourceItemData handItem, bool toSelect, bool isInitial)
    {
        if (handItem == null || !handItem.typeCode.Equals("righthand"))
            return;

       /* Debug.Log("Hand id : ---> " + handItem.id);*/

        if(!handItem.isDefault)
        {
            OnDefaultHands();
            return;
        }

        posture.UpdatePosture(handItem, toSelect, isInitial);
        /*postureHandler.UpdatePosture(handIitem, true, true);*/
        rightHand.UpdateItem(handItem, "right_hand", toSelect, isInitial);
        leftHand.UpdateItem(handItem, "left_hand", toSelect, isInitial);
    }

    void OnDefaultHands()
    {
        posture.OnDefault();
        rightHand.OnDefault();
        leftHand.OnDefault();
    }

    public void OnInitial(List<_SourceItemData> itemList){

        OnDefault();

        if (posture.enabled)
		    posture.Back_T_Pose();

		_SourceItemData hatItem = itemList.Find(x => x.typeCode == "hat");
		hat.UpdateItem(hatItem, true, true);

		_SourceItemData glassesItem = itemList.Find(x => x.typeCode == "glasses");
		glasses.UpdateItem(glassesItem, true, true);

		_SourceItemData EyeringsItem = itemList.Find(x => x.typeCode == "earrings");
		eyerings.UpdateItem(EyeringsItem, true, true);

		_SourceItemData HairItem = itemList.Find(x => x.typeCode == "hair");
		hair.UpdateItem(HairItem, true, true);

		_SourceItemData bowsItem = itemList.Find(x => x.typeCode == "brows");
		brow.UpdateItem(bowsItem, true, true);

		_SourceItemData eyesItem = itemList.Find(x => x.typeCode == "eyes");
		eye.UpdateItem(eyesItem, true, true);

		_SourceItemData mouthItem = itemList.Find(x => x.typeCode == "mouth");
		mouth.UpdateItem(mouthItem, true, true);

		_SourceItemData upperItem = itemList.Find(x => x.typeCode == "upper");
		upper.UpdateItem(upperItem, true, true);

		_SourceItemData handIitem = itemList.Find(x => x.typeCode == "righthand");
        // 		posture.UpdatePosture(handIitem, true, true);
        // 		/*postureHandler.UpdatePosture(handIitem, true, true);*/
        // 		rightHand.UpdateItem(handIitem, "right_hand", true, true);
        // 		leftHand.UpdateItem(handIitem, "left_hand", true, true);
        OnUpdateHands(handIitem, true, true);

        _SourceItemData bottomItem = itemList.Find(x => x.typeCode == "bottom");
		bottom.UpdateItem(bottomItem, true, true);

		_SourceItemData footsItem = itemList.Find(x => x.typeCode == "foots");
		rightFoot.UpdateItem(footsItem, "right_foot", true, true);
		leftFoot.UpdateItem(footsItem, "left_foot", true, true);

		BuildSkinnedMesh();

        /*postureHandler.SetCurPosture(true);*/

        if(posture.enabled)
            posture.SetCurPosture();


    }

	/// <summary>
	/// Updates the accessory.
	/// 分发 配饰更新 事件
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="id">Identifier.</param>
	/// <param name="isPlayable">If set to <c>true</c> is playable.</param>
	public void UpdateAccessory(_SourceItemData item, bool toSelect = true, bool isPlayable = true, bool isInitial = false){

//		if(posture == null)
//			posture = GetComponent<_Posture>();

        if(posture.enabled)
	        posture.Back_T_Pose();
        
		switch(item.typeCode){
		case "hat":
			hat.UpdateItem(item, toSelect, isInitial);
			break;

		case "glasses":
			glasses.UpdateItem(item, toSelect, isInitial);
			break;

		case "earrings":
			eyerings.UpdateItem(item, toSelect, isInitial);
			break;

		case "hair":
			hair.UpdateItem(item, toSelect, isInitial);
			break;

		case "brows":
			brow.UpdateItem(item, toSelect, isInitial);
			break;

		case "eyes":
			eye.UpdateItem(item, toSelect, isInitial);
			break;

		case "mouth":
			mouth.UpdateItem(item, toSelect, isInitial);
			break;

		case "upper":
			upper.UpdateItem(item, toSelect, isInitial);
			break;

		case "righthand":
                OnUpdateHands(item, toSelect, isInitial);
                break;

		case "bottom":
			bottom.UpdateItem(item, toSelect, isInitial);
			break;

		case "foots":
			leftFoot.UpdateItem(item, "left_foot", toSelect, isInitial);
			rightFoot.UpdateItem(item, "right_foot", toSelect, isInitial);
			break;

		default:
			break;
		}

		BuildSkinnedMesh();

//		combineMeshAndAcceObj.GetComponent<Animator>().Play(0);

        /*postureHandler.SetCurPosture(true);*/

        if (posture.enabled)
            posture.SetCurPosture();

        //		posture.SetCurPosture(upper.currentObj);

    }

//	// Use this for initialization
	void Start ()
    {
//		animator = GetComponent<Animator>();
		brow 		= new _FeatureTextureLoader("_BrowTex", defaultBrowTex, 291, featureRenderer);
		eye 		= new _FeatureTextureLoader("_EyeTex", defaultEyesTex, 287, featureRenderer);
		mouth	    = new _FeatureTextureLoader("_MouthTex", defaultMouthTex, 283, featureRenderer);

        posture = GetComponent<_Posture>();
        posture.Target = ActualAvatorObj;
        /*postureHandler = ActualAvatorObj.GetComponent<_PostureLocalAnimHandler>();*/
        
        //		head.OnInit();

    }

    void BuildSkinnedMesh(){

		float startTime = Time.realtimeSinceStartup;
		
		// The SkinnedMeshRenderers that will make up a character will be
		// combined into one SkinnedMeshRenderers using multiple materials.
		// This will speed up rendering the resulting character.
		List<CombineInstance> combineInstances = new List<CombineInstance>();
		List<Material> materials = new List<Material>();
		List<Transform> bones = new List<Transform>();

		CombineSubMesh(head, ref combineInstances, ref materials, ref bones);

//		Debug.Log ("Bottom : " );
		CombineSubMesh(bottom, ref combineInstances, ref materials, ref bones);

//		Debug.Log ("Upper : " );
		CombineSubMesh(upper, ref combineInstances, ref materials, ref bones);


		SkinnedMeshRenderer r = ActualAvatorObj.GetComponentInChildren<SkinnedMeshRenderer>();
		
		r.sharedMesh = new Mesh();
		r.sharedMesh.CombineMeshes(combineInstances.ToArray(), false, false);
		r.bones = bones.ToArray();
		r.materials = materials.ToArray();
		
		Debug.Log("Generating character took: " + (Time.realtimeSinceStartup - startTime) * 1000 + " ms");
	}

	void CombineSubMesh(_Accessory subMeshAcce, ref List<CombineInstance> combineInstances, ref List<Material> materials, ref List<Transform> bones)
	{
//		GameObject skinnedObj = Instantiate(subMeshAcce.currentObj) as GameObject;
//		SkinnedMeshRenderer smr = skinnedObj.GetComponentInChildren<SkinnedMeshRenderer>();

		SkinnedMeshRenderer smr = subMeshAcce.GetComponentInChildren<SkinnedMeshRenderer>();

//		Debug.Log("" + smr.name + "");

		materials.AddRange(smr.materials);

		for (int sub = 0; sub < smr.sharedMesh.subMeshCount; sub++)
		{
			CombineInstance ci = new CombineInstance();

			ci.mesh = smr.sharedMesh;

			ci.subMeshIndex = sub;
			combineInstances.Add(ci);
		}

//		foreach (string bone in element.GetBoneNames())
		foreach(Transform bone in smr.bones)
		{
//			Debug.Log ("bone : " + bone.name);
			foreach (Transform transform in ActualAvatorObj.GetComponentsInChildren<Transform>())
			{
				if (transform.name != bone.name)//|| bones.Contains(transform)) 
					continue;
//				Debug.Log ("Transform : " + transform.name);
				bones.Add(transform);

				break;
			}
		}

//		GameObject.Destroy(skinnedObj);

	}

	public void OnDefault(){


        if (posture.enabled)
            posture.Back_T_Pose();

        

        hat.OnDefault();
		
		glasses.OnDefault();
		
		eyerings.OnDefault();
		
		hair.OnDefault();
		
		brow.OnDefault();
		
		eye.OnDefault();
		
		mouth.OnDefault();
		
		upper.OnDefault();//upper need to generate skinnedmesh conpine...

        /*posture.OnInit();*///.currentPose = posture.defaultPose = _InternalDataManager.Instance.T_Pose;
                             /*posture.OnDefault();
                             rightHand.OnDefault();
                             leftHand.OnDefault();*/
        OnDefaultHands();

        bottom.OnDefault();
		
		rightFoot.OnDefault();
		leftFoot.OnDefault();

		BuildSkinnedMesh();

        if (posture.enabled)
            posture.SetCurPosture();
        //		combineMeshAndAcceObj.animation.Play();

    }
	
	public void OnRelease(){
		hat.OnRelease();
		
		glasses.OnRelease();
		
		eyerings.OnRelease();
		
		hair.OnRelease();
		
		brow.OnRelease();
		
		eye.OnRelease();
		
		mouth.OnRelease();
		
		upper.OnRelease();
		
		rightHand.OnRelease();

		leftHand.OnRelease();
		
		bottom.OnRelease();
		
		rightFoot.OnRelease();

		leftFoot.OnRelease();

//		postureHandler.OnRelease();
		
		Resources.UnloadUnusedAssets();

		System.GC.Collect();
	}

//	void BackToHome(){
//		transform.localPosition = Vector3.zero;
//		transform.localEulerAngles = Vector3.zero;
//	}

}
