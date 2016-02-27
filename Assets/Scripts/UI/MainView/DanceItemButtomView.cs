using UnityEngine;
using System.Collections;

public class DanceItemButtomView : ItemButtonView
{
    public override Texture2D LoadTexture()
    {
        
        return Resources.Load<Texture2D>(string.Format("DanceItemSprites/{0}", itemData.title));
    }

    public override bool IsAssetLoaded()
    {
        return true;
    }

    public override bool IsUILoaded()
    {
        return true;
    }
}

