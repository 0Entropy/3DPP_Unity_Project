using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class _SkinnedMeshAccessory : MonoBehaviour {

//	Transform root;

//	public GameObject defaultPerfab;

	public GameObject defaultObj;
	GameObject initObj{set; get;}
	public GameObject currentObj{private set; get;}
	
	_SourceItemData initItem{set; get;}
	_SourceItemData currentItem{set; get;}

	Dictionary<_SourceItemData, GameObject> itemObjTable;

	public int defaultID;
	
	public int ID{
		get{
			if(currentItem != null)
				return currentItem.id;
			else if(initItem != null)
				return initItem.id;
			else
				return defaultID;
		}
	}
	
	public float Volume{
		get{
			if(currentItem != null)
				return currentItem.superficial_area;
			else if(initItem != null)
				return initItem.superficial_area;
			else 
				return 0;
		}
	}

	public SkinnedMeshRenderer GetSkinnedMeshRenderer(){
		return currentObj.GetComponentInChildren<SkinnedMeshRenderer>();
//		return null;
	}

	public string[] GetBoneNames(){
		return null;
	}

	public void UpdateItem(_SourceItemData item, bool toSelect, bool isInitial){
		UpdateItem(item, string.Empty, toSelect, isInitial);
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
				
				itemObj = GameObject.Instantiate(ab.mainAsset) as GameObject;
				
			}
			else
			{
				
				itemObj = GameObject.Instantiate(ab.Load (name)) as GameObject;
				
			}
			
			itemObj.transform.parent = transform;
			itemObj.transform.localPosition += transform.localPosition;
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

	public void UpdateItem(_SourceItemData item, string subName, bool toSelect, bool isInitial){
		
		if(item == null)
			return;
		
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
					initObj = itemObj;
					initItem = item;
					
					if(!initItem.isHide)
						initItem.isHide = true;
					//					if(initItem != null)
					//						_InternalDataManager.Instance.localConfigTable.Find(x => x.title == initItem.title).isHide = true;
					//initItem.isHide = true;
					//					Debug.Log ("Refesh default");
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
				currentObj = initObj;
				currentItem = initItem;
			}else{
				Debug.Log ("There must be something else wrong!");
				//				currentObj.SetActive(false);
				//				currentObj = itemObj;
			}
		}
		//
		currentObj.SetActive(true);
		//		_InternalDataManager.Instance.AppendLog(item.title + "Accessory SetActive !! \n");
		
	}

	
	public void OnInit(){

		if(currentObj)
			currentObj.SetActive(false);
		if(initObj)
			initObj.SetActive(false);
		if(defaultObj)
			defaultObj.SetActive(true);
		currentObj = initObj = defaultObj;
		initItem = currentItem = _InternalDataManager.Instance.localConfigTable.Find(x => x.id == defaultID);
		
		if(initItem != null)
			initItem.isHide = true;
		
		if(itemObjTable == null)
			itemObjTable = new Dictionary<_SourceItemData, GameObject>();

	}
	
	public void OnRelease(){}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
