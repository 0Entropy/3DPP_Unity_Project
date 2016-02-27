using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

#if UNITY_ANDROID
public class AndroidPlatformBridge : Singleton<AndroidPlatformBridge>, IPlatformBridge{

	public override void Init(){
		Debug.Log("Android Platform is Inited!!!");
		
	}
	
	public override void Release(){
		Debug.Log("Android Platform is Released!!!");
	}
	/// <summary>
	/// 联系客服。
	/// </summary>
	public void _ContactToServer(){
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("ContactToServer");
		}
	}
	/// <summary>
	/// QQ分享。
	/// </summary>
	public void _ShareToQQ(){
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("ShareToQQ");
		}
	}
	/// <summary>
	/// 微信分享。
	/// </summary>
	public void _ShareToWX(){
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("ShareToWX");
		}
	}
	/// <summary>
	/// 朋友圈分享。
	/// </summary>
	public void _ShareToFC(){
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("ShareToFC");
		}
	}
	/// <summary>
	/// 微博分享。
	/// </summary>
	public void _ShareToWB(){
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("ShareToWB");
		}
	}

	/// <summary>
	/// 调用前端系统分享。
	/// </summary>
	public void _ShareToNative(){

	}

//	public string _GetLastSavedId(){
//		if(Application.platform == RuntimePlatform.Android){
//			if(GetCurrentActivity() != null)
//				return GetCurrentActivity().Call<string>("GetLastSavedId");
//		}
//		return string.Empty;
//	}

	/// <summary>
	/// 上传当前模型数据（json字符串）
	/// </summary>
	/// <param name="model_config_json">Model_config_json. ===> 
	/// {"app_code":"small",
	/// "name":"model_name",
	/// "wallpaperIndex":0,
	/// "texLocalPath":"",
	/// "decorationlist" : [{"id" : 1, "typeCode" : "head", ... },{"id" : 2, "typeCode" : "eyes", ... }]}
	/// </param>
    /// </summary>
	public void _UploadModelConfig(string itemsList, string model_config_json, float volume){
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("UploadModelConfig", itemsList, model_config_json, volume);
		}
	}

	/// <summary>
	/// 上传当前模型文件
	/// </summary>
	/// <param name="model_config">Model_config. ===> {"decorationlist" : [{"id" : 1, "typeCode" : "head", ... },{"id" : 2, "typeCode" : "head", ... }]} </param>
	/// <param name="model_name">Model_name. 用户输入之模型名字；如为空，则为"yyyyMMdd_HHmmss"。</param>
	/// <param name="file_path">File_path. 模型截图之保存地址（Android版中无用）。</param>
	/// <param name="app_code">App_code. ===> "small" </param>

	public void _UploadModel(string model_config, string model_name, string tex_path, string app_code)
	{
//		captureImageLocalPath = file_path;
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("UploadModel",  model_config, model_name, tex_path, app_code);
		}
		
	}
	
	/// <summary>
	/// 直接购买当前模型。
	/// </summary>
	/// <param name="strJson">String json.===> {"num": 1, "size": "7cm", "material":"石膏", "price": "288.00" }</param>
	/// Version 1.1.0 Cancle!!! ===> , "historyId": "123456"  

	public void _NextToBuyMent(string strJson)
	{
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("NextToBuyMent", strJson);
		}
	}
	
	/// <summary>
	/// 将当前模型加入购物车。
	/// </summary>
	/// <param name="strJson">String json ===> {"num": 1, "size": "7cm", "material":"石膏", "price": "288.00"}</param>
	/// Version 1.1.0 Cancle!!! ===> , "historyId": "123456"  

	public void  _AddToCart(string  strJson)
	{
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("AddToCart", strJson);
		}
	}
	
	/// <summary>
	/// 获取购物车内商品数量。
	/// </summary>

	public int _GetCartCount(){
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				return GetCurrentActivity().Call<int>("getShopcartCount");
		}
		return -1;
	}
	
	
	/// <summary>
	/// 分享模型。
	/// </summary>

	public void  _NextToShare()
	{
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("NextToShare", string.Empty);
		}
	}
	
	/// <summary>
	/// 前往我的工厂
	/// </summary>

	public void _NextToMyFactory()
	{
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("NextToMyFactory");
		}
	}
	
	/// <summary>
	/// 跳转至购物车
	/// </summary> 

	public void _NextToCart()
	{
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("NextToCart");
		}
		
	}
	
	/// <summary>
	/// 友盟数据分析之用户数据提交；
	/// </summary>
	/// <param name="type">Type. 数据类型。</param>
	/// <param name="info">Info. 数据明细。</param>

	public void _UserActionData(string type, string info)
	{
		if(Application.platform == RuntimePlatform.Android){
			if(GetCurrentActivity() != null)
				GetCurrentActivity().Call("eventHappen", type, info);
		}
	}
	
	/// <summary>
	/// 实体键返回
	/// </summary> 
	/// 实体 Back 返回键 行为 同 屏幕虚拟 Home 键行为相同； 
	public void _KeyBackHome(){
		//_Hero.Instance.SavePlayerPrefs();//SavePlayerPrefs

		if(GetCurrentActivity() != null)
			GetCurrentActivity().Call("BtnBackHome");
	}
	
	/// <summary>
	/// 屏幕‘Home’按键
	/// </summary>
	public void _BtnBackHome(){
		//_Hero.Instance.SavePlayerPrefs();

		if(GetCurrentActivity() != null)
			GetCurrentActivity().Call("BtnBackHome");
	}

	AndroidJavaObject GetCurrentActivity()
	{

		AndroidJavaClass jc = new AndroidJavaClass("com.ulifang.phone.threedmaker.UnityActivity");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("mContext");
		
		return jo;
	}
}
#endif
