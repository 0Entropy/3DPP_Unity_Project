using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class _UIController : Singleton<_UIController> {

	public delegate void OnUpdateAccessoryEvent(_SourceItemData item, bool toSelect, bool isPlayable);
	public static event OnUpdateAccessoryEvent OnUpdateAccessory;

    public delegate void OnUpdateWallpaperEvent(_SourceItemData item, bool toSelect);
    public static event OnUpdateWallpaperEvent OnUpdateWallpaper;

    public delegate void OnUpdateDanceEvent(_SourceItemData item, bool toSelect);
    public static event OnUpdateDanceEvent OnUpdateDance;

    public delegate void OnEnterDanceViewEvent();
    public static event OnEnterDanceViewEvent OnEnterDanceView;

    public delegate void OnExitDanceViewEvent();
    public static event OnExitDanceViewEvent OnExitDanceView;

    public NavigationBar navigationBar;

    public AcceView acceView;
    public DanceView danceView;

    BaseView currentView;
    
    /*public InputField inputName;*/
    public PopWindow popWindow;
    public AlertWindow alertWindow;

    /*	string currentFocusedAcceName = string.Empty;*/

    _MainCameraController mCamControllor;
	_RotatController mRotControllor;

/*	public UITextConfig uiTextConfig{private set; get;}*/

	bool isBlock = false;

	public bool IsAcceView{
		get
        {
			return currentView == acceView;
		}
	}

    public bool IsDanceView
    {
        get
        {
            return currentView == danceView;
        }
    }

	public void OnClickItem(_SourceItemData item, bool toSelect, bool isPlayable){
        if (item.typeCode.Equals("wallpaper"))
        {
            if (OnUpdateWallpaper != null)
            {
                Debug.Log("Update wallpaper : " + item.typeCode + ", " + item.id);
                OnUpdateWallpaper(item, toSelect);
            }
        }
        else if (item.typeCode.Equals("dance"))
        {
            Debug.Log("Update Dance: " + item.typeCode + ", " + item.id);
            if (OnUpdateDance != null)
            {
                OnUpdateDance(item, toSelect);
            }
        }
        else
        {
            if (OnUpdateAccessory != null)
            {
                OnUpdateAccessory(item, toSelect, isPlayable);
            }
        }
	}

	public void BlockTouch(){
		isBlock = true;
		mCamControllor.isScalable = false;
		mRotControllor.isRotatable = false;
	}

	public void UnblockTouch(){
		isBlock = false;
		mCamControllor.isScalable = true;
		mRotControllor.isRotatable = true;
	}

	public void ShowMainView(){

		/*_Hero.Instance.GetComponent<WallpaperHandler>().OnShow();*/

		isBlock = false;

        if (OnExitDanceView != null)
        {
            OnExitDanceView();
        }

        HandleOnShow(acceView);

        navigationBar.OnInit();

        /*_BasicInputManager.Instance.Input_Y_Area_Factor = new Vector2(0.2f, 0.9f);*/
        _BasicInputManager.Instance.Input_Y_Area_Factor = new Vector2(0.3f, 0.9f);

        mCamControllor.BackToInitPoint();

		mRotControllor.BackToInitPoint();
		mRotControllor.isRotatable = true;

	}

    /*public string TempPixName
    {
    	set; get;
    }*/

    public _ModelData TempModelData { set; get; }

	public void ShowAcceView(){

        /*if (!string.IsNullOrEmpty(TempPixName))
        {
            _InternalDataCodec.DeleteLocalData(Path.Combine(_InternalDataManager.Instance._CapturePicturePath, TempPixName));
        }*/

        if(TempModelData != null && !TempModelData.IsNull())
        {
            TempModelData.OnDestroy();
        }

        _Hero.Instance.GetComponent<_RotatController>().BackToInitPoint();
        isBlock = false;

        if(OnExitDanceView != null)
        {
            OnExitDanceView();
        }

        /*_Hero.Instance.GetComponent<WallpaperHandler>().OnShow();*/

		HandleOnShow(acceView);

		_BasicInputManager.Instance.Input_Y_Area_Factor = new Vector2(0.3f, 0.9f);
	}

    public void ShowDanceView()
    {

        //TempPixName = Camera.main.GetComponent<_MainCameraController>().OnCapturePicture();

        TempModelData = _ModelData.OnCreate();

        isBlock = false;

        /*_Hero.Instance.GetComponent<WallpaperHandler>().OnHide();*/

        if (OnEnterDanceView != null)
        {
            OnEnterDanceView();
        }

        HandleOnShow(danceView);
    }

	void OnEnable(){
/*		_BasicInputManager.OnFocus += HandleOnFocus;*/
	}

	void OnDisable(){
/*		_BasicInputManager.OnFocus -= HandleOnFocus;*/
	}

	public override void Init(){

		isBlock = false;

		mCamControllor = Camera.main.GetComponent<_MainCameraController>();
		mRotControllor = _Hero.Instance.GetComponent<_RotatController>();

        /*		currentFocusedAcceName = string.Empty;*/

        // 		if(saveView == null || shopView == null || mainView == null || acceView == null)
        // 			return;

        if (acceView == null || danceView == null)
            return;

        // 		saveView.OnHide();
        // 		shopView.OnHide();
        // 		acceView.OnHide();
        // 		mainView.OnHide();

        acceView.OnHide();
        danceView.OnHide();

        alertWindow.OnHide();

		popWindow.OnHide();

		/*uiTextConfig = JsonFx.Json.JsonReader.Deserialize<UITextConfig>(uiText.text);*/

		//TODO...
	}

    public void HandleOnBack()
    {
        Debug.Log("UIController HandleOnBack");
        currentView.HandleOnBack();
    }

    public void HandleOnNext()
    {
        Debug.Log("UIController HandleOnNext");
        currentView.HandleOnNext();
    }

    public void HandleOnSave(Action act)
    {

        if (_Hero.Instance.IsEdited)
        {
            if (_Hero.Instance.IsNew)
            {
                Debug.Log("Is Edited & Is New");
                popWindow.OnShow(act);
                _Hero.Instance.SaveToFactory(null, true);
            }
            else
            {
                Debug.Log("Is Edited & Is Not New");
                alertWindow.OnShow(act);
            }
        }
        else
        {
            Debug.Log("Is Not Edited");

            if (act != null)
            {
            	act();
            }
        }
    }



    public override void Release(){
		//TODO...
		/*_BasicInputManager.OnFocus -= HandleOnFocus;*/
		/*PlatformBridge.OnShareFinish -= HandleOnShareFinish;*/
	}

	
	void HandleOnShow(BaseView view){
		if(currentView != null)
			currentView.OnHide();
		currentView = view;
		currentView.OnShow();
	}
}
