using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;

public class _Hero : Singleton<_Hero> {


    public _Avator Current;

    string tempAvatorData = "{\"id\":\"\",\"app_code\":\"small\",\"name\":\"\u672A\u547D\u540D\",\"wallpaperIndex\":968,\"texLocalPath\":\"\",\"decorationlist\":[{\"id\":670,\"title\":\"LuffyHatTwo\",\"superficial_area\":13.45,\"decoration_type\":13,\"typeCode\":\"hat\",\"typeDescript\":\"\u5934\u9970-\u5E3D\u5B50\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/LuffyHatTwo/201509152102593694.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/LuffyHatTwo/201509152102596542.unity3d\",\"decorationfile_ios\":\"http://admin.3dmaker.cn/attachment/model/decoration/LuffyHatTwo/201509152102598678.unity3d\",\"isDefault\":true,\"decoration_status\":0,\"createTime\":1442322179,\"lastupDateTime\":0,\"remark\":\"0915\",\"version\":0,\"isHide\":false,\"isSelected\":true,\"internalID\":\"\"},{\"id\":390,\"title\":\"startHair\",\"superficial_area\":41.7,\"decoration_type\":1,\"typeCode\":\"hair\",\"typeDescript\":\"\u5934\u53D1\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startHair/201506251713034121.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startHair/201508101559320387.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223583,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":11,\"isHide\":true,\"isSelected\":false,\"internalID\":\"\"},{\"id\":291,\"title\":\"startEyebrows\",\"superficial_area\":0,\"decoration_type\":2,\"typeCode\":\"brows\",\"typeDescript\":\"\u7709\u6BDB\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startEyebrows/201506251711397935.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startEyebrows/201506301728372852.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223499,\"lastupDateTime\":0,\"remark\":\"\",\"version\":6,\"isHide\":true,\"isSelected\":false,\"internalID\":\"\"},{\"id\":287,\"title\":\"startEyes\",\"superficial_area\":0,\"decoration_type\":3,\"typeCode\":\"eyes\",\"typeDescript\":\"\u773C\u775B\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startEyes/201506251712242964.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startEyes/201506301731241154.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223544,\"lastupDateTime\":0,\"remark\":\"\",\"version\":7,\"isHide\":true,\"isSelected\":false,\"internalID\":\"\"},{\"id\":283,\"title\":\"startMouth\",\"superficial_area\":0,\"decoration_type\":4,\"typeCode\":\"mouth\",\"typeDescript\":\"\u5634\u5DF4\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startMouth/201506251713593223.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startMouth/201506301732461561.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223639,\"lastupDateTime\":0,\"remark\":\"\",\"version\":5,\"isHide\":true,\"isSelected\":false,\"internalID\":\"\"},{\"id\":416,\"title\":\"startUp\",\"superficial_area\":11.55,\"decoration_type\":6,\"typeCode\":\"upper\",\"typeDescript\":\"\u8863\u670D\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startUp/201506252003503145.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startUp/201508101602285199.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223754,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":14,\"isHide\":true,\"isSelected\":false,\"internalID\":\"\"},{\"id\":392,\"title\":\"StartHands\",\"superficial_area\":1,\"decoration_type\":8,\"typeCode\":\"righthand\",\"typeDescript\":\"\u53F3\u624B\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/StartHands/201507312038097237.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/StartHands/201508101601187129.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435906770,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":18,\"isHide\":true,\"isSelected\":false,\"internalID\":\"\"},{\"id\":414,\"title\":\"startPants\",\"superficial_area\":7.15,\"decoration_type\":9,\"typeCode\":\"bottom\",\"typeDescript\":\"\u88E4\u5B50\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startPants/201506251714561322.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startPants/201508101601450798.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223696,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":12,\"isHide\":true,\"isSelected\":false,\"internalID\":\"\"},{\"id\":398,\"title\":\"startShoes\",\"superficial_area\":0.6,\"decoration_type\":10,\"typeCode\":\"foots\",\"typeDescript\":\"\u978B\u5B50\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startShoes/201506251715283405.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startShoes/201508101602108568.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223728,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":7,\"isHide\":true,\"isSelected\":false,\"internalID\":\"\"}]}";
    string tempAvatorData_0 = "{\"id\":\"\",\"app_code\":\"small\",\"name\":\"\u672A\u547D\u540D\",\"wallpaperIndex\":968,\"texLocalPath\":\"20151126_103604.jpg\",\"decorationlist\":[{\"id\":670,\"title\":\"LuffyHatTwo\",\"superficial_area\":13.45,\"decoration_type\":13,\"typeCode\":\"hat\",\"typeDescript\":\"\u5934\u9970-\u5E3D\u5B50\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/LuffyHatTwo/201509152102593694.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/LuffyHatTwo/201509152102596542.unity3d\",\"decorationfile_ios\":\"http://admin.3dmaker.cn/attachment/model/decoration/LuffyHatTwo/201509152102598678.unity3d\",\"isDefault\":true,\"decoration_status\":0,\"createTime\":1442322179,\"lastupDateTime\":0,\"remark\":\"0915\",\"version\":0,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":390,\"title\":\"startHair\",\"superficial_area\":41.7,\"decoration_type\":1,\"typeCode\":\"hair\",\"typeDescript\":\"\u5934\u53D1\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startHair/201506251713034121.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startHair/201508101559320387.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223583,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":11,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":750,\"title\":\"YasutomoEyebrowsTwo\",\"superficial_area\":0,\"decoration_type\":2,\"typeCode\":\"brows\",\"typeDescript\":\"\u7709\u6BDB\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/YasutomoEyebrowsTwo/201509161518031428.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/YasutomoEyebrowsTwo/201509161518032104.unity3d\",\"decorationfile_ios\":\"http://admin.3dmaker.cn/attachment/model/decoration/YasutomoEyebrowsTwo/201509161518035103.unity3d\",\"isDefault\":true,\"decoration_status\":0,\"createTime\":1442387883,\"lastupDateTime\":0,\"remark\":\"0916\",\"version\":0,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":287,\"title\":\"startEyes\",\"superficial_area\":0,\"decoration_type\":3,\"typeCode\":\"eyes\",\"typeDescript\":\"\u773C\u775B\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startEyes/201506251712242964.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startEyes/201506301731241154.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223544,\"lastupDateTime\":0,\"remark\":\"\",\"version\":7,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":283,\"title\":\"startMouth\",\"superficial_area\":0,\"decoration_type\":4,\"typeCode\":\"mouth\",\"typeDescript\":\"\u5634\u5DF4\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startMouth/201506251713593223.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startMouth/201506301732461561.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223639,\"lastupDateTime\":0,\"remark\":\"\",\"version\":5,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":770,\"title\":\"FushimiUpOne\",\"superficial_area\":12.95,\"decoration_type\":6,\"typeCode\":\"upper\",\"typeDescript\":\"\u8863\u670D\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/FushimiUpOne/201509161533114102.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/FushimiUpOne/201509161533112244.unity3d\",\"decorationfile_ios\":\"http://admin.3dmaker.cn/attachment/model/decoration/FushimiUpOne/201509161533112630.unity3d\",\"isDefault\":true,\"decoration_status\":0,\"createTime\":1442388791,\"lastupDateTime\":0,\"remark\":\"0916\",\"version\":0,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":392,\"title\":\"StartHands\",\"superficial_area\":1,\"decoration_type\":8,\"typeCode\":\"righthand\",\"typeDescript\":\"\u53F3\u624B\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/StartHands/201507312038097237.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/StartHands/201508101601187129.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435906770,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":18,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":414,\"title\":\"startPants\",\"superficial_area\":7.15,\"decoration_type\":9,\"typeCode\":\"bottom\",\"typeDescript\":\"\u88E4\u5B50\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startPants/201506251714561322.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startPants/201508101601450798.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223696,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":12,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":398,\"title\":\"startShoes\",\"superficial_area\":0.6,\"decoration_type\":10,\"typeCode\":\"foots\",\"typeDescript\":\"\u978B\u5B50\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startShoes/201506251715283405.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startShoes/201508101602108568.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223728,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":7,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"}]}";
    string tempAvatorData_1 = "{\"id\":\"\",\"app_code\":\"small\",\"name\":\"\u672A\u547D\u540D\",\"wallpaperIndex\":966,\"texLocalPath\":\"20151126_103939.jpg\",\"decorationlist\":[{\"id\":670,\"title\":\"LuffyHatTwo\",\"superficial_area\":13.45,\"decoration_type\":13,\"typeCode\":\"hat\",\"typeDescript\":\"\u5934\u9970-\u5E3D\u5B50\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/LuffyHatTwo/201509152102593694.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/LuffyHatTwo/201509152102596542.unity3d\",\"decorationfile_ios\":\"http://admin.3dmaker.cn/attachment/model/decoration/LuffyHatTwo/201509152102598678.unity3d\",\"isDefault\":true,\"decoration_status\":0,\"createTime\":1442322179,\"lastupDateTime\":0,\"remark\":\"0915\",\"version\":0,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":390,\"title\":\"startHair\",\"superficial_area\":41.7,\"decoration_type\":1,\"typeCode\":\"hair\",\"typeDescript\":\"\u5934\u53D1\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startHair/201506251713034121.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startHair/201508101559320387.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223583,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":11,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":750,\"title\":\"YasutomoEyebrowsTwo\",\"superficial_area\":0,\"decoration_type\":2,\"typeCode\":\"brows\",\"typeDescript\":\"\u7709\u6BDB\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/YasutomoEyebrowsTwo/201509161518031428.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/YasutomoEyebrowsTwo/201509161518032104.unity3d\",\"decorationfile_ios\":\"http://admin.3dmaker.cn/attachment/model/decoration/YasutomoEyebrowsTwo/201509161518035103.unity3d\",\"isDefault\":true,\"decoration_status\":0,\"createTime\":1442387883,\"lastupDateTime\":0,\"remark\":\"0916\",\"version\":0,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":287,\"title\":\"startEyes\",\"superficial_area\":0,\"decoration_type\":3,\"typeCode\":\"eyes\",\"typeDescript\":\"\u773C\u775B\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startEyes/201506251712242964.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startEyes/201506301731241154.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223544,\"lastupDateTime\":0,\"remark\":\"\",\"version\":7,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":283,\"title\":\"startMouth\",\"superficial_area\":0,\"decoration_type\":4,\"typeCode\":\"mouth\",\"typeDescript\":\"\u5634\u5DF4\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startMouth/201506251713593223.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startMouth/201506301732461561.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223639,\"lastupDateTime\":0,\"remark\":\"\",\"version\":5,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":770,\"title\":\"FushimiUpOne\",\"superficial_area\":12.95,\"decoration_type\":6,\"typeCode\":\"upper\",\"typeDescript\":\"\u8863\u670D\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/FushimiUpOne/201509161533114102.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/FushimiUpOne/201509161533112244.unity3d\",\"decorationfile_ios\":\"http://admin.3dmaker.cn/attachment/model/decoration/FushimiUpOne/201509161533112630.unity3d\",\"isDefault\":true,\"decoration_status\":0,\"createTime\":1442388791,\"lastupDateTime\":0,\"remark\":\"0916\",\"version\":0,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":526,\"title\":\"KanekiHandTwo\",\"superficial_area\":1.9,\"decoration_type\":8,\"typeCode\":\"righthand\",\"typeDescript\":\"\u53F3\u624B\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/KanekiHandTwo/201508101716002344.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/KanekiHandTwo/201508111435129293.unity3d\",\"decorationfile_ios\":\"http://admin.3dmaker.cn/attachment/model/decoration/KanekiHandTwo/201510131552094122.unity3d\",\"isDefault\":true,\"decoration_status\":0,\"createTime\":1439198160,\"lastupDateTime\":0,\"remark\":\"0811-1\r\n1013 ios\",\"version\":1,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":414,\"title\":\"startPants\",\"superficial_area\":7.15,\"decoration_type\":9,\"typeCode\":\"bottom\",\"typeDescript\":\"\u88E4\u5B50\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startPants/201506251714561322.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startPants/201508101601450798.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223696,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":12,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"},{\"id\":398,\"title\":\"startShoes\",\"superficial_area\":0.6,\"decoration_type\":10,\"typeCode\":\"foots\",\"typeDescript\":\"\u978B\u5B50\",\"logoFile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startShoes/201506251715283405.png\",\"decorationfile\":\"http://admin.3dmaker.cn/attachment/model/decoration/startShoes/201508101602108568.unity3d\",\"decorationfile_ios\":\"\",\"isDefault\":false,\"decoration_status\":0,\"createTime\":1435223728,\"lastupDateTime\":0,\"remark\":\"0810\",\"version\":7,\"isHide\":false,\"isSelected\":false,\"internalID\":\"\"}]}";

    public TextAsset defaultAvatorTxt;

    _AvatorData _defaultAvatorData;
    public _AvatorData defaultAvatorData
    {
        get
        {
            if(_defaultAvatorData == null)
                _defaultAvatorData = JsonReader.Deserialize<_AvatorData>(defaultAvatorTxt.text);
            return _defaultAvatorData;
        }
    }

    public _AvatorData initAvatorData { set; get; }

    public _AvatorData currentAvatorData
    {
        get
        {
            return new _AvatorData()
            {
                id = AvatorID,
                texLocalPath = TexLocalPath,
                app_code = APP_CODE,
                name = AvatorName,
                wallpaperIndex = WallpaperIndex,
                decorationlist = Current.ItemList
            };
        }
    }

    public string   AvatorID {set; get;}
    public string   TexLocalPath { set; get; }
    const string    APP_CODE = "small";
	public string   AvatorName{set; get;}
    /*public int      WallpaperIndex { set; get; }*/
	int WallpaperIndex
    {
        get
        {
            return GetComponent<WallpaperHandler>().WallpaperID;
        }
    }
    
	bool IsLoaded{set; get;}

	private static GameObject _rootObj;

	public bool IsNew{
		get{
			if(initAvatorData == null)
				return true;
			return string.IsNullOrEmpty(initAvatorData.id) || string.IsNullOrEmpty(AvatorID);
		}
	}

