using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class PlatformBridge : MonoBehaviour{

	public delegate void OnUploadFinishEvent(string result);
	public static event OnUploadFinishEvent OnUploadFinish;

	public delegate void OnShareFinishEvent(string result);
	public static event OnShareFinishEvent OnShareFinish;

	private static IPlatformBridge _instance;
	public static IPlatformBridge Instance{
		get{
			if(_instance ==null)
			{
#if UNITY_ANDROID
				if(Application.platform == RuntimePlatform.Android){
						_instance = AndroidPlatformBridge.Instance;
				}
#elif UNITY_IPHONE
				if(Application.platform == RuntimePlatform.IPhonePlayer){
						_instance = IOSPlatformBridge.Instance;
				}
#endif
			}
			return _instance;
		}
	}

	[Serializable]
	public class HistoryID{
		public int result{set; get;}
		public int id{set; get;}
	}

	/// <summary>
	/// Handles the on update cart number.
	/// Android 平台下，点击实体‘back’键从购物车返回粘土人 shopcartView 页面时，前端回调；
	/// </summary>
	/// <param name="num">Number.</param>
//	public void HandleOnBackFromShopcart(string num){
//		_Hero.Instance.HandleOnCartCountCallback(num);
//		_UIController.Instance.ShowShopView();
//	}


//	public void HandleOnUpdateAvatorID(string id){
//
//	}

	/// <summary>
	/// 点击分享后，由前端返回调用。
	/// 本方法 Android 平台 仅需针对 ‘第三方分享 APP 未安装’状况 做反馈；
	///  <param> result : string :
	/// -2: 第三方分享 APP 未安装；
	/// -1: 分享失败；
	/// 0: 分享取消；
	/// 1: 分享成功；
	///  </param> 
	/// </summary>
	public void HandleOnShareCallback(string result){
		/*_UIController.Instance.HandleOnShareFinish(result);*/
	}

	/// <summary>
	/// 上传模型文件后，之返回调用。
	/// Edited bu Robin at 2015-09-10 
	/// <param> result : string: 
	///  本地 id 
	/// </param>
	/// </summary>
	public void HandleOnUploadCallback(string result){
		_Hero.Instance.HandleOnUploadFinish(result);

	}



	/// <summary>
	/// 创建初始模型时传值
	/// </summary>
	/// <param> Json : string :
	/// {"id":"0",
	/// "app_code":"small",
	/// "name":"赐我一个名字吧!",
	/// "wallpaperIndex":0,
	/// "texLocalPath":"",
	/// "decorationlist" : [{"id" : 1, "typeCode" : "head", ... },{"id" : 2, "typeCode" : "head", ... }]}
	/// </param>
	/// </summary>
	public void HandleOnCreateAvator(string json){
//		_UIController.Instance.Init();
		Debug.Log ("PlatformBridge.HandleOnCreateAvator(string):void");
		StartCoroutine(_Hero.Instance.OnInitialAvator(json));
	}
}


