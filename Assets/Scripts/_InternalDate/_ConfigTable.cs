using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// _ config table.
/// {"result":number 1, "data":array[...]}
/// </summary>
[Serializable]
public class _ConfigTable : IEquatable<_ConfigTable>{

	public int result{set; get;}
	public List<_SourceMenuData> data{set; get;}

	#region Equatable
	public override bool Equals (object obj){
		if(obj == null) return false;
		_ConfigTable other = obj as _ConfigTable;
		if(other == null) return false;
		else return Equals(other);
		
	}
	
	public bool Equals(_ConfigTable other){

		if(other == null || this == null)
			return false;

		bool isEquals = this.data.Count == other.data.Count ;

		if(!isEquals)
			return false;

		data.Sort();
		other.data.Sort();

		for(int i=0; i< data.Count; i++){
			isEquals &= this.data[i].deepEquals(other.data[i]);
			if(!isEquals )
				return false;
		}
		return true;
	}
	
	public override int GetHashCode(){
//		int hashCode = result.GetHashCode();
//		hashCode ^= data.Count.GetHashCode();
//		return hashCode;
		return 0;//data.GetHashCode();
	}
	#endregion

	public _SourceItemData Find(Predicate<_SourceItemData> match){
		_SourceItemData dest = null;
		foreach(_SourceMenuData menu in data){
			dest = menu.Find(match);
			if(dest!= null)
                break;
		}
        if(dest != null)
		    return dest;
        return _SourceItemData.NullItemData();
	}

	public List<_SourceItemData> FindAll(Predicate<_SourceItemData> match){
		List<_SourceItemData> dest = new List<_SourceItemData>();
		foreach(_SourceMenuData menu in data){
			dest.AddRange(menu.FindAll(match));
		}
		return dest;
	}


	public void RemoveAll( Predicate<_SourceItemData> filter ){
		int i = 0;
		while(i<data.Count){
			data[i].RemoveAll(filter);
			i++;
		}
	}
}

/// <summary>
/// _ source menu data.
/// {"sort": number 0, "data": array[...],"children": array[...], "typeDesc": string "头","type": string "suit","typeId": number 1}
/// </summary>
/// 
[Serializable]
public class _SourceMenuData :  IComparable<_SourceMenuData>, IEquatable<_SourceMenuData>{
	public int typeId{set; get;}
	public int sort{set; get;}
	public string type{set; get;}
	public List<_SourceMenuData> children{set; get;}
	public List<_SourceItemData> data{set; get;}

	public void RemoveAll( Predicate<_SourceItemData> filter ){
		if(hasChildrenMenu){
			int i = 0;
			while(i<children.Count){
				children[i].RemoveAll(filter);
				i++;
			}
		}
		data.RemoveAll(filter);
	}
	
//	IEnumerator IEnumerable.GetEnumerator()
//	{
//		if(this.hasChildrenMenu){
//
//		}
//		else{
//
//		}
////		return (IEnumerator) GetEnumerator();
//	}

	#region Equatable
	public override bool Equals (object obj){
		if(obj == null) return false;
		_SourceMenuData other = obj as _SourceMenuData;
		if(other == null) return false;
		else return Equals(other);

	}

	public bool Equals(_SourceMenuData other){
		return this != null & other != null & this.sort == other.sort & typeId.Equals(other.typeId);

	}

	public override int GetHashCode(){
		return sort.GetHashCode();
	}
	#endregion

	#region Comparable
	public int CompareTo(_SourceMenuData other){
		if(other == null) return 1;
//		return typeId.CompareTo(other.typeId);
		return this.sort.CompareTo(other.sort);
	}
	#endregion

	public bool hasChildrenMenu{get{return children.Count > 0 & data.Count == 0;}}