	public bool IsEdited{
		get{
//			bool result = true;

			if(initAvatorData == null){
				Debug.Log ("initAvatorData IS NULL");
				return true;
			}

			if(!string.IsNullOrEmpty(initAvatorData.id) && !string.IsNullOrEmpty(AvatorID) && AvatorID != initAvatorData.id){
				Debug.Log ("ID IS NOT EQUALS");
				return true;
			}

			if(AvatorName != initAvatorData.name){
				Debug.Log ("NAME HAS EDITED");
				return true;
			}

			if(WallpaperIndex != initAvatorData.wallpaperIndex){
				Debug.Log ("wallpaperIndex HAS EDITED");
				return true;
			}

			_AvatorData curr = currentAvatorData;

			if(curr.decorationlist.Count != initAvatorData.decorationlist.Count){
				Debug.Log ("COUNT IS NOE EQUALS");
				return true;
			}

			curr.decorationlist.Sort();
			initAvatorData.decorationlist.Sort();

			for(int i=0; i<curr.decorationlist.Count; i++){
				_SourceItemData currItem = curr.decorationlist[i];
				_SourceItemData initItem = initAvatorData.decorationlist[i];
				if(!currItem.Equals(initItem))
					return true;
			}



			return false;
		}
	}

	bool IsWattingForUpload {set; get;}
	
	Action _callbackAction = null;


    public void TestInitialAvatorByString_First()
    {
        StartCoroutine(OnInitialAvator(tempAvatorData_0));
    }

    public void TestInitialAvatorByString_Second()
    {
        StartCoroutine(OnInitialAvator(tempAvatorData_1));
    }

    public void Awake()
	{

		/*defaultAvatorData = JsonReader.Deserialize<_AvatorData>(defaultAvatorTxt.text);*/

		IsLoaded = false;
		_rootObj = gameObject;

		DontDestroyOnLoad(_rootObj);
		
		StartCoroutine(BuildConfigTable());

        /*StartCoroutine(OnInitialAvator(tempAvatorData_1));*/

        /*StartCoroutine(OnInitialAvator(""));*/
    }

	IEnumerator BuildConfigTable(){

		LoadingView.Instance.OnStart(Color.white, 20F
		                             , 
		                             null, 
		                             () => {
										OnInitialAvator();
									}
		);

		_BasicInputManager.Instance.Init ();
		
		if(PlatformBridge.Instance != null)
			PlatformBridge.Instance.Init();

		_UIController.Instance.Init();
		
		Init ();
		
		_InternalDataManager.Instance.Init();
		
		_UnitPriceSetting.Instance.Init();

		while(!_InternalDataManager.Instance.IsLoaded || !_UnitPriceSetting.Instance.IsLoaded)
			yield return new WaitForFixedUpdate();

		Debug.Log ("Game Root init end at " + Time.frameCount+ "\n-----------------------------------------");

        /*OnInitialAvator();*/

        IsLoaded = true;
        
        yield return null;

	}


	public IEnumerator OnInitialAvator(string avatorJsonData){

		while(!IsLoaded)
			yield return new WaitForFixedUpdate();

		Debug.Log ("_Hero.OnInitialAvator(string):IEnumerator");

		Debug.Log ("Avatar Json String :" + avatorJsonData);

		LoadingView.Instance.OnStart(Color.white, 10F
		                             , 
		                             null, 
		                             () => {
										OnInitialAvator();
									}
		);

        if (string.IsNullOrEmpty(avatorJsonData))
        {
            OnInitialAvator();
            Debug.Log("OnInitialAvator()...");
            yield return null;
        }

		initAvatorData = JsonReader.Deserialize<_AvatorData>(avatorJsonData);

		

		if(initAvatorData == null || initAvatorData.decorationlist == null || initAvatorData.decorationlist.Count == 0
		   || string.IsNullOrEmpty(initAvatorData.name)){

            /*initAvatorData = LoadPlayerPrefs();*/
            /*initAvatorData = defaultAvatorData;*/
            OnInitialAvator();
            yield return null;

        }

        Debug.Log("Deserialize >>> Avator Data ToString() :" + initAvatorData.ToString());

        AvatorID = initAvatorData.id;
		AvatorName = initAvatorData.name;
        
        

        List<_SourceItemData> itemList = new List<_SourceItemData>();

		foreach(_SourceItemData item in _InternalDataManager.Instance.localConfigTable.FindAll(x => x.isSelected)){
			item.isSelected = false;
		}

		for(int i = 0; i < initAvatorData.decorationlist.Count; i++)
        {

			_SourceItemData item = initAvatorData.decorationlist[i];

			if(item == null){
				//nullIndices.Add (i);//nitAvatorData.decorationlist.IndexOf(item));
				continue;
			}

			if(!item.isDefault) {//This is the famous 'bool true = false!!!'.LOL
				//startIndices.Add (i);//nitAvatorData.decorationlist.IndexOf(item));
				continue;
			}



			_SourceItemData dest = _InternalDataManager.Instance.localConfigTable.Find( x => x.id == item.id);
            
			if(!dest.IsNull()){

                Debug.Log("OnInitial : " + dest.typeCode + " : " + dest.title);

                if (!dest.isDecorationLoaded){

					Debug.Log (dest.title + " is NOT Loaded!" + dest.PlatformDecoFile);

					if(string.IsNullOrEmpty(dest.PlatformDecoFile)){
						//nullIndices.Add (i);//nitAvatorData.decorationlist.IndexOf(item));
						continue;

					}

                    /*_InternalDataManager.Instance.DownloadItemAsset(dest);*/
                    StartCoroutine(dest.WaitForDownloadAsset());

					while(!dest.isDecorationLoaded)
						yield return new WaitForFixedUpdate();

				}
				itemList.Add (dest);
			}
            else
            {
				_SourceItemData temp = _InternalDataManager.Instance.localConfigTable.Find( x => x.title == item.title);
				if(!temp.IsNull()){

					Debug.Log (temp.title + "'s Temp Asset is NOT Loaded!" + temp.PlatformDecoFile);

					if(string.IsNullOrEmpty(temp.PlatformDecoFile)){
						//nullIndices.Add (i);//nitAvatorData.decorationlist.IndexOf(item));
						continue;
						
					}

                    /*_InternalDataManager.Instance.DownloadTempAsset(temp);*///, item.PlatformDecoFile);//.decorationfile);

                    StartCoroutine(temp.WaitForDownloadTemp());

					while(!temp.isTempLoaded)
						yield return new WaitForFixedUpdate();

					itemList.Add (temp);
				}
			}
		}

        //		foreach(int i in nullIndices){
        //			initAvatorData.decorationlist.RemoveAt(i);
        //		}

        //		for(int i = 0; i<startIndices.Count; i++){
        //			initAvatorData.decorationlist[startIndices[i]].isHide = true;
        //		}

        
        foreach (_SourceItemData item in itemList)
        {
            Debug.Log("Item TItle : " + item.title);
            item.isHide = true;
        }
			

		Current.OnInitial(itemList);

        

        /*yield return GetComponent<WallpaperHandler>().OnInitialWallpaper(WallpaperIndex);*/
        
        _SourceItemData wallpaperItem = _InternalDataManager.Instance.localConfigTable.Find(x => x.id == initAvatorData.wallpaperIndex);
        if (!wallpaperItem.IsNull())
        {
            if (!wallpaperItem.isDecorationLoaded)
            {

                Debug.Log(wallpaperItem.title + " is NOT Loaded!" + wallpaperItem.PlatformDecoFile);

                if (string.IsNullOrEmpty(wallpaperItem.PlatformDecoFile))
                {
                    //nullIndices.Add (i);//nitAvatorData.decorationlist.IndexOf(item));
                    //continue;
                    /*_InternalDataManager.Instance.DownloadItemAsset(dest);*/
                    
                }
                StartCoroutine(wallpaperItem.WaitForDownloadAsset());

                while (!wallpaperItem.isDecorationLoaded)
                    yield return new WaitForFixedUpdate();


            }
            GetComponent<WallpaperHandler>().OnUdateWallpaper(wallpaperItem, true, true);
            
        }
        else
        {
            initAvatorData.wallpaperIndex = 0;
            GetComponent<WallpaperHandler>().OnDefault();
        }

        _UIController.Instance.ShowMainView();

		LoadingView.Instance.OnFinish();

//         Debug.Log(string.Format("This is new ? : {0}", IsNew));
//         Debug.Log(string.Format("This is Edited ? : {0}", IsEdited));

        yield return null;
	}

