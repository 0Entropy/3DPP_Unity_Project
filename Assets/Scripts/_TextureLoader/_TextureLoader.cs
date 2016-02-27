using UnityEngine;
using System.Collections.Generic;

public abstract class _TextureLoader {

    public Texture2D defaultTex{ set; get; }
    Texture2D initialTex;
    protected Texture2D currentTex;

    _SourceItemData _defaultItem;
    _SourceItemData defaultItem
    {
        get
        {
            if(_defaultItem == null)
                _defaultItem = _InternalDataManager.Instance.localConfigTable.Find(x => x.id == defaultID);
            return _defaultItem;
        }
    }
    _SourceItemData initItem { set; get; }
    _SourceItemData currentItem { set; get; }

    public int defaultID { set; get; }
    public string texName { set; get; }

    public Dictionary<_SourceItemData, Texture2D> itemTexTable;

    public _SourceItemData ItemData
    {
        get
        {
            if (currentItem != null)
                return currentItem;
            if (initItem != null)
                return initItem;
            if (defaultItem != null)
                return defaultItem;
            return null;
        }
    }

    public int ID
    {
        get
        {
            if (currentItem != null)
                return currentItem.id;
            else if (initItem != null)
                return initItem.id;
            else
                return defaultID;
        }
    }

//     public _TextureLoader()
//     {
// 
//     }

    public _TextureLoader(string _textName, Texture2D _defaultTex, int _id)
    {
        texName = _textName;
        defaultTex = _defaultTex;
        defaultID = _id;
    }

    public void OnDefault()
    {
        initialTex = currentTex = defaultTex;

        SetTexture();

        itemTexTable = new Dictionary<_SourceItemData, Texture2D>();
        itemTexTable.Add(defaultItem, defaultTex);

        initItem = currentItem = defaultItem;

        if (!initItem.IsNull())
            initItem.isHide = true;
    }

    public void OnRelease()
    {
        if (initItem != null)
            initItem.isHide = false;
        if (currentItem != null)
            currentItem.isSelected = false;

        if (itemTexTable == null)
            return;

        List<_SourceItemData> items = new List<_SourceItemData>(itemTexTable.Keys);

        foreach (_SourceItemData item in items.FindAll(x => x != currentItem))
        {
            //			GameObject.Destroy(itemTexTable[item]);
            itemTexTable.Remove(item);
        }


    }



    Texture2D GetItemTexture(_SourceItemData item)
    {
        Texture2D itemTex;// = itemObjTable[item];
                          //-------------------------------------------------------------
        if (itemTexTable == null)
            itemTexTable = new Dictionary<_SourceItemData, Texture2D>();

        if (!itemTexTable.ContainsKey(item))
        {//.title + name)){// || !itemObjTable[item.title + name]
            AssetBundle ab = AssetBundle.CreateFromFile(item.isTempLoaded ? item.TempLocalPath : item.DecorationLocalPath);

            itemTex = ab.mainAsset as Texture2D;//Object.Instantiate()

            if (itemTex != null)
            {
                /*itemTex.wrapMode = TextureWrapMode.Clamp;*/
                itemTexTable.Add(item, itemTex);

            }
            else
            {
                Debug.Log("_TextureLoader.GetItemTexture : itemTex is Null !");
            }
            ab.Unload(false);
        }
        else
        {
            itemTex = itemTexTable[item];
        }
        return itemTex;
    }



    public void UpdateItem(_SourceItemData item, bool toSelect, bool isInitial)
    {

        if (item == null)
        {
            /*Debug.Log("Item is Null!");*/
            this.OnDefault();
            return;
        }

        Texture2D itemTex = GetItemTexture(item);

        if (itemTex == null)
        {
            /*Debug.Log("Texture is Null!");*/
            this.OnDefault();
            return;

        }

        if (isInitial)
        {
            initialTex = itemTex;
            initItem = item;
            initItem.isHide = true;

        }

        ///Show
        if (toSelect)
        {
            /*Debug.Log(item.title + " is Selected !");*/
            if (item == currentItem)
            {
                Debug.Log("There must be something wrong!");
                return;
            }
            else
            {

                

                currentTex = itemTex;
                currentItem = item;
            }

            ///Hide
        }
        else
        {
            /*Debug.Log("Accessory Reselesed !");*/
            if (item == currentItem)
            {
                currentTex = initialTex;
                currentItem = initItem;
            }
            else
            {

                Debug.Log("There must be something else wrong!");
                return;
            }
        }

        //render.material.SetTexture(texName, currentTex);
        SetTexture();
    }

    public abstract void SetTexture();
}
