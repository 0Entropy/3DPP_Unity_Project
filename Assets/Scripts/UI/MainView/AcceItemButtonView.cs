using UnityEngine;
using System.Collections;
using System;

public class AcceItemButtonView : ItemButtonView
{
    public override Texture2D LoadTexture()
    {
        return _InternalDataCodec.LoadTexture(itemData.UIImageLocalPath);
    }

    public override bool IsAssetLoaded()
    {
        return itemData.isDecorationLoaded;
    }

    public override bool IsUILoaded()
    {
        return itemData.isUIImageLoaded;
    }
}