	public void OnInitialAvator(){

		GetComponent<_RotatController>().BackToInitPoint();

		foreach(_SourceItemData item in _InternalDataManager.Instance.localConfigTable.FindAll(x => x.isSelected)){
			item.isSelected = false;
		}
        
		initAvatorData = defaultAvatorData;
		TexLocalPath = initAvatorData.texLocalPath;
		AvatorID = initAvatorData.id;
		AvatorName = initAvatorData.name;

        GetComponent<WallpaperHandler>().OnDefault();

        Current.OnDefault();

		_UIController.Instance.ShowMainView();

		LoadingView.Instance.OnFinish();

	}

	public void CloneCurrToInit(){
		initAvatorData = currentAvatorData;
	}

	#region Upload Callback


	public void SaveToFactory(Action callbackAct, bool isWaitting)//, bool needsCaptureBG)
    {

		IsWattingForUpload = isWaitting;
		_callbackAction = callbackAct;
        
        StartCoroutine(WaitForUploadCallback());

	}

	IEnumerator WaitForUploadCallback()
	{
        /*TexLocalPath = Camera.main.GetComponent<_MainCameraController>().OnCapturePicture();*/

        GetComponentInChildren<_DanceHandler>().DoPose();
        
        if (PlatformBridge.Instance != null)
        {
            if (_UIController.Instance.IsDanceView && _UIController.Instance.TempModelData != null && !_UIController.Instance.TempModelData.IsNull())
            {
                PlatformBridge.Instance._UploadModelConfig(
                    _UIController.Instance.TempModelData.ItemIDs,
                    _UIController.Instance.TempModelData.AvatorData,
                    _UIController.Instance.TempModelData.Volume);
            }
            else
            {
                TexLocalPath = Camera.main.GetComponent<_MainCameraController>().OnCapturePicture();
                PlatformBridge.Instance._UploadModelConfig(GetCurrentItemIds(), GetCurrentAvatorData(), GetCurrentVolume());
            }
        }

            Debug.Log("Upload Avatar Data : " + GetCurrentAvatorData());
		
		while(IsWattingForUpload)
			yield return new WaitForFixedUpdate();

		yield return null;
				
	}

