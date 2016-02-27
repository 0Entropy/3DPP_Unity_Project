using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AcceView : BaseView {

	public ItemButtonViewManager itemManager;

	public MenuButtonViewManager menuManager;

	public override void HandleOnBack(){
        /*_UIController.Instance.ShowMainView();*/
        /*_Hero.Instance.SavePlayerPrefs();*/
        _Hero.Instance.Release();
        _UIController.Instance.HandleOnSave(() => {
           /* Debug.Log("AcceView.HanleOnBack(){ PlatformBridge.Instance._BtnBackHome();}");*/
            if (PlatformBridge.Instance != null)
             {
                PlatformBridge.Instance._BtnBackHome();
            }
        });
        
    }

    public override void HandleOnNext()
    {
        Debug.Log("AcceView.HandleOnNext(){  _UIController.Instance.ShowDanceView();}");
        /*_Hero.Instance.TexLocalPath = Camera.main.GetComponent<_MainCameraController>().OnCapturePicture();*/
        _UIController.Instance.ShowDanceView();
    }
		
	public override void HandleOnEscape ()
	{
        HandleOnBack();
	}
    
	public override void OnInit ()
	{
		menuManager.OnInit();
        itemManager.OnShow(menuManager.HandleOnColliderTrigging("brows"));
    }


// 	public void HandleOnColliderTrigging(string type){
// 		itemManager.OnShow(menuManager.HandleOnColliderTrigging(type));
// 	}

	public void HandleOnMenuClicked(_SourceMenuData data)
	{
		
		itemManager.OnShow(data);

/*		_UIController.Instance.HandleOnAcceMenuButClicked(data.type);*/
		
	}
}