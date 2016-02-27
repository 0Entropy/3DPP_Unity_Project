using UnityEngine;
using System.Collections;

public interface IPlatformBridge {

	void Init();

	void Release();

	void _ContactToServer();

	void _ShareToQQ();
	void _ShareToWX();
	void _ShareToFC();
	void _ShareToWB();

	void _ShareToNative();

    //	string _GetLastSavedId();

    /// <summary>
    /// 上传当前模型数据（json字符串）
    /// </summary>
    /// <param name="model_config_json">
    /// Model_config_json. ===> 
    /// {"app_code":"small",
    /// "name":"model_name",
    /// "wallpaperIndex":0,
    /// "texLocalPath":"",
    /// "decorationlist" : [{"id" : 1, "typeCode" : "head", ... },{"id" : 2, "typeCode" : "eyes", ... }]}
    /// </param>
    /// <param name="volume">
    /// _Hero.Instence.currentAvatorData.volume;
    /// </param>
    /// </summary>

    void _UploadModelConfig(string itemsList, string model_config_json, float volume);

	/// <summary>
	/// 上传当前模型文件
	/// </summary>
	/// <param name="model_config">Model_config. ===> {"decorationlist" : [{"id" : 1, "typeCode" : "head", ... },{"id" : 2, "typeCode" : "head", ... }]} </param>
	/// <param name="model_name">Model_name. 用户输入之模型名字；如为空，则为创建时间之"yyyyMMdd_HHmmss"格式字符串。</param>
	/// <param name="file_path">File_path. 模型截图之保存地址（Android版中无用）。</param>
	/// <param name="app_code">App_code. ===> "small" </param>
	void _UploadModel(string model_config, string model_name, string file_path, string app_code);
	
	/// <summary>
	/// 直接购买当前模型。
	/// </summary>
	/// <param name="strJson">String json.===> 
	/// {"num": 1, "size": "7cm", "material":"石膏", "price": "288.00", "historyId": "123456" }</param>
	void _NextToBuyMent(string strJson);
	
	/// <summary>
	/// 将当前模型加入购物车。
	/// </summary>
	/// <param name="strJson">String json ===> 
	/// {"num": 1, "size": "7cm", "material":"石膏", "price": "288.00", "historyId": "123456" }</param>
	void _AddToCart(string  strJson);
	
	/// <summary>
	/// 获取购物车内商品数量。
	/// </summary>
	int _GetCartCount();

	/// <summary>
	/// 分享模型。
	/// </summary>
	void  _NextToShare();
	
	/// <summary>
	/// 前往我的工厂
	/// </summary>
	void _NextToMyFactory();
	
	/// <summary>
	/// 跳转至购物车
	/// </summary> 
	void _NextToCart();
	
	/// <summary>
	/// 友盟数据分析之用户数据提交；
	/// </summary>
	/// <param name="type">Type. 数据类型字符串。</param>
	/// <param name="info">Info. 数据明细字符串。</param>
	void _UserActionData(string type, string info);
	
	/// <summary>
	/// 实体键返回
	/// </summary> 
	/// 实体 Back 返回键 行为 同 屏幕虚拟 Home 键行为相同； 
	void _KeyBackHome();
	
	/// <summary>
	/// 屏幕‘Home’按键
	/// </summary>
	void _BtnBackHome();

}
