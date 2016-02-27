using UnityEngine;
using System.Collections;

public class BaseView : MonoBehaviour {

    public virtual void HandleOnBack()
    {

    }

    public virtual void HandleOnNext()
    {

    }



//    public BaseController controller;
     
	public virtual void HandleOnEscape()
	{

	}

    /// <summary>
    /// 初始化基本UI   没有数据也能执行
    /// </summary>
    public  virtual void OnInit()
    {
         
    }
     

    /// <summary>
    /// 加载基本数据   加载初始化UI 的第一次数据
    /// </summary>
//    public  virtual void InitData()
//    {
//
//    }



//    public virtual void ClickBack()
//    {
//        controller.DoMoveBack( null);
//    }

	public void OnShow(){
//		_BasicInputManager.OnEscape += HandleOnEscape;
		gameObject.SetActive(true);
		OnInit();
	}

	public void OnHide(){
//		_BasicInputManager.OnEscape -= HandleOnEscape;
		gameObject.SetActive(false);
	}

	
	public void OnEnable(){
		_BasicInputManager.OnEscape += HandleOnEscape;
		
	}
	
	public void OnDisable(){
		_BasicInputManager.OnEscape -= HandleOnEscape;
	}
    
}
