using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Modified at 2015-10-21.
/// 
/// Modified at 2015-07-02.
/// 取消 enum AccessoryType 标识；
/// 添加 itemObjTable, defaultObj, currentObj 属性；
/// 
/// Modified at 2015-06-10.
/// 此类型 核心功能为 根据配饰类型 ID 实时更换 显示内容；
/// BORWS, EYES, MOUTH 和 FOOTS 为更换贴图；
/// HAIR, HEADWEAR, UP, LEFT_HAND, RIGHT_HAND, BOTTOM 为更换模型及其贴图；
/// HEAD 为预制配饰，应用启动时创建，应用关闭时销毁，过程中只替换 BORWS, EYES, MOUTH 的贴图内容。
/// </summary>
public class _Accessory : MonoBehaviour {

//	Transform _root;

	public Dictionary<_SourceItemData, GameObject> itemObjTable;

	public GameObject defaultObj;//
    public int defaultID;
    _SourceItemData _defaultItem;
    public _SourceItemData defaultItem
    {
        get
        {
            if (_defaultItem == null)
            {
                _defaultItem = _InternalDataManager.Instance.localConfigTable.Find(x => x.id == defaultID);
            }
            return _defaultItem;
        }
    }

    GameObject initialObj{set; get;}
	GameObject currentObj{set; get;}

	
	_SourceItemData initialItem{set; get;}
	_SourceItemData currentItem{set; get;}

	

	public _SourceItemData ItemData{
		get{
			if(!currentItem.IsNull())
				return currentItem;
			if(!initialItem.IsNull())
				return initialItem;
			/*if(!defaultItem.IsNull())
				return defaultItem;
			return null;*/
            return defaultItem;
        }
	}

	public int ID{
		get{
            if (!currentItem.IsNull())
                return currentItem.id;
			if (!initialItem.IsNull())
                return initialItem.id;
            return defaultID;
		}
	}

	public float Volume{
		get{
            if (!currentItem.IsNull())
                return currentItem.superficial_area;
            if (!initialItem.IsNull())
                return initialItem.superficial_area;
            if (!defaultItem.IsNull())
                return defaultItem.superficial_area;
            return 0;
		}
	}

	GameObject GetItemObject(_SourceItemData item, string name){
		GameObject itemObj;// = itemObjTable[item];
		//-------------------------------------------------------------
		if(itemObjTable == null)
			itemObjTable = new Dictionary<_SourceItemData, GameObject>();

		if(!itemObjTable.ContainsKey(item)){//.title + name)){// || !itemObjTable[item.title + name]
			AssetBundle ab = AssetBundle.CreateFromFile(item.isTempLoaded ? item.TempLocalPath : item.DecorationLocalPath);
			if(string.IsNullOrEmpty(name))
			{
				itemObj = Instantiate(ab.mainAsset) as GameObject;
			}
			else
			{
				itemObj = Instantiate(ab.Load (name)) as GameObject;
			}

			itemObj.transform.SetParent(transform);
			itemObj.transform.localPosition = defaultObj.transform.localPosition;
			itemObj.transform.localRotation = defaultObj.transform.localRotation;
			itemObj.SetActive(false);
			itemObjTable.Add(item, itemObj);
			ab.Unload(false);
		}else{
			itemObj = itemObjTable[item];
		}
		//-------------------------------------------------------------
//		AssetBundle ab = AssetBundle.CreateFromFile(item.isTempLoaded ? item.TempLocalPath : item.DecorationLocalPath);
//		if(name.Equals(string.Empty) || name.Equals(""))
//			itemObj = Instantiate(ab.mainAsset) as GameObject;
//		else
//			itemObj = Instantiate(ab.Load (name)) as GameObject;
//		
//		itemObj.transform.parent = transform;
//		itemObj.SetActive(false);
//
//		ab.Unload(false);
		//-------------------------------------------------------------
		return itemObj;
	}