	public void HandleOnUploadFinish (string result)
	{
		AvatorID = result;
		CloneCurrToInit();
		if(_callbackAction != null){
			_callbackAction();
		}
		IsWattingForUpload = false;
	}
	#endregion

	public void HandleOnUpdateAccessory(_SourceItemData item, bool toSelect, bool isPlayable){

		Vector3 curAngles = transform.eulerAngles;
		GetComponent<_RotatController>().BackToInitPoint();
		Current.UpdateAccessory(item, toSelect, isPlayable, false);
		transform.eulerAngles += curAngles;

//         Debug.Log( string.Format("initAvatorData ID IsNullOrEmpty : {0}, AvatorID IsNullOrEmpty :{1};", 
//             string.IsNullOrEmpty(initAvatorData.id), string.IsNullOrEmpty(AvatorID)));
//         Debug.Log(string.Format("This is new ? : {0}", IsNew));
//         Debug.Log(string.Format("This is Edited ? : {0}", IsEdited));
//         Debug.Log("\n-----------------IPHONE--------------------\n");
//         Debug.Log(currentAvatorData.ToString());
//         Debug.Log("\n-----------------ANDROID--------------------\n");
//         Debug.Log(JsonWriter.Serialize(currentAvatorData));
    }

	public string GetCurrentItemIds(){
		return Current.ItemIDs;
	}

	public string GetCurrentAvatorData(){
//		return  JsonFx.Json.JsonWriter.Serialize(currentAvatorData);
//		return currentAvatorData.ToString();
		if(Application.platform == RuntimePlatform.IPhonePlayer)
			return currentAvatorData.ToString();
		else
			return JsonWriter.Serialize(currentAvatorData);
	}

	public float GetCurrentVolume(){
		return Current.Volume;
	}

	public override void Init(){

	}

	public override void Release(){

		Current.OnRelease();
        GetComponent<WallpaperHandler>().OnRelease();

	}

	void OnEnable(){
		_UIController.OnUpdateAccessory += HandleOnUpdateAccessory;
        /*_UIController.OnEnterDanceView += HandleOnEnterDanceView;
        _UIController.OnExitDanceView += HandleOnExitDanceView;*/
    }

	void OnDisable(){
		_UIController.OnUpdateAccessory -= HandleOnUpdateAccessory;
        /*_UIController.OnEnterDanceView -= HandleOnEnterDanceView;
        _UIController.OnExitDanceView -= HandleOnExitDanceView;*/
    }

    /*private void HandleOnExitDanceView()
    {
        Current.OnResetHands();
    }

    private void HandleOnEnterDanceView()
    {
        Current.OnInitialHands();
    }*/

    /// <summary>
    /// Clear those two points:
    /// _BtnBackHome (221):    _Hero.Instance.SavePlayerPrefs();
    ///_KeyBackHome (211):    _Hero.Instance.SavePlayerPrefs();//SavePlayerPrefs
    /// </summary>
    /*
    public void SavePlayerPrefs()
    {

        Debug.Log("Save Player Prefs");

        _InternalDataCodec.SaveSerializableData<_AvatorData>(currentAvatorData, _InternalDataManager.Instance._PlayerPrefsPath);

    }

    _AvatorData LoadPlayerPrefs(){
		if(File.Exists(_InternalDataManager.Instance._PlayerPrefsPath)){//_PlayerPrefsPath)){
			return _InternalDataCodec.LoadSerializableData<_AvatorData>(_InternalDataManager.Instance._PlayerPrefsPath);
		}
		return defaultAvatorData;
		
	}
    */

}
