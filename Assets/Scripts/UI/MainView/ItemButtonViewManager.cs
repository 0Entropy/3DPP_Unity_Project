using UnityEngine;
using UnityEngine.UI ;
using System ;
using System.Collections;
using System.Collections.Generic;

public class ItemButtonViewManager : MonoBehaviour {

/*	AcceView mAcceView;*/

	public ItemButtonView prefabItemView;

    public RectTransform gridTransform;

	public List<ItemButtonView>  itemViews ;

	void Start () {
		/*mAcceView = transform.GetComponentInParent<AcceView>();*/
		prefabItemView.CreatePool();
	}

	public void OnShow(_SourceMenuData data){

		if(data == null)
			return;
        
		List<_SourceItemData> list = data.FindAll(x => x.isUIImageLoaded && !x.isHide);
		list.Sort((x, y) => {return -x.createTime.CompareTo(y.createTime);});

        OnShow(list);

	}

    public void OnShow(List<_SourceItemData> list)
    {
        if (list == null || list.Count <= 0)
            return;

        ResetViews();

        int count = 0;
        foreach (_SourceItemData item in list)
        {
            ItemButtonView itemView = prefabItemView.Spawn();

            itemView.OnShow(item, this);

            itemViews.Add(itemView);
            count++;
        }

        UGUIGridFix.refreshGrid(count, gridTransform, 200, 30, 1080, 64);
    }

    public void UnSelectedOthers(ItemButtonView itemView){
		for(int i = 0; i < itemViews.Count; i++){
			ItemButtonView other = itemViews[i];
			if(other.itemData.typeCode == itemView.itemData.typeCode && other.itemData.isSelected){
				other.itemData.isSelected = false;
				other.UpdateState();
			}
		}
	}

	void ResetViews(){

        if (itemViews == null)
        {
            itemViews = new List<ItemButtonView>();
            return;
        }

        if (itemViews.Count == 0)
        {
            return;
        }

        for (int i = 0; i < itemViews.Count; i++){
	
			itemViews[i].Recycle();
        }

		itemViews.Clear();
	}

}
