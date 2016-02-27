
#if UNITY_IPHONE
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class IOSPlatformBridge : Singleton<IOSPlatformBridge>, IPlatformBridge {

//	public delegate void OnLoadFinishEvent(int result);
//	public static event OnLoadFinishEvent OnLoadFinish;

//	public int GoodHistoryID {set; get;}
	
	public override void Init(){
		Debug.Log("iOS　Platform is Inited!!!");
		
	}
	
	public override void Release(){
		Debug.Log("iOS　Platform is Released!!!");
	}

	/// <summary>
	/// 联系客服。
	/// </summary>
	[DllImport("__Internal")]
	private static extern void ContactToServer();
	public void _ContactToServer(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : ContactToServer");
			ContactToServer();
		}
	}

	/// <summary>
	/// QQ分享。
	/// </summary>
	[DllImport("__Internal")]
	private static extern void ShareToQQ();
	public void _ShareToQQ(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : ShareToQQ");
			ShareToQQ();
		}
	}

	/// <summary>
	/// 微信分享。
	/// </summary>
	[DllImport("__Internal")]
	private static extern void ShareToWX();
	public void _ShareToWX(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : ShareToWX");
			ShareToWX();
		}
	}

	/// <summary>
	/// 朋友圈分享。
	/// </summary>
	[DllImport("__Internal")]
	private static extern void ShareToFC();
	public void _ShareToFC(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : ShareToFC");
			ShareToFC();
		}
	}

	/// <summary>
	/// 微博分享。
	/// </summary>
	[DllImport("__Internal")]
	private static extern void ShareToWB();
	public void _ShareToWB(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : ShareToWB");
			ShareToWB();
		}
	}

	/// <summary>
	/// 调用前端原生分享。
	/// </summary>
	[DllImport("__Internal")]
	private static extern void ShareToNative();
	public void _ShareToNative(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : ShareToNative");
			ShareToNative();
		}
	}

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
	[DllImport("__Internal")]
	private static extern void UploadModelConfig(string itemsList, string model_config_json, float volume);
	public void _UploadModelConfig(string itemsList, string model_config_json, float volume){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : UploadModelConfig");
			UploadModelConfig(itemsList, model_config_json, volume);
		}
	}

	/// <summary>
	/// 此方法已替换为 UploadModelConfig 方法。 2015-09-16
	/// 上传当前模型文件
	/// </summary>
	/// <param name="model_config">Model_config. ===> {"decorationlist" : [{"id" : 1, "typeCode" : "head", ... },{"id" : 2, "typeCode" : "head", ... }]} </param>
	/// <param name="model_name">Model_name. 用户输入之模型名字；如为空，则为"yyyyMMdd_HHmmss"。</param>
	/// <param name="file_path">File_path. 模型截图之保存地址（Android版中无用）。</param>
	/// <param name="app_code">App_code. ===> "small" </param>
	[DllImport("__Internal")]
	private static extern void UploadModel(string model_config, string model_name, string file_path, string app_code);
	public void _UploadModel(string model_config, string model_name, string file_path, string app_code)
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer){

			UploadModel(model_config, model_name, file_path, app_code);
		}
		
	}
	
	/// <summary>
	/// 直接购买当前模型。
	/// </summary>
	/// <param name="strJson">String json.===> {"num": 1, "size": "7cm", "material":"石膏", "price": "288.00", "historyId": "123456" }</param>
	[DllImport("__Internal")]
	private static extern void NextToBuyMent(string strJson);
	public void _NextToBuyMent(string strJson)
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : NextToBuyMent");
			NextToBuyMent(strJson);
		}
	}
	
	/// <summary>
	/// 将当前模型加入购物车。
	/// </summary>
	/// <param name="strJson">String json ===> {"num": 1, "size": "7cm", "material":"石膏", "price": "288.00", "historyId": "123456" }</param>
	[DllImport("__Internal")]
	private static extern void AddToCart(string strJson);
	public void  _AddToCart(string  strJson)
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : AddToCart");
			AddToCart(strJson);
		}
	}
	
	/// <summary>
	/// 获取购物车内商品数量。
	/// </summary>
	[DllImport("__Internal")]
	private static extern int GetCartCount();//????
	public int _GetCartCount(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : GetCartCount");
			return GetCartCount();
		}
		return -1;
	}
	
	
	/// <summary>
	/// 分享模型。
	/// </summary>
	[DllImport("__Internal")]
	private static extern void NextToShare();
	public void  _NextToShare()
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : NextToShare");
			NextToShare();
		}
	}
	
	/// <summary>
	/// 前往我的工厂
	/// </summary>
	[DllImport("__Internal")]
	private static extern void NextToMyFactory();
	public void _NextToMyFactory()
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : NextToMyFactory");
			NextToMyFactory();
		}
	}
	
	/// <summary>
	/// 跳转至购物车
	/// </summary> 
	[DllImport("__Internal")]
	private static extern void NextToCart();
	public void _NextToCart()
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : NextToCart");
			NextToCart();
		}
		
	}
	
	/// <summary>
	/// 友盟数据分析之用户数据提交；
	/// </summary>
	/// <param name="type">Type. 数据类型。</param>
	/// <param name="info">Info. 数据明细。</param>
	[DllImport("__Internal")]
	private static extern void UserActionData(string type, string info);
	public void  _UserActionData(string type, string info)
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : UserActionData");
			UserActionData(type, info);
		}
	}
	
	/// <summary>
	/// 此方法 iOS 平台无用。2015-09-16
	/// 实体键返回
	/// </summary> 
	/// 实体 Back 返回键 行为 同 屏幕虚拟 Home 键行为相同； 
	public void _KeyBackHome(){
		//Nothing To Do!
	}
	
	/// <summary>
	/// 屏幕‘Home’按键
	/// </summary>
	[DllImport("__Internal")]
	private static extern void BtnBackHome();
	public void _BtnBackHome(){
		if(Application.platform == RuntimePlatform.IPhonePlayer){
			Debug.Log("iOS　Platform : BtnBackHome");
			BtnBackHome();
		}
	}

}
#endif
