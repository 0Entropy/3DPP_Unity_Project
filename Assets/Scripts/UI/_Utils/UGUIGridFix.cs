using UnityEngine;
using System.Collections;

public class UGUIGridFix  {
      

    /// <summary>
    ///  动态添加grid的元素后 grid的程度不会动态修改的bug
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="cellWith"></param>
    /// <param name="space"></param>
    /// <param name="isVertical"></param>
    public static void refresh(RectTransform tran, int cellWith, int space,  bool isVertical = false , int padding = 0  )
    {
        int n = tran.childCount; 
        int L = cellWith * n + (n - 1) * space;
         

        if(isVertical)
        {
            tran.sizeDelta = new Vector2(tran.sizeDelta.x, L);
            tran.localPosition = new Vector3(tran.localPosition.x, -L / 2, tran.localPosition.z);
        }else
        { 
            if (L < 1080)
            {
                L = 1080;
            }

            Debug.Log("----" + L / 2 + padding);
            tran.sizeDelta = new Vector2(L, tran.sizeDelta.y);
            tran.localPosition = new Vector3(L / 2 + padding , tran.localPosition.y, tran.localPosition.z);
        }
    }

    public static void refreshGrid( int  count ,  RectTransform  gridtran , int  cellWith , int  cellSpace , int  screenWith ,  int  screenPadding )
    {
        int n = Mathf.CeilToInt(count * 0.5f);
        int L = cellWith * n + (n - 1) * cellSpace + screenPadding;


		if (L <= screenWith + screenPadding )
        {
            L = screenWith;
		}



		gridtran.sizeDelta = new Vector2(L, gridtran.sizeDelta.y);
        gridtran.localPosition = new Vector3(L * 0.5F, gridtran.localPosition.y, gridtran.localPosition.z);

    }
}
