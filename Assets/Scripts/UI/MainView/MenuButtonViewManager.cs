using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuButtonViewManager : MonoBehaviour {

	AcceView mAcceView;
	public MenuButtonView PrefabMenuButton;

	public RectTransform gridTransform;

	MenuButtonView curMenuView;

	int count{set; get;}
	List<MenuButtonView> menuViews;
//	public Dictionary<string, MenuButtonView> menuTable;

	private const int MenuButRectWidth = 216;

	public void OnInit(){
//		if(_InternalDataManager.Instance.localConfigTable == null)
//			return;
		if(menuViews == null){
			menuViews = new List<MenuButtonView>();
			_InternalDataManager.Instance.localConfigTable.data.Sort((x, y) => {return x.sort.CompareTo(y.sort);});
			count = _InternalDataManager.Instance.localConfigTable.data.Count;
			for(int i = 0; i < count; i++){
				MenuButtonView menuView = PrefabMenuButton.Spawn();

				menuView.OnInit(_InternalDataManager.Instance.localConfigTable.data[i], this);

				menuViews.Add(menuView);

			}
		}

//		return menuViews[0].data;
	}


	public _SourceMenuData HandleOnColliderTrigging(string type){
//		OnInit();
//		if(string.IsNullOrEmpty(type))
//			return null;
		for(int i = 0; i < count; i++){
			if(menuViews[i].data.type == type){

				SwitchCurrentMenu(i);
//				return menuViews[i].data;
				break;
			}

		}
		return curMenuView.data;

	}

	public void HandleOnMenuClicked(MenuButtonView menuButView)
	{

		SwitchCurrentMenu(menuViews.IndexOf(menuButView));

		mAcceView.HandleOnMenuClicked(curMenuView.data);
		
	}

	void SwitchCurrentMenu(int i){

		//gridTransform.anchoredPosition3D = new Vector3(-(i * MenuButRectWidth), 0, 0);

		if(curMenuView != null)
			curMenuView.OnSelect(false);
		
		curMenuView = menuViews[i];
		
		curMenuView.OnSelect(true);
	}

	// Use this for initialization
	void Start () {
		mAcceView = transform.GetComponentInParent<AcceView>();
		PrefabMenuButton.CreatePool();
	}

}
