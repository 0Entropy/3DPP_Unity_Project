using UnityEngine;
using UnityEngine.UI ;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public abstract class ItemButtonView : MonoBehaviour, IItemUIIamgeLoader, IItemPreporty
{
    public Image imageIcon;
    public Image imageIsNew;
    public Image imageNotSelect;
    public Image imageSelect;
    public Image imageNotDown;
    public Image imageLoading;

    //	Action<ItemButtonView> UnSelectOther;
    ItemButtonViewManager mManager;

    public _SourceItemData itemData { set; get; }

    bool isDownloading { set; get; }

    public void OnShow(_SourceItemData data, ItemButtonViewManager manager)//Action<ItemButtonView> back)
    {
        this.itemData = data;

        mManager = manager;

        transform.SetParent(manager.gridTransform);
        transform.localScale = Vector3.one;
        name = string.Format("Button Item {0}", data.title);

        UpdateIconSprite();

        UpdateState();

    }

    public void UpdateState() {
        if (IsUILoaded()) {

            imageIsNew.gameObject.SetActive(itemData.isNew);

            if (IsAssetLoaded()) {
                if (itemData.isSelected) {
                    imageSelect.gameObject.SetActive(true);
                    imageNotSelect.gameObject.SetActive(false);
                } else {
                    imageSelect.gameObject.SetActive(false);
                    imageNotSelect.gameObject.SetActive(true);
                }
                imageLoading.gameObject.SetActive(false);
                imageNotDown.gameObject.SetActive(false);

            } else {
                imageLoading.gameObject.SetActive(false);
                imageNotDown.gameObject.SetActive(true);
                imageSelect.gameObject.SetActive(false);
                imageNotSelect.gameObject.SetActive(false);
            }
        }

    }

    public void _OnClick()
    {
        //		clickAction( this ) ;
        HandleOnClick();
    }

    void HandleOnClick()
    {
        if (IsAssetLoaded() && !isDownloading) {
            OnSelect();
        } else {
            StartCoroutine(itemData.WaitForDownloadAsset());
            StartCoroutine(OnAssetDowload());
        }
    }

    void OnSelect()
    {
        bool toSelect = !itemData.isSelected;

        if (toSelect) {
            //			UnSelectOther(this);
            mManager.UnSelectedOthers(this);
        }

        itemData.isSelected = toSelect;
        UpdateState();

        _UIController.Instance.OnClickItem(itemData, toSelect, true);

    }

    IEnumerator OnAssetDowload() {
        ShowDownloadView();
        /*_InternalDataManager.Instance.DownloadItemAsset(itemData);*/

        while (!IsAssetLoaded()) {
            yield return new WaitForFixedUpdate();
        }
        itemData.isSelected = false;
        HideDownloadView();
        yield return null;
    }


    void ShowDownloadView() {
        isDownloading = true;
        imageLoading.gameObject.SetActive(true);
        imageNotDown.gameObject.SetActive(false);
        imageSelect.gameObject.SetActive(false);
        imageNotSelect.gameObject.SetActive(false);
    }

    void HideDownloadView() {
        isDownloading = false;
        imageLoading.gameObject.SetActive(false);
        imageNotDown.gameObject.SetActive(false);
        imageSelect.gameObject.SetActive(false);
        imageNotSelect.gameObject.SetActive(true);
    }

    void UpdateIconSprite()
    {
        if (itemData == null || !IsUILoaded())
            return;
        Texture2D texture = LoadTexture();// _InternalDataCodec.LoadTexture(itemData.UIImageLocalPath);

        imageIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }

    public abstract Texture2D LoadTexture();
    public abstract bool IsAssetLoaded();
    public abstract bool IsUILoaded();

}

public interface IItemUIIamgeLoader
{
    Texture2D LoadTexture();
}

public interface IItemPreporty
{
    bool IsAssetLoaded();
    bool IsUILoaded();
    
}