	public bool deepEquals(_SourceMenuData other){

		if(this != other)
			return false;

		bool isEquals = this.hasChildrenMenu == other.hasChildrenMenu;
		if(!isEquals)
			return false;
		if(this.hasChildrenMenu){
			isEquals &= this.children.Count == other.children.Count;
			if(!isEquals)
				return false;
			this.children.Sort();
			other.children.Sort();
			for(int i=0; i<this.children.Count; i++){
				isEquals &= this.children[i].deepEquals(other.children[i]);
				if(!isEquals)
					return false;
			}
		}else{
			isEquals &= this.data.Count == other.data.Count;
			if(!isEquals)
				return false;
			this.data.Sort();
			other.data.Sort ();
			for(int i=0; i<this.data.Count; i++){
				isEquals &= this.data[i] == other.data[i];
				if(!isEquals)
					return false;
			}
		}
		return true;

	}

//	public _SourceItemData Find(_SourceItemData item){
//		_SourceItemData dest = null;
//		if(this.hasChildrenMenu){
//			foreach(_SourceMenuData subMenu in children){
//				dest = subMenu.Find(item);
//				if(dest != null)
//					break;
//			}
//		}else{
//			dest = data.Find( (x) => x.Equals(item));
//		}
//		return dest;
//	}

	public _SourceItemData Find(Predicate<_SourceItemData> match){
		_SourceItemData dest = null;
		if(this.hasChildrenMenu){
			foreach(_SourceMenuData subMenu in children){
				dest = subMenu.Find(match);
				if(dest != null)
					break;
			}
		}else{
			dest = data.Find(match);
		}
		return dest;
	}

	public List<_SourceItemData> FindAll(Predicate<_SourceItemData> match){
		List<_SourceItemData> dest = new List<_SourceItemData>();
		if(this.hasChildrenMenu){
			foreach(_SourceMenuData subMenu in children){
				dest.AddRange(subMenu.FindAll(match));
			}
		}else{
			dest.AddRange(data.FindAll(match));
		}
		return dest;
	}

//	public _SourceMenuData FindMenu(Predicate<_SourceMenuData> match){
//		if(hasChildrenMenu)
//			return children.Find(match);
//		else
//			return null;
//	}
}

/// <summary>
/// 	"isDefault":1, 是否预置配饰 为 ‘否’; 
/// 	"isDefault":0, 是否预置配饰 为 ‘是’;
/// 	"decoration_status":1, 状态 为 ‘下架’； 
/// 	"decoration_status":0, 状态 为 ‘上架’；
/// just like 'bool true = false;' WTF it is !!!
/// </summary>
[Serializable]
public class _SourceItemData : IEquatable<_SourceItemData>, IComparable<_SourceItemData>, INullObject
{
    public int id{set; get;}
	public string title{set; get;}
	public float superficial_area{set; get;}

	public int decoration_type{set; get;}

	public string typeCode {set; get;}
	public string typeDescript {set; get;}

	public string logoFile{set; get;}
	public string decorationfile{set; get;}//decorationfile
	public string decorationfile_ios{set; get;}

	public bool isDefault{set; get;} // 是否默认
	public int decoration_status{set; get;} //是否下架

	public int createTime{set; get;}
	public int lastupDateTime{set; get;}
    
	public string remark{set; get;}

    public string internalID = string.Empty;

    public string PlatformDecoFile{
		get{

			#if UNITY_ANDROID
				return decorationfile;
			#elif UNITY_IPHONE
				return decorationfile_ios;
			#else
				return decorationfile;
			#endif


		}
	}

	public int version{set; get;}

    public bool isHide{set; get;}

	public bool isSelected{set; get;}

	public bool isNew{
		get{
			long nowTime = (long)((TimeSpan)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds;
			int days = (int)((nowTime - createTime) / (24 * 60 * 60));
//			Debug.Log(days);
			return days < 7;
		}
	}
    
	public bool isUIImageLoaded {
		get {
			return File.Exists(UIImageLocalPath);
		}
	}
    
	public bool isDecorationLoaded{
		get {
			return File.Exists(DecorationLocalPath);
		}
	}

	public bool isTempLoaded{
		get {
			return File.Exists(TempLocalPath);
		}
	}

	public string UIImageLocalPath{
		get{
			return Path.Combine(_InternalDataManager.Instance._LocalUIDirectory, title + ".png");
		}
	}

	public string DecorationLocalPath{
		get{
			return Path.Combine(_InternalDataManager.Instance._LocalAssetDirectory, title + ".unity3d");
		}
	}

	public string TempLocalPath{
		get{
			return Path.Combine(_InternalDataManager.Instance._LocalTempDirectory, title + ".unity3d");
		}
	}

    public IEnumerator WaitForDownloadUI()
    {
        return new WWWProxy()
        {
            url = logoFile,
            feedback = delegate (WWW www){
                _InternalDataCodec.SaveTexture(UIImageLocalPath, www.texture, true);
            }
        }.WaitForFinished();
    }

    public IEnumerator WaitForDownloadAsset()
    {
        return new WWWProxy()
        {
            url = PlatformDecoFile,
            feedback = delegate (WWW www)
            {
                _InternalDataCodec.SaveBytes(DecorationLocalPath, www.bytes);
            }
        }.WaitForFinished();
    }

    public IEnumerator WaitForDownloadTemp()
    {
        return new WWWProxy()
        {
            url = PlatformDecoFile,
            feedback = delegate (WWW www)
            {
                _InternalDataCodec.SaveBytes(TempLocalPath, www.bytes);
            }
        }.WaitForFinished();
    }

    #region Equatable
    public override bool Equals(object obj)
	{
		if(obj == null) return false;
		_SourceItemData other = obj as _SourceItemData;
		if(other == null) return false;
		else return Equals(other);
	}

	public bool Equals(_SourceItemData other){
		return !this.IsNull() && !other.IsNull() && this != null && other != null && this.id == other.id && this.title.Equals(other.title);
	}

	public override int GetHashCode()
	{
		return id.GetHashCode();
	}
	#endregion

	#region Comparable
	public int CompareTo(_SourceItemData other){
		if(other == null) return 1;
		return this.id.CompareTo(other.id);
	}
	#endregion

	public override string ToString ()
	{
        return string.Format ("{{\"id\":{0},\"title\":\"{1}\",\"superficial_area\":{2},\"decoration_type\":{3},\"typeCode\":\"{4}\",\"typeDescript\":\"{5}\",\"logoFile\":\"{6}\",\"decorationfile\":\"{7}\",\"isDefault\":{8},\"decoration_status\":{9},\"createTime\":{10},\"lastupDateTime\":{11},\"version\":{12},\"isHide\":{13},\"isSelected\":{14},\"internalID\":\"{15}\",\"decorationfile_ios\":\"{16}\",\"remark\":\"{17}\"}}",
		                      id,					 	title, 				superficial_area, 					decoration_type, 					typeCode, 					typeDescript, 					logoFile, 						decorationfile,    			 (isDefault ? 1 : 0),					decoration_status,              			   createTime, 							lastupDateTime,          			   version,        			  (isHide?1:0),   		  (isSelected?1:0), internalID,								 decorationfile_ios,	 				remark );

	}

    public virtual bool IsNull()
    {
        return false;
    }

    public static _SourceItemData NullItemData()
    {
        return new _NullItemData();
    }

    
}

public class _NullItemData : _SourceItemData
{
    public override bool IsNull()
    {
        return true;
    }
}

public interface INullObject
{
    bool IsNull();
}

/// <summary>
/// _ source menu data.
/// {"typeDesc": string "头","type": string "suit","typeId": number 1}
/// </summary>
///
[Serializable]
public class _SourceAssetType{
	public int id{set; get;}
	/// <summary>
	/// Gets or sets the code.
	/// Something likes NAME!!!
	/// head, face, foot, pants???
	/// </summary>
	/// <value>The code.</value>
	public string code {set; get;}
	/// <summary>
	/// Gets or sets the index of the sorted.
	/// 此值本地基本没用处。
	/// </summary>
	/// <value>The index of the sorted.</value>
	public int sort {set; get;}
	/// <summary>
	/// Gets or sets the remark.
	/// 中文！！！备注！！！
	/// </summary>
	/// <value>The remark.</value>
	public string remark{set; get;}
}
