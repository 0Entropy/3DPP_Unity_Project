using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;

public class _LogUtils : Singleton<_LogUtils> {

	string LogPath;
	StringBuilder LogBuilder;

	//

	public void AppendAndroidLog(string contents){
		AppendLog("Android" , contents);
	}

	public void AppendLog(string tag, string contents){
		if(LogBuilder == null){
			LogBuilder = new StringBuilder();
			LogBuilder.AppendLine();
		}
		LogBuilder.Append(string.Format("{0}, {1}, at {2};" , tag, contents, Time.frameCount));
		LogBuilder.AppendLine();
	}

	public void SaveLog(){
		if(LogBuilder == null)
			return;
		if(string.IsNullOrEmpty(LogPath))
			LogPath = Path.Combine(_InternalDataManager.Instance._LocalDatabaseDirectory, "internal_log.txt");
		if(File.Exists(LogPath))
			_InternalDataCodec.DeleteLocalData(LogPath);
		_InternalDataCodec.SaveSerializableData<string>(LogBuilder.ToString(), LogPath);
	}

	public override void Init ()
	{
		base.Init ();
	}

	public override void Release ()
	{
		base.Release ();
	}

	public void OnDisable(){
		SaveLog();
	}
}
