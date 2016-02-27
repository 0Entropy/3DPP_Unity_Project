using UnityEngine;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class _AvatorData {//, IComparable<_AvatorData>{// : IEquatable<_AvatorData>
	public string id {set; get;}
	public string app_code{set; get;}
	public string name{set; get;}
	public int wallpaperIndex{set; get;}
	public string texLocalPath{set; get;}
	public List<_SourceItemData> decorationlist{set; get;}

//	#region Equatable
//	public override bool Equals(object obj)
//	{
//		Debug.Log ("Equals(object obj)");
//		if(obj == null) return false;
//		_AvatorData other = obj as _AvatorData;
//		if(other == null) return false;
//		return Equals(other);
//	}
//	
//	public bool Equals(_AvatorData other){
//
////		if(other == null){
////			Debug.Log ("OTHER　IS NULL");
////			return false;
////		}
////		if(this.id != other.id){
////			Debug.Log ("ID　IS NOE EQUALS");
////			return false;
////		}
////		if(this.name != other.name){
////			Debug.Log ("NAME　IS NOE EQUALS");
////			return false;
////		}
////		if(this.wallpaperIndex != other.wallpaperIndex){
////			Debug.Log ("wallpaperIndex　IS NOE EQUALS");
////			return false;
////		}
////		if(this.decorationlist.Count != other.decorationlist.Count){
////			Debug.Log ("COUNT　IS NOE EQUALS");
////			return false;
////		}
////		bool isEquals = this.id == other.id && this.name == other.name && this.wallpaperIndex == other.wallpaperIndex && this.decorationlist.Count == other.decorationlist.Count;
////		if(!isEquals)
////			return false;
//
//		bool isEquals = true;
//
////		this.decorationlist.Sort();
////		other.decorationlist.Sort();
//		for(int i=0; i<this.decorationlist.Count; i++){
//
//			isEquals &= this.decorationlist[i] == other.decorationlist[i];
//			if(!isEquals)
//				return false;
//		}
//
//		return this.GetHashCode() == other.GetHashCode();
//	}
//	
//	public override int GetHashCode()
//	{
//
//		int hashCode = id.GetHashCode();
//		hashCode ^= name.GetHashCode();
//		hashCode ^= wallpaperIndex.GetHashCode();
//		int i = 0;
//		while(i<this.decorationlist.Count){
//			hashCode ^= this.decorationlist[i].id.GetHashCode();
////			Debug.Log(" i = " + i + ", id : " + this.decorationlist[i].id);
//			i++;
//		}
////		Debug.Log ("GetHashCode : " + hashCode);
//		return hashCode;
//////		hashCode ^= wallpaperIndex.GetHashCode();
//
////		return hashCode;
////		int hashCode = string.
//	}
//	#endregion

//	public int CompareTo(_AvatorData other){
//		if(other == null) return 1;
//		return this.name.CompareTo(other.name);//this.id.CompareTo(other.id);
//	}

	public override string ToString ()
	{
		StringBuilder itemsSB = new StringBuilder();
		for(int i=0; i<decorationlist.Count; i++){
			itemsSB.Append(decorationlist[i].ToString());
			if(i < decorationlist.Count - 1){
				itemsSB.Append(",");
			}
		}
//		if(Application.platform == RuntimePlatform.IPhonePlayer)
		return string.Format ("{{\"id\":\"{0}\",\"app_code\":\"{1}\",\"name\":\"{2}\",\"wallpaperIndex\":{3},\"texLocalPath\":\"{4}\",\"decorationlist\":[{5}]}}",
		                      id, 			app_code, 		name,			wallpaperIndex, 		texLocalPath,  		itemsSB.ToString());

	}

}