	public void OnDefault(){
		if(currentObj)
			currentObj.SetActive(false);
		if(initialObj)
			initialObj.SetActive(false);
		if(defaultObj)
			defaultObj.SetActive(true);
		currentObj = initialObj = defaultObj;
        
		initialItem = currentItem = defaultItem;

		if(!initialItem.IsNull())
			initialItem.isHide = true;

		if(itemObjTable == null)
			itemObjTable = new Dictionary<_SourceItemData, GameObject>();

	}

	public void OnRelease(){

		if(!initialItem.IsNull())
			initialItem.isHide = false;

		if(!currentItem.IsNull())
			currentItem.isSelected = false;

		if(itemObjTable == null)
			return;

		List<_SourceItemData> items = new List<_SourceItemData>(itemObjTable.Keys);

		foreach(_SourceItemData item in items.FindAll(x => x != currentItem)){
			GameObject.Destroy(itemObjTable[item]);
			itemObjTable.Remove(item);
		}
		
	}

	public void UpdateItem(_SourceItemData item, bool toSelect, bool isInitial){
		UpdateItem (item, string.Empty, toSelect, isInitial);
	}

	public void UpdateItem(_SourceItemData item, string subName, bool toSelect, bool isInitial){

		if(item == null || item.IsNull()){
			OnDefault();
			return;
		}

		Debug.LogError("ITEM_REMARK : " + item.remark);
		///Show
		if(toSelect){
			Debug.Log (item.title + " is Selected !");
//			_InternalDataManager.Instance.AppendLog(item.title + "Accessory Selected ! \n");
			if(item == currentItem){
				Debug.Log (item.title + "toSelect must be something wrong!");
				return;
			}else{

				GameObject itemObj = GetItemObject(item, subName);

				if(!itemObj){
					return;
				}

				if(isInitial){
					initialObj = itemObj;
					initialItem = item;

					if(!initialItem.isHide)
						initialItem.isHide = true;
				}

				if(currentObj)
					currentObj.SetActive(false);
				currentObj = itemObj;
				currentItem = item;
			}
			
		///Hide
		}else{
			Debug.Log ("Accessory Reselesed !");
//			_InternalDataManager.Instance.AppendLog(item.title + "Accessory Reselesed !! \n");
			if(item == currentItem){
//				itemObj.SetActive(false);
				currentObj.SetActive(false);
				currentObj = initialObj;
				currentItem = initialItem;
			}else{
				Debug.Log ("There must be something else wrong!");
				//				currentObj.SetActive(false);
				//				currentObj = itemObj;
			}
		}
		//
		currentObj.SetActive(true);
		//PrintBoneWeightInfo(currentObj, currentItem);
//		_InternalDataManager.Instance.AppendLog(item.title + "Accessory SetActive !! \n");

	}

	string format = "Vector : {0}, Bone : {1}, Weight : {2} \n";

	void PrintBoneWeightInfo(GameObject obj, _SourceItemData item){
		SkinnedMeshRenderer smr = obj.GetComponentInChildren<SkinnedMeshRenderer>();
		if(smr == null)
			return;
		
		Transform[] bs = smr.bones;
		
		BoneWeight[] bws = smr.sharedMesh.boneWeights;
		System.Text.StringBuilder stsb = new System.Text.StringBuilder();
		if(item != null)
			stsb.Append(item.typeCode + " : \n");
		int i = 0;
		while(i < bws.Length){
			BoneWeight bw = bws[i];
			stsb.Append(string.Format(format, i, bs[bw.boneIndex0].name, bw.weight0));
			stsb.Append(string.Format(format, i, bs[bw.boneIndex1].name, bw.weight1));
			stsb.Append(string.Format(format, i, bs[bw.boneIndex2].name, bw.weight2));
			stsb.Append(string.Format(format, i, bs[bw.boneIndex3].name, bw.weight3));
			i++;
		}
		
		Debug.Log (stsb.ToString ());
	}

	void Start(){
		/*OnDefault();*/
	}
}
