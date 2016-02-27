using UnityEngine;
using System.Collections;

public class WallpaperHandler : MonoBehaviour {

	public Transform textureGroup;
	public Transform shadowGroup;

    public Texture2D defaultTex;

    _WallpaperTextureLoader wallpaperLoader;

    public int WallpaperID
    {
        get
        {
            return wallpaperLoader.ID;
        }
    }
    
	public void OnHide(){
		textureGroup.gameObject.SetActive(false);
		shadowGroup.gameObject.SetActive(false);
	}

	public void OnShow(){
        textureGroup.gameObject.SetActive(true);
		shadowGroup.gameObject.SetActive(true);
    }

    void Start()
    {
         wallpaperLoader = new _WallpaperTextureLoader("_MainTex", defaultTex, 0, textureGroup);
        
    }

    public void OnDefault()
    {
        wallpaperLoader.OnDefault();
    }

    public void OnRelease()
    {
        wallpaperLoader.OnRelease();
    }

    public void OnUdateWallpaper(_SourceItemData item, bool toSelect, bool isInitial)
    {
        wallpaperLoader.UpdateItem(item, toSelect, isInitial);
    }

    private void HandleOnUpdateWallpaper(_SourceItemData item, bool toSelect)
    {
        Debug.Log("Wallpaper HandleOnUpdateWallpaper");
        Debug.Log(string.Format("Item is {0}, WallpaperLoader is {1}", (item == null), (wallpaperLoader == null)));
        wallpaperLoader.UpdateItem(item, toSelect, false);
    }



    void OnEnable()
    {
        _UIController.OnUpdateWallpaper += HandleOnUpdateWallpaper;
        _UIController.OnEnterDanceView += HandleOnEnterDanceView;
        _UIController.OnExitDanceView += HandleOnExitDanceView;
    }

    private void HandleOnExitDanceView()
    {
        OnShow();
    }

    private void HandleOnEnterDanceView()
    {
        OnHide();
    }

    void OnDisable()
    {
        _UIController.OnUpdateWallpaper -= HandleOnUpdateWallpaper;
        _UIController.OnEnterDanceView -= HandleOnEnterDanceView;
        _UIController.OnExitDanceView -= HandleOnExitDanceView;
    }
}
