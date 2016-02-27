using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class _DefaultDataBuilder : MonoBehaviour {

	public bool isTestEnvironment = false;
	public bool isIOSPlatform = false;

	string _ServerJsonFilePath;
	string _DefaultJsonFilePath;
	string _DafultDataFilePath;
	_ConfigTable tempConfigTable;

	void Start()
	{

		if(isTestEnvironment)
			_ServerJsonFilePath =  "http://mk1.ulifang.com/api/shop/decoration!getDecorationData.do?app_code=small";
		else
			_ServerJsonFilePath =  "http://www1.3dmaker.cn/api/shop/decoration!getDecorationData.do?app_code=small";

		_DefaultJsonFilePath = Application.dataPath + "/StreamingAssets/default_jason_file.txt";

		_DafultDataFilePath = Application.dataPath + "/StreamingAssets/LocalDatabase/";

		if(!Directory.Exists(_DefaultJsonFilePath)){
			Directory.CreateDirectory(_DafultDataFilePath + "UI/");
			Directory.CreateDirectory(_DafultDataFilePath + "Asset/");
		}
//#if UNITY_EDITOR
		BuildDefaultAssets();
//#endif

	}

	void BuildDefaultAssets(){
		
		StartCoroutine(new WWWProxy(){
			
			url = _ServerJsonFilePath,
			feedback = delegate(WWW www) {
				
				//				Debug.Log("Json File: " + www.text);
				
				tempConfigTable = JsonFx.Json.JsonReader.Deserialize<_ConfigTable>(www.text);
				
				if(tempConfigTable == null){
					//					Debug.LogError("DefaultConfigTable is NULL!!!");
					//TODO....
					return;
				}

				tempConfigTable.RemoveAll (x => (x.isDefault | x.decoration_status == 1));
				
				foreach(_SourceMenuData menu in tempConfigTable.data){
					DownloadDefaultData(menu);
					
				}
			}
		}.WaitForFinished());
		
		
		
		
	}
//	
	void DownloadDefaultData(_SourceMenuData menu){
		foreach(_SourceItemData item in menu.data){
			DownloadDefaultData(item);
		}
		if(menu.children.Count > 0)
			foreach(_SourceMenuData subMenu in menu.children)
				DownloadDefaultData(subMenu);
	}
//	
	void DownloadDefaultData(_SourceItemData item){

		//string saveDefaultPath = Application.dataPath + "/StreamingAssets/LocalDatabase/";
		
		//		Debug.Log("Asset URL: " + item.PlatformDecoFile);//.decorationfile);
		
		StartCoroutine(new WWWProxy(){
			
			//			waitTime = 5.0f,
			url = item.logoFile,
			feedback = delegate(WWW www){
				
				
				_InternalDataCodec.SaveTexture(_DafultDataFilePath + "UI/" + item.title + ".png", www.texture, true);
				//				item.isUIImageLoaded = true;
//				item.UIImageLocalPath = _DafultDataFilePath + "UI/" + item.title + ".png";
//				item.isUIImageLoaded = true;
				
				
				
//				SaveDefaultConfigTable();
				_InternalDataCodec.SaveString(JsonFx.Json.JsonWriter.Serialize(tempConfigTable), _DefaultJsonFilePath);
				//				_InternalDataCodec.SaveSerializableData<string>(WriteFileTool.ClassToJson<_ConfigTable>(defaultConfigTable), 
				//				                                                Application.dataPath + "/Resources/default_config_json.txt");
			}}.WaitForFinished());
		
		StartCoroutine(new WWWProxy(){
			
			//			waitTime = 5.0f,
			url = isIOSPlatform ? item.decorationfile_ios : item.decorationfile,//.
			feedback = delegate(WWW www){
				
				
				
				_InternalDataCodec.SaveBytes(_DafultDataFilePath + "Asset/" + item.title + ".unity3d", www.bytes);
				//				item.isDecorationLoaded = true;
				//item.DecorationLocalPath = _DafultDataFilePath + "Asset/" + item.title + ".unity3d";
				
				//				item.State = itemState.notSelect;
				
//				SaveDefaultConfigTable();
				_InternalDataCodec.SaveString(JsonFx.Json.JsonWriter.Serialize(tempConfigTable), _DefaultJsonFilePath);
				//				_InternalDataCodec.SaveSerializableData<string>(WriteFileTool.ClassToJson<_ConfigTable>(defaultConfigTable), 
				//				                                                Application.dataPath + "/Resources/default_config_json.txt");
			}}.WaitForFinished());
	}
}
