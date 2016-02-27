using System.Collections;
using System.Collections.Generic;
// using UnityEngine.UI;
// using System;
// using System.Text;
using System.IO;
using UnityEngine;

/// <summary>
/// _ internal data manager.
/// 内部数据管理：
/// 20150716更新内容：
/// 1.初装模型及UI文件由Android端工程解压并存放在 androidDatabasePath 绝对路径下(即可替代原 LocalDatabasePath 属性)；
/// 2.
/// </summary>
public class _InternalDataManager : Singleton<_InternalDataManager> {

//	public string APP_NAME = "small";
	// test environment
	public bool isTestEnvironment = false;

	public TextAsset defaultConfigTxt;

	/// <summary>
	/// MainFunction: 保存 预置配饰 内容。
	/// 	本配置表仅在 首次安装 或 清除缓存后， 通过读取本地 streamingAssets 文件夹 json 数据生成；
	/// 	保存在 persistentDataPath/default_config_table.gd 文件内；
	/// 	在销毁（清除缓存或版本更新）前，始终保持不变；
	/// </summary>
	/// <value>The default config table.</value>
	private _ConfigTable defaultConfigTable;//{private set; get;}

	/// <summary>
	/// MainFunction: 临时保存服务端 所有上架配饰 内容。
	/// 	本配置表在 每次运行初始阶段， 通过下载服务器端 json 数据生成；
	/// </summary>
	/// <value>The server config table.</value>
	private _ConfigTable serverConfigTable;//{private set; get;}

	/// <summary>
	/// MainFunction: 保存本地配饰内容状态。
	/// 	本配置表仅在 首次安装 或 清除缓存后， 通过合并 defaultConfigTable 和 serverConfigTable 数据生成；
	/// 	保存在 persistentDataPath/local_config_table.gd 文件内；
	/// 	在每次运行时 更新 服务端差异部分 数据文件；
	/// 	在每次下载配饰模型文件后，更新状态；
	/// </summary>
	/// <value>The local config table.</value>
	public _ConfigTable localConfigTable{private set; get;}

//	public _ConfigTable summaryConfigTable{private set; get;}



	public bool IsLoaded{private set; get;}


	
	/*public string _DefaultJsonFilePath{set; get;}*/
//	public string _DefaultAvatorDataPath{set; get;}
//	public string _DefaultAvatorJsonPath{set; get;}
//	public string _TPoseFilePath{set; get;}

// 	public string _DefaultRootDirectory{set; get;}
// 	public string _DefaultUIDirectory{set; get;}
// 	public string _DefaultAssetDirectory{set; get;}

	public string _ServerJsonFilePath{set; get;}


	public string _LocalRootDirectory{set; get;}
	public string _LocalDatabaseDirectory{set; get;}
//	public string _LocalRootDirectory{set; get;}
	public string _LocalUIDirectory{set; get;}
	public string _LocalAssetDirectory{set; get;}
	public string _LocalTempDirectory{set; get;}

	public string _DefaultConfigTablePath{set; get;}
	public string _LocalConfigTablePath{set; get;}

	public string _CapturePicturePath{set; get;}

	public string _PlayerPrefsPath{set; get;}

