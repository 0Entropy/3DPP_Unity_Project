using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DanceView : BaseView {

    public Button nextButton;

    public ItemButtonViewManager itemManager;

    List<_SourceItemData> danceItemList;

    void Awake()
    {
        danceItemList = new List<_SourceItemData>();

        Texture2D[] textures = Resources.LoadAll<Texture2D>("DanceItemSprites");

        for(int i=0; i<textures.Length; i++)
        {
            danceItemList.Add(new _SourceItemData { typeCode = "dance", title = textures[i].name, id = i });
        }
    }

    public override void HandleOnBack()
    {
        /*Debug.Log("DanceView.HanleOnBack(){ _UIController.Instance.ShowAcceView();}");*/

        _UIController.Instance.ShowAcceView();
    }

    public override void HandleOnNext()
    {
        /*_Hero.Instance.SavePlayerPrefs();*/
        _Hero.Instance.Release();
        _UIController.Instance.HandleOnSave(() =>
        {
            /*Debug.Log("DanceView.HandleOnNext(){ PlatformBridge.Instance._NextToCart();}");*/

            if (PlatformBridge.Instance != null)
            {
                PlatformBridge.Instance._NextToCart();
            }
        });
        
    }

    public override void HandleOnEscape()
    {
        /*base.HandleOnEscape();*/
        HandleOnBack();
    }

    void UnselectAll()
    {
        if (danceItemList == null || danceItemList.Count <= 0)
            return;

        foreach(_SourceItemData item in danceItemList)
        {
            item.isSelected = false;
        }
    }

    public override void OnInit()
    {
        /*base.OnInit();*/
        if (_Hero.Instance.IsNew && !_Hero.Instance.IsEdited)
            nextButton.gameObject.SetActive(false);
        else
            nextButton.gameObject.SetActive(true);
        UnselectAll();
        itemManager.OnShow(danceItemList);
    }
}
