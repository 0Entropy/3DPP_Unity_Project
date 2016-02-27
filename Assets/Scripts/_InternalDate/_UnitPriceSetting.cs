using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class _UnitPriceSetting : Singleton<_UnitPriceSetting> {

	public bool isTestEnvironment = false;

	public TextAsset defaultUnitPrice;

	//每立方厘米材料费用
	const float COST_MATERIAL_PER_CUBIC_CM = 14.50F;//
	//每包装费用
	const float COST_PACKAGE_PER_UNIT = 10.00F;
	//运费
	const float Cost_TRANSFER_PER_UNIT = 10.00F;

	public bool IsLoaded{set; get;}

	void Awake(){
// 		Forces a different code path in the BinaryFormatter that doesn't rely on run-time code generation (which would break on iOS).
//		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	float CostMaterialPerCubicCm{
		get{
			if(localUnitPrice == null || localUnitPrice.data.unit_price == 0)
				return COST_MATERIAL_PER_CUBIC_CM;
			else
				return localUnitPrice.data.unit_price;
		}
	}
	
	float CostPackagePerUnit{
		get{
			if(localUnitPrice == null || localUnitPrice.data.packing_price == 0)
				return COST_PACKAGE_PER_UNIT;
			else
				return localUnitPrice.data.packing_price;
		}
	}
	
	float CostTransferPerUnit{
		get{
			if(localUnitPrice == null || localUnitPrice.data.freight == 0)
				return Cost_TRANSFER_PER_UNIT;
			else
				return localUnitPrice.data.freight;
		}
	}

	public float CalcuUnitPrice(float volume){
		return (CostMaterialPerCubicCm * volume + CostPackagePerUnit + CostTransferPerUnit);
	}

	string _ServerPriceFilePath{set; get;}
	string _LocalPriceFilePath{set; get;}
	_UnitPrice localUnitPrice{set; get;}

	public override void Init()
	{

		_LocalPriceFilePath = Path.Combine(_InternalDataManager.Instance._LocalDatabaseDirectory, "local_price_file.gd");

		if(isTestEnvironment)
			_ServerPriceFilePath =  "http://mk1.ulifang.com/api/shop/setting!getPriceSetting.do";
		else
			_ServerPriceFilePath = "http://www1.3dmaker.cn/api/shop/setting!getPriceSetting.do";

		StartCoroutine(GetServerPriceSetting());

	}

	IEnumerator GetServerPriceSetting(){

		IsLoaded = false;

		localUnitPrice = _InternalDataCodec.LoadSerializableData<_UnitPrice>(_LocalPriceFilePath);
		if(localUnitPrice == null){
			localUnitPrice = JsonFx.Json.JsonReader.Deserialize<_UnitPrice>(defaultUnitPrice.text);
			_InternalDataCodec.SaveSerializableData<_UnitPrice>(localUnitPrice, _LocalPriceFilePath);
		}

		if(Application.internetReachability == NetworkReachability.NotReachable){
			Debug.Log ("NetworkReachability NotReachable !");
			IsLoaded = true;
			yield return null;
		}

		_UnitPrice serverUnitPrice = null;

		StartCoroutine(new WWWProxy(){
			url = _ServerPriceFilePath,
			feedback = www => {
				serverUnitPrice = JsonFx.Json.JsonReader.Deserialize<_UnitPrice>(www.text);

			}
		}.WaitForFinished());

		while(!WWWProxy.IsDone)
			yield return new WaitForFixedUpdate();

		if(serverUnitPrice == null){
			IsLoaded = true;
			yield return null;
		}

		if(localUnitPrice == serverUnitPrice){
			IsLoaded = true;
			yield return null;
		}else{
			localUnitPrice = serverUnitPrice;
			_InternalDataCodec.SaveSerializableData<_UnitPrice>(localUnitPrice, _LocalPriceFilePath);

		}

		IsLoaded  = true;
		yield return null;

	}

	public override void Release()
	{
		//TODO
	}
}

//{"result":1,"data":{"freight":"10","packing_price":"10","unit_price":"18"}}
[Serializable]
public class _UnitPrice : IEquatable<_UnitPrice>{
	public int result{set; get;}
	public _PriceData data{set; get;}

	#region Equatable
	public override bool Equals(object obj)
	{
		if(obj == null) return false;
		_UnitPrice other = obj as _UnitPrice;
		if(other == null) return false;
		else return Equals(other);
	}
	
	public bool Equals(_UnitPrice other){
		return this != null & other != null & this.result == other.result & this.data == other.data;
	}
	
	public override int GetHashCode()
	{
		//		return id;
		int hashCode = result.GetHashCode();
		hashCode ^= data.GetHashCode();
		return hashCode;
	}
	#endregion
}

//{"result":1,"data":{"freight":"10","packing_price":"10","unit_price":"18"}}
[Serializable]
public class _PriceData : IEquatable<_PriceData>{
	public float freight{set; get;}
	public float packing_price{set; get;}
	public float unit_price{set; get;}

	#region Equatable
	public override bool Equals(object obj)
	{
		if(obj == null) return false;
		_PriceData other = obj as _PriceData;
		if(other == null) return false;
		else return Equals(other);
	}
	
	public bool Equals(_PriceData other){
		return this != null & other != null & this.freight == other.freight & this.packing_price == other.packing_price & this.unit_price == other.unit_price;
	}
	
	public override int GetHashCode()
	{
		//		return id;
		int hashCode = freight.GetHashCode();
		hashCode ^= packing_price.GetHashCode();
		hashCode ^= unit_price.GetHashCode();
		//		//		foreach(_SourceItemData d in data){
		//		//			hashCode ^= d.GetHashCode();
		//		//		}
		//		return hashCode;
		return hashCode;
	}
	#endregion


}