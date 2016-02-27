using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 内部数据辅助工具类
/// </summary>
/// 
public class _InternalDataUtils{

	public void FilterConfigTable(_ConfigTable configTable){

	}

	public void FilterMenuData(List<_SourceMenuData> sourceMenus){
		foreach(_SourceMenuData menu in sourceMenus){
			FilterItemData(menu.data);
			if(menu.children.Count > 0)
				FilterMenuData(menu.children);
		}
	}

	
	void FilterItemData(List<_SourceItemData> sourceItems){
		sourceItems.RemoveAll( x => !x.isDefault & x.decoration_status == 1);
	}

}
