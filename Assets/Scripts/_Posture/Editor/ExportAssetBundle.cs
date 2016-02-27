using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class ExportAssetBundle : MonoBehaviour {

	[MenuItem("Assets/Build AssetBundle/Hands For Android")]
	static void ExportAnimationClipForAndroid () {

		ExportAnimationClip(BuildTarget.Android);

	}

	[MenuItem("Assets/Build AssetBundle/Hands For iOS")]
	static void ExportAnimationClipForIOS () {
		
		ExportAnimationClip(BuildTarget.iPhone);
		
	}

	static void ExportAnimationClip (BuildTarget target) {

		string resultPath = ActiveBundleDirectionForTarget(target) + "hands.unity3d";

		DeleteBundleFileByName(target, "hands");

		List<Object> handsToInclude = new List<Object>();
		List<string> tempPaths = new List<string>();
		
		foreach (Object o in Selection.GetFiltered(typeof (Object), SelectionMode.DeepAssets))
		{
			
			if (o.name.Contains("animation") && !o.name.Contains("@")){
				Debug.Log (o.name);
				AnimationClip clip = o as AnimationClip;
				AnimationClipEx clipEx = ScriptableObject.CreateInstance<AnimationClipEx>();
				clipEx.SetAnimationClip(clip);
				string path = "Assets/anime.asset";
				AssetDatabase.CreateAsset(clipEx, path);
				
				handsToInclude.Add(AssetDatabase.LoadAssetAtPath(path, typeof (AnimationClipEx)));
				tempPaths.Add (path);
			}
			
			if (o.name.Contains("left_hand") && !o.name.Contains("tex") && !o.name.Contains("mat")){
				AddPrefabClone(o, "left_hand", ref handsToInclude, ref tempPaths);
			}
			
			if (o.name.Contains("right_hand") && !o.name.Contains("tex") && !o.name.Contains("mat")){
				AddPrefabClone(o, "right_hand", ref handsToInclude, ref tempPaths);
			}
			
			if(o.name.Contains("pose") && !o.name.Contains("_")){
				TextAsset poseObj = o as TextAsset;
				//
				Debug.Log (poseObj.text);
				handsToInclude.Add (poseObj);
			}
			
		}
		
		
		BuildPipeline.BuildAssetBundle(null, handsToInclude.ToArray(), resultPath, 
		                               BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle,
		                               target//BuildTarget.Android// | BuildTarget.iPhone
		                               );
		Debug.Log("Saved " + resultPath + " with " + (handsToInclude.Count) + " items");
		
		int i = 0;
		while( i < tempPaths.Count ){
			AssetDatabase.DeleteAsset(tempPaths[i]);
			i++;
		}
		
	}

	[MenuItem("Assets/Build AssetBundle/All For Android")]
	static void ExportAllAssetsForAndroid(){ ExportAllAssets(BuildTarget.Android); }

	[MenuItem("Assets/Build AssetBundle/All For iOS")]
	static void ExportAllAssetsForIOS(){ ExportAllAssets(BuildTarget.iPhone); }

	static void ExportAllAssets(BuildTarget target){

//		string resultDir = ActiveBundleDirectionForTarget(target);

		DeleteAllBundleFiles(target);

		List<Object> handsToInclude = new List<Object>();
		List<string> tempPaths = new List<string>();
		List<Object> feetToInclude = new List<Object>();

		foreach (Object o in Selection.GetFiltered(typeof (Object), SelectionMode.DeepAssets))
		{
			
			if (o.name.Contains("_hair_") && !o.name.Contains("tex") && !o.name.Contains("mat")){

				BuildFbxObjAsset(o, "hair", target);
				continue;
			}

			if (o.name.Contains("_earring_") && !o.name.Contains("tex") && !o.name.Contains("mat")){
				
				BuildFbxObjAsset(o, "earrings", target);
				continue;
			}

			if (o.name.Contains("_glasses_") && !o.name.Contains("tex") && !o.name.Contains("mat")){
				
				BuildFbxObjAsset(o, "glasses", target);
				continue;
			}

			if (o.name.Contains("_hat_") && !o.name.Contains("tex") && !o.name.Contains("mat")){
				
				BuildFbxObjAsset(o, "hat", target);
				continue;
			}



			if (o.name.Contains("_eyebrows_")){
				
				BuildTextureAsset(o, "brows", target);
				continue;
			}

			if (o.name.Contains("_eyes_")){
				
				BuildTextureAsset(o, "eyes", target);
				continue;
			}

			if (o.name.Contains("_mouth_")){
				
				BuildTextureAsset(o, "mouth", target);
				continue;
			}

			if (o.name.Contains("_bottom_") && !o.name.Contains("tex") && !o.name.Contains("mat")){
				
				BuildFbxObjAsset(o, "bottom", target);
				continue;
			}

			if (o.name.Contains("_up_") && !o.name.Contains("tex") && !o.name.Contains("mat")){
				
				BuildFbxObjAsset(o, "upper", target);
				continue;
			}

			BuildHandsAssets(o, ref handsToInclude, ref tempPaths);

			BuildFeetAssets(o, ref feetToInclude, ref tempPaths);
			
		}

		BuildPipeline.BuildAssetBundle(null, handsToInclude.ToArray(), ActiveBundleDirectionForTarget(target) + "righthand.unity3d", 
		                               BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle,
		                               target//BuildTarget.Android// | BuildTarget.iPhone
		                               );
		Debug.Log("Saved " + "righthand" + " with " + (handsToInclude.Count) + " items");

		BuildPipeline.BuildAssetBundle(null, feetToInclude.ToArray(), ActiveBundleDirectionForTarget(target) + "foots.unity3d", 
		                               BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle,
		                               target//BuildTarget.Android// | BuildTarget.iPhone
		                               );
		Debug.Log("Saved " + "foots" + " with " + (handsToInclude.Count) + " items");

		int i = 0;
		while( i < tempPaths.Count ){
			AssetDatabase.DeleteAsset(tempPaths[i]);
			i++;
		}

	}

	static void BuildTextureAsset(Object o, string assetName, BuildTarget target){
		Texture2D texture = o as Texture2D;

		BuildPipeline.BuildAssetBundle(texture, null, ActiveBundleDirectionForTarget(target) + assetName + ".unity3d",
		                               BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle,
		                               target
		                               );
		Debug.Log("Saved " + assetName );
//		AssetDatabase.DeleteAsset(tempPath);

	}

	static void BuildFbxObjAsset(Object o, string assetName, BuildTarget target){
		Debug.Log (assetName);
		GameObject obj = o as GameObject;
		GameObject clone = Object.Instantiate(obj) as GameObject;
		
		string tempPath = "Assets/" + assetName + ".prefab";
		Object tempPrefab = PrefabUtility.CreateEmptyPrefab(tempPath);
		tempPrefab = PrefabUtility.ReplacePrefab(clone, tempPrefab);
		Object.DestroyImmediate(clone);
		
		BuildPipeline.BuildAssetBundle(tempPrefab, null, ActiveBundleDirectionForTarget(target) + assetName + ".unity3d",
		                               BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle,
		                               target
		                               );
		Debug.Log("Saved " + assetName );
		AssetDatabase.DeleteAsset(tempPath);
//		return tempPath;
	}

	static void BuildHandsAssets(Object o, ref List<Object> toInclued, ref List<string> tempPaths){
		if (o.name.Contains("animation") && !o.name.Contains("@")){
			Debug.Log (o.name);
			AnimationClip clip = o as AnimationClip;
			AnimationClipEx clipEx = ScriptableObject.CreateInstance<AnimationClipEx>();
			clipEx.SetAnimationClip(clip);
			string path = "Assets/anime.asset";
			AssetDatabase.CreateAsset(clipEx, path);
			
			toInclued.Add(AssetDatabase.LoadAssetAtPath(path, typeof (AnimationClipEx)));
			tempPaths.Add (path);
		}
		
		if (o.name.Contains("left_hand") && !o.name.Contains("tex") && !o.name.Contains("mat")){
			AddPrefabClone(o, "left_hand", ref toInclued, ref tempPaths);
		}
		
		if (o.name.Contains("right_hand") && !o.name.Contains("tex") && !o.name.Contains("mat")){
			AddPrefabClone(o, "right_hand", ref toInclued, ref tempPaths);
		}
		
		if(o.name.Contains("pose") && !o.name.Contains("_")){
			TextAsset poseObj = o as TextAsset;
			//
			Debug.Log (poseObj.text);
			toInclued.Add (poseObj);
		}
	}

	static void BuildFeetAssets(Object o, ref List<Object> toInclued, ref List<string> tempPaths){
				
		if (o.name.Contains("left_foot") && !o.name.Contains("tex") && !o.name.Contains("mat")){
			AddPrefabClone(o, "left_foot", ref toInclued, ref tempPaths);
		}
		
		if (o.name.Contains("right_foot") && !o.name.Contains("tex") && !o.name.Contains("mat")){
			AddPrefabClone(o, "right_foot", ref toInclued, ref tempPaths);
		}

	}

	static void AddPrefabClone(Object o, string name, ref List<Object> assets, ref List<string> paths){
		Debug.Log (o.name);
		GameObject obj = o as GameObject;
		GameObject clone = (GameObject)Object.Instantiate(obj);
		
		string path = "Assets/" + name + ".prefab";
		Object tempPrefab = PrefabUtility.CreateEmptyPrefab(path);
		tempPrefab = PrefabUtility.ReplacePrefab(clone, tempPrefab);
		Object.DestroyImmediate(clone);
		
		assets.Add (tempPrefab);
		paths.Add(path);
	}



//	static AnimationClipEx CreateAnimScripeObj(Object o){
//		AnimationClip clip = o as AnimationClip;
//		AnimationClipEx clipEx = ScriptableObject.CreateInstance<AnimationClipEx>();
//		clipEx.SetAnimationClip(clip);
//		return clipEx;
//	}

	#region Path And Name
	static string RootBundleDirection{
		get{ return "Assets" + Path.DirectorySeparatorChar + "_assetbundles" + Path.DirectorySeparatorChar; }
	}

	static string ActiveObjectName{
		get{ 
			int last = Selection.activeObject.name.LastIndexOf("_");
			if(last < 0){
				Debug.LogError ("The Selection ActiveObject is Not \" name_00_project\"!");
				return string.Empty;
			}
			return Selection.activeObject.name.Substring(0, last + 1);
		}
	}

	static string ActiveBundleDirectionForTarget(BuildTarget target){
		
		string resultDir = RootBundleDirection + ActiveObjectName + target.ToString() + Path.DirectorySeparatorChar;
		
		if (!Directory.Exists(resultDir)){
			Directory.CreateDirectory(resultDir);
		}
		
		return resultDir;
	}

	
	static void DeleteBundleFileByName(BuildTarget target, string name){
				
		foreach (string bundle in Directory.GetFiles(ActiveBundleDirectionForTarget(target)))
		{
			Debug.Log (bundle);
			
			if (bundle.EndsWith(".unity3d") && bundle.Contains(name))
				File.Delete(bundle);
		}
	}

	static void DeleteAllBundleFiles(BuildTarget target){
		foreach (string bundle in Directory.GetFiles(ActiveBundleDirectionForTarget(target)))
		{
			Debug.Log (bundle);

			File.Delete(bundle);
		}
	}

	static void DeleteTempFiles(List<string> tempPaths){
		int i = 0;
		while( i < tempPaths.Count ){
			AssetDatabase.DeleteAsset(tempPaths[i]);
			i++;
		}
	}

	#endregion

	[MenuItem("Assets/Create Posture/Create pose.txt ")]
	static void CreatePostTextAsset () {
		
		string rootPath = AssetDatabase.GetAssetPath(Selection.activeObject);
		string[] existingAssetbundles = Directory.GetFiles(rootPath, "*.txt", SearchOption.AllDirectories);
		foreach (string bundle in existingAssetbundles)
		{
			Debug.Log (bundle);
			
			if (bundle.EndsWith(".txt") && bundle.Contains("pose"))
				File.Delete(bundle);
		}
		
		foreach (Object o in Selection.GetFiltered(typeof (Object), SelectionMode.DeepAssets))
		{
			if (o.name.Contains("_pose_")){
				GameObject poseObj = o as GameObject;
				GameObject poseClone = (GameObject)Object.Instantiate(poseObj);
				
				string path =rootPath + "/pose.txt";
				
				List<_SkeletonData> skeleton = new List<_SkeletonData>();
				Transform root = poseClone.transform;
				AddSkelet(root, ref skeleton);
				AddChildrenSkelets(root, ref skeleton);
				
				_PostureData postureData = new _PostureData(){ data = skeleton};
				_InternalDataCodec.SaveString(JsonFx.Json.JsonWriter.Serialize(postureData), path);
				
				Object.DestroyImmediate(poseClone);
			}
		}
	}
	


	static void AddChildrenSkelets(Transform root, ref List<_SkeletonData> data){
		foreach(Transform child in root){
			AddSkelet(child, ref data);
			if(child.childCount > 0)
				AddChildrenSkelets(child, ref data);
		}
	}
	
	static void AddSkelet(Transform trans, ref List<_SkeletonData> data){
		Vector3 pos = trans.position;
		Vector3 ang = trans.eulerAngles;
		data.Add ( new _SkeletonData(){
			name = trans.name,
			positionF = new float[3]{pos.x, pos.y, pos.z},
			eulerAnglesF = new float[3]{ang.x, ang.y, ang.z}
		});
	}

// 	[MenuItem("Assets/Build AssetBundle/Selection For Android")]
// 	static void ExportResourceForAndroid () {
// 		ExportResource(BuildTarget.Android);
// 	}
// 	
// 	[MenuItem("Assets/Build AssetBundle/Selection For iOS")]
// 	static void ExportResourceForIOS() {
// 		ExportResource(BuildTarget.iPhone);
// 	}
// 	
// 	static void ExportResource(BuildTarget target){
// 		string path = EditorUtility.SaveFilePanel ("Save Resource", "", "New Resource", "unity3d");
// 		if (path.Length != 0) {
// 			// Build the resource file from the active selection.
// 			Object[] selection = Selection.objects;//.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
// 			BuildPipeline.BuildAssetBundle(
// 				null, 
// 				selection, 
// 				path, 
// 				BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle,
// 				target//BuildTarget.iPhone
// 				);
// //			Selection.objects = selection;
// 		}
// 	}

    [MenuItem("Assets/Build AssetBundle/For Android    - Track dependencies")]
    static void ExportResourceForAndroid()
    {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(
                Selection.activeObject,
                selection,
                path,
                BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle,
                BuildTarget.Android// | BuildTarget.iPhone
                );
            Selection.objects = selection;
        }
    }

    [MenuItem("Assets/Build AssetBundle/For iOS           - Track dependencies")]
    static void ExportResourceForIOS()
    {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(
                Selection.activeObject,
                selection,
                path,
                BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.UncompressedAssetBundle,
                BuildTarget.iPhone
                );
            Selection.objects = selection;
        }
    }

}