	//{"result":1,"data":{"freight":"6","packing_price":"10","unit_price":"19"}}


//	public string _ServerConfigTablePath{set; get;}

//	StringBuilder sb = new StringBuilder("Internal Database finished! \n");
//	public Text LogText;
//
//	public void AppendLog(string log){
//		sb.Append(log + "\n");
//		LogText.text = sb.ToString();
//	}
//
//	public void ClearLog(string log){
//		sb = new StringBuilder(log + "\n");
//		LogText.text = sb.ToString();
//	}
	void Awake(){
		// Forces a different code path in the BinaryFormatter that doesn't rely on run-time code generation (which would break on iOS).
//		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	public override void Init()
	{
//		AppendLog("_InternalDataManager Init is Start !!!");
//		DefaultItemIds = new int[]{146, 151, 155, 157, 257, 260, 193, 156};

//		IsLoaded = false;



		BuidInternalDirectory();

		StartCoroutine(BuildConfigTable());

	}

	void BuidInternalDirectory(){

// 		_DefaultRootDirectory =// Path.Combine(Application.streamingAssetsPath, "LocalDatabase");//(Application.streamingAssetsPath, "LocalDatabase");//
// 			#if UNITY_EDITOR
// 			Application.dataPath + "/StreamingAssets/"
// 			#elif UNITY_ANDROID
// 			"jar:file://" + Application.dataPath + "!/assets/"
// 			#elif UNITY_IPHONE
// 			Application.dataPath + "/Raw/"
// 			#elif UNITY_STANDALONE_WIN
// 			Application.dataPath + "/StreamingAssets/"
// 			#else
// 			string.Empty
// 			#endif
// 			+ "LocalDatabase";
// 
// 		#if UNITY_EDITOR
// 		if(!Directory.Exists(_DefaultRootDirectory))
// 			Directory.CreateDirectory(_DefaultRootDirectory);
// 		#endif
// 		
// 		_DefaultUIDirectory = Path.Combine(_DefaultRootDirectory, "UI");
// 		#if UNITY_EDITOR
// 		if(!Directory.Exists(_DefaultUIDirectory))
// 			Directory.CreateDirectory(_DefaultUIDirectory);
// 		#endif
// 
// 		_DefaultAssetDirectory = Path.Combine(_DefaultRootDirectory, "Asset");
// 		#if UNITY_EDITOR
// 		if(!Directory.Exists(_DefaultAssetDirectory))
// 			Directory.CreateDirectory(_DefaultAssetDirectory);
// 		#endif

		/*_DefaultJsonFilePath = Path.Combine(_DefaultRootDirectory, "default_jason_file.txt");*/

		_LocalRootDirectory = CombineAndCreateDirectory(Application.persistentDataPath, "raw");

		_CapturePicturePath = _LocalRootDirectory;

		_LocalDatabaseDirectory = CombineAndCreateDirectory(_LocalRootDirectory, "LocalDatabase");

		_LocalUIDirectory = CombineAndCreateDirectory(_LocalDatabaseDirectory, "UI");

		_LocalAssetDirectory = CombineAndCreateDirectory(_LocalDatabaseDirectory, "Asset");

		_LocalTempDirectory = CombineAndCreateDirectory(_LocalDatabaseDirectory, "Temp");

		_DefaultConfigTablePath = Path.Combine(_LocalDatabaseDirectory,  "default_config_table.gd");
		_LocalConfigTablePath = Path.Combine(_LocalDatabaseDirectory,  "local_config_table.gd");
		_PlayerPrefsPath = Path.Combine(_LocalDatabaseDirectory,  "player_prefs.gd");

		if(isTestEnvironment)
			_ServerJsonFilePath =  "http://mk1.ulifang.com/api/shop/decoration!getDecorationData.do?app_code=small";
		else
			_ServerJsonFilePath =  "http://www1.3dmaker.cn/api/shop/decoration!getDecorationData.do?app_code=small";

	}

    string CombineAndCreateDirectory(string parentDir, string subDir)
    {
        string result = Path.Combine(parentDir, subDir);  
        if(!Directory.Exists(result))
        {
            Directory.CreateDirectory(result);
        }
        return result;
    }

	public IEnumerator BuildConfigTable(){

		IsLoaded = false;

		defaultConfigTable = _InternalDataCodec.LoadSerializableData<_ConfigTable>(_DefaultConfigTablePath);

		if(defaultConfigTable == null){

//			AppendLog("Android Database UnZip --- begin :");

			defaultConfigTable = JsonFx.Json.JsonReader.Deserialize<_ConfigTable>(defaultConfigTxt.text);

//#if UNITY_IPHONE
//			TransferConfigTable(defaultConfigTable, _DefaultRootDirectory, _LocalDatabaseDirectory);
//#endif

//			while(!WWWProxy.IsDone)
//				yield return new WaitForFixedUpdate();

			SaveDefaultConfigTable();
		}

		localConfigTable = _InternalDataCodec.LoadSerializableData<_ConfigTable>(_LocalConfigTablePath);

		if(localConfigTable == null){
			
			localConfigTable = defaultConfigTable;
			SaveLocalConfigTable();
		}

		/*Debug.Log ("InternetReachability : " + Application.internetReachability.ToString());*/

		if(Application.internetReachability == NetworkReachability.NotReachable){
			SaveLocalConfigTable();
			IsLoaded = true;
			yield return null;
		}
		
		StartCoroutine(new WWWProxy(){
			
			url = _ServerJsonFilePath,
			
			feedback = www => {
				
				serverConfigTable = JsonFx.Json.JsonReader.Deserialize<_ConfigTable>(www.text);
            //过滤临时表
            /// 	"isDefault":1, 是否预置配饰 为 ‘否’;
            /// 	"isDefault":0, 是否预置配饰 为 ‘是’;
            /// 	"decoration_status":1, 状态 为 ‘下架’；
            /// 	"decoration_status":0, 状态 为 ‘上架’；
            serverConfigTable.RemoveAll(x => (x.decoration_status == 1));// || !x.isDefault));
			}
			
		}.WaitForFinished());
		
		while(!WWWProxy.IsDone)
			yield return new WaitForFixedUpdate();
		
//		Debug.Log ("Server Config Table Transfer --- end\n Server Config Table is null? " + (serverConfigTable == null).ToString());
//		AppendLog("Server Config Table Transfer --- end : " +(serverConfigTable == null ? " NULL !!!" : "Exits! ") + Time.frameCount);
		
		
		if(serverConfigTable == null || serverConfigTable.data == null || serverConfigTable.data.Count == 0){
			
//			Debug.Log("Server Config Table is NOT Exist!!! There must be another BIG Mistake!!!");
			SaveLocalConfigTable();
			IsLoaded = true;
			yield return null;
		}
		
		if(localConfigTable.Equals(serverConfigTable)){
			
//			Debug.Log("Nothing is changed!!! Just GO!GO!GO!!!");
			IsLoaded = true;
			yield return null;
			
		}else{
//			Debug.Log ("Compare server to local --- begin ");
			if(serverConfigTable != null && serverConfigTable.data != null && localConfigTable != null && localConfigTable.data != null)
				CompareMenuData(serverConfigTable.data, localConfigTable.data);
			//SaveLocalConfigTable();
//			Debug.Log ("Compare server to local --- end ");
		}

		while(!WWWProxy.IsDone)
			yield return new WaitForFixedUpdate();
		
		SaveLocalConfigTable();
		
		IsLoaded = true;
		yield return null;
	}

    /*
        void RemarkItemLocalPath(_ConfigTable table, string directory){

            string[] uiFileEntris = Directory.GetFiles(directory + "/UI");
            foreach(string fileName in uiFileEntris){
    //			AppendLog("UI :" + fileName);			
                string name = Path.GetFileNameWithoutExtension(fileName);
                _SourceItemData item = table.Find( x => x.title == name);
    //			AppendLog("Name :" + name);
    //			if(item != null)
    //				item.isUIImageLoaded = true;
    //				item.UIImageLocalPath = fileName;

            }

            string[] assetFileEntris = Directory.GetFiles(directory + "/Asset");
            foreach(string fileName in assetFileEntris){
    //			AppendLog("Asset :" + fileName);
                string name = Path.GetFileNameWithoutExtension(fileName);
                _SourceItemData item = table.Find( x => x.title == name);
    //			if(item != null)
    //				item.DecorationLocalPath = fileName;
            }

            SaveDefaultConfigTable();
        }*/

    void SaveDefaultConfigTable(){
		_InternalDataCodec.SaveSerializableData<_ConfigTable>(defaultConfigTable, _DefaultConfigTablePath);
	}

	void SaveLocalConfigTable(){
		_InternalDataCodec.SaveSerializableData<_ConfigTable>(localConfigTable, _LocalConfigTablePath);
	}

	void CompareMenuData(List<_SourceMenuData> sourceMenus, List<_SourceMenuData> destMenus){
		if(sourceMenus == null || destMenus == null || (sourceMenus.Count == 0 && destMenus.Count == 0))
			return;

		foreach(_SourceMenuData menu in destMenus.FindAll( x => !sourceMenus.Contains(x))){

			DeletaMenuData (menu);
			destMenus.Remove (menu);

			SaveLocalConfigTable();
		}

		foreach(_SourceMenuData menu in sourceMenus){
//			Debug.Log(menu.type + "-------");
			_SourceMenuData dest = destMenus.Find(x => menu.Equals(x));
//			Debug.Log("find menu.equals(x)" + dest.type);
            //Debug.Log (dest.type + ", " + menu.type);
//			List<_SourceMenuData> dests = destMenus.FindAll(x => menu.Equals(x));

			if(dest == null){
//				int index = destMenus.IndexOf(menu);

				DownloadMenuUI(menu);
				destMenus.Add (menu);

//				SaveLocalConfigTable();

			}else{

				CompareItemData(menu.data, dest.data);// destMenus[index]);
				if(menu.children.Count > 0)
					CompareMenuData(menu.children, dest.children);

//				SaveLocalConfigTable();
			}

		}

	}

	void CompareItemData(List<_SourceItemData> sourceItems, List<_SourceItemData> destItems){

		foreach(_SourceItemData item in destItems.FindAll (x => !sourceItems.Contains(x))){
//			Debug.Log("destItems.FindAll (x => !sourceItems.Contains(x))" + item.title);
			DeletaItemData(item);
			destItems.Remove (item);

//			SaveLocalConfigTable();
//			bool isDeleted = destItems.Remove (item);
			/*Debug.Log(item.title + " is Deleted, " + !destItems.Contains(item));*/
		}

		foreach(_SourceItemData item in sourceItems.FindAll(x => !destItems.Contains(x))){
            /// DownLoad UI Texture from server
            //			Debug.Log("sourceItems.FindAll(x => !destItems.Contains(x))" + item.title);
            /*DownloadItemUI(item);*/
            StartCoroutine(item.WaitForDownloadUI());
            destItems.Add (item);

//			SaveLocalConfigTable();
			/*Debug.Log(item.title + " is Added, " + destItems.Contains(item));*/
		}

	}

	void DownloadConfigTableUI(_ConfigTable table){
		foreach(_SourceMenuData menu in table.data)
			DownloadMenuUI(menu);
	}

	void DownloadMenuUI(_SourceMenuData menu){
		foreach(_SourceItemData item in menu.data){
            /*DownloadItemUI(item);*/
            StartCoroutine(item.WaitForDownloadUI());
		}
		if(menu.children.Count > 0)
			foreach(_SourceMenuData subMenu in menu.children)
				DownloadMenuUI(subMenu);
	}

/*
	void DownloadItemUI(_SourceItemData item){

//		string uiLocalPath = Path.Combine(_LocalUIDirectory , item.title + ".png");
		//return Path.Combine(_InternalDataManager.Instance._LocalUIDirectory, title + ".png");
		StartCoroutine(new WWWProxy(){

			url = item.logoFile,
			feedback = delegate(WWW www){
				_InternalDataCodec.SaveTexture(item.UIImageLocalPath, www.texture, true);
//				item.isUIImageLoaded = true;
//				item.UIImageLocalPath = uiLocalPath;
//				SaveLocalConfigTable();

			}}.WaitForFinished());

	}*/

	/*public void DownloadItemAsset(_SourceItemData item)
	{
//		string assetLocalPath = Path.Combine(_LocalAssetDirectory ,item.title + ".unity3d");

//		Debug.Log ("Asset Local Path : " + assetLocalPath);
		
		StartCoroutine(new WWWProxy(){
			url = item.PlatformDecoFile,//.decorationfile,
			feedback = delegate(WWW www){

				_InternalDataCodec.SaveBytes(item.DecorationLocalPath, www.bytes);
//				item.DecorationLocalPath = assetLocalPath;
//				SaveLocalConfigTable();
				
			}}.WaitForFinished());
	}*/

	/*public void DownloadTempAsset(_SourceItemData item)//, string tempAssetUrl)
	{
//		string assetLocalPath = Path.Combine(_LocalTempDirectory ,item.title + ".unity3d");
		
		StartCoroutine(new WWWProxy(){
			url = item.PlatformDecoFile,//tempAssetUrl,
			feedback = delegate(WWW www){
				
				_InternalDataCodec.SaveBytes(item.TempLocalPath, www.bytes);
//				item.TempLocalPath = assetLocalPath;
//				SaveLocalConfigTable();
				
			}}.WaitForFinished());
	}*/

	public void DeleteTempAsset(_SourceItemData item){
		_InternalDataCodec.DeleteLocalData(item.TempLocalPath);
//		item.TempLocalPath = string.Empty;
		SaveLocalConfigTable();
	}

	/*void TransferConfigTable(_ConfigTable table, string fromDir, string toDir){
		foreach(_SourceMenuData menu in table.data)
			TransferMenuData(menu, fromDir , toDir);
	}

	void TransferMenuData(_SourceMenuData menu, string fromDir, string toDir){

		if(menu.children.Count > 0){
			foreach(_SourceMenuData subMenu in menu.children){
				TransferMenuData(subMenu, fromDir, toDir);
			}
		}else{
			foreach(_SourceItemData item in menu.data){
				TransferItemData(item, fromDir, toDir);
			}
		}
	}

	void TransferItemData(_SourceItemData item, string fromDir, string toDir){

//		string path_0 = Path.Combine(Application.dataPath,  "/../LocalDatabase/UI/"+ item.title + ".png");
//		if(File.Exists(path_0)){
//			Debug.Log("Application.dataPath is Exists !");
//		}
//
//		string path_1 =  "file://" + Application.dataPath + "/../LocalDatabase/UI/"+ item.title + ".png";
//		if(File.Exists(path_1)){
//			Debug.Log("File:// Path is Exists !");
//		}

		StartCoroutine(new WWWProxy(){
//			url = fromDir + "/UI/" + item.title + ".png",
			url =  "file://" + Application.dataPath + "/../LocalDatabase/UI/"+ item.title + ".png",
			feedback = delegate(WWW www){
				
				_InternalDataCodec.SaveTexture(item.UIImageLocalPath, www.texture, true);
//				item.UIImageLocalPath = toDir + "/UI/" + item.title + ".png";
//				item.isUIImageLoaded = true;

				SaveDefaultConfigTable();
				Debug.Log("Transfer UI :" + item.title);
			}}.WaitForFinished());

		StartCoroutine(new WWWProxy(){
//			url = fromDir + "/Asset/" + item.title + ".unity3d",
			url =   "file://" + Application.dataPath + "/../LocalDatabase/Asset/"+ item.title + ".unity3d",
			feedback = delegate(WWW www){
				
				_InternalDataCodec.SaveBytes(item.DecorationLocalPath, www.bytes);
				//item.DecorationLocalPath = toDir + "/Asset/" + item.title + ".unity3d";
				SaveDefaultConfigTable();
				Debug.Log("Transfer Assets : " + item.title);
			}}.WaitForFinished());

		/ *
#if UNITY_EDITOR


		_InternalDataCodec.TransferBytes(fromDir + "/Asset/" + item.title + ".unity3d", toDir + "/Asset/" + item.title + ".unity3d");
//		item.DecorationLocalPath = toDir + "/Asset/" + item.title + ".unity3d";
		SaveDefaultConfigTable();
		
#else

		StartCoroutine(new WWWProxy(){
			url = fromDir + "/Asset/" + item.title + ".unity3d",
			feedback = delegate(WWW www){

				_InternalDataCodec.SaveBytes(toDir + "/Asset/" + item.title + ".unity3d", www.bytes);
				//item.DecorationLocalPath = toDir + "/Asset/" + item.title + ".unity3d";
				SaveDefaultConfigTable();
//				Debug.Log("Transfer Assets : " + item.title);
			}}.WaitForFinished());

#endif

* /
	}*/

	void DeletaMenuData(_SourceMenuData menu){
		foreach(_SourceItemData item in menu.data)
			DeletaItemData(item);
		menu.data.Clear();
		if(menu.children.Count > 0){
			foreach(_SourceMenuData subMenu in menu.children)
				DeletaMenuData(subMenu);
			menu.children.Clear();
		}
	}

	void DeletaItemData(_SourceItemData item){
		if(item.isUIImageLoaded){
			_InternalDataCodec.DeleteLocalData(item.UIImageLocalPath);
//			item.UIImageLocalPath = string.Empty;
//			item.isUIImageLoaded = false;
		}
		if(item.isDecorationLoaded){
			_InternalDataCodec.DeleteLocalData(item.DecorationLocalPath);
//			item.DecorationLocalPath = string.Empty;
		}
	}

	public override void Release()
	{
		//TODO
	}

}
