using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System ;

public class MenuButtonView : MonoBehaviour {

	public string menuName{set; get;}

    public Image imUp;
    public Image imDown;
    public Image imPoint;

    Action<MenuButtonView> HandleOnClick;
	AcceView mAcceView;
	
	public _SourceMenuData data{set; get;}

	MenuButtonViewManager menuManager{set; get;}
	string menuType{set; get;}

	bool isSelected = false;

	void UpdateMenuSprite(){
		if(data == null || string.IsNullOrEmpty(data.type))
			return;
        /*Debug.Log("Data_type : " + data.type);*/
		imDown.sprite = Resources.Load<Sprite>(string.Format("MenuViewSprites/icon_{0}_down",data.type));
		imUp.sprite = Resources.Load<Sprite>(string.Format("MenuViewSprites/icon_{0}_up",data.type));
	}

    public void OnSelect(bool isSelect)
    {
		isSelected = isSelect;
        if (isSelect)
        {
            imUp.gameObject.SetActive(false);
            imDown.gameObject.SetActive(true);
            imPoint.gameObject.SetActive(true);
        }
		else
        {
            imUp.gameObject.SetActive(true);
            imDown.gameObject.SetActive(false);
            imPoint.gameObject.SetActive(false); 
        }
    }

//	public void OnShow(_SourceMenuData data, AcceView acceView)//Action<MenuButtonView> clickAction)
//    {
//        this.data = data;
////        this.HandleOnClick = clickAction;
//		mAcceView = acceView;
//
//		OnSelect(false);
//    }

	public void OnInit(_SourceMenuData _data, MenuButtonViewManager manager){
		data = _data;
		menuManager = manager;

		transform.SetParent(manager.gridTransform);
		transform.localScale = Vector3.one;
		name = string.Format("Button Menu {0}", data.type);

		UpdateMenuSprite();
		OnSelect(false);
	}


    public void _OnClick()
    {
//		if(PlatformBridge.Instance != null)
//			PlatformBridge.Instance._UserActionData(data.type, " be selected! ");

//		mAcceView.HandleOnShowItems(this);//HandleOnClick(this);
		if(!isSelected){
			menuManager.HandleOnMenuClicked(this);
		}
    }

}
