using UnityEngine;
using System.Collections;
using System.IO;

public class _ModelData {

    public string PicName { set; get; }
    public string ItemIDs { set; get; }
    public string AvatorData { set; get; }
    public float Volume { set; get; }
    
    public bool IsNull()
    {
        return string.IsNullOrEmpty(PicName)
            || string.IsNullOrEmpty(ItemIDs) 
            || string.IsNullOrEmpty(AvatorData)
            || Volume <= 0;
    }

    public void OnDestroy()
    {
        if (!string.IsNullOrEmpty(PicName))
        {
            _InternalDataCodec.DeleteLocalData(Path.Combine(_InternalDataManager.Instance._CapturePicturePath, PicName));
            PicName = string.Empty;
        }
        ItemIDs = AvatorData = string.Empty;
        Volume = 0;
    }

    public static _ModelData OnCreate()
    {
        
        string texLocalPath = Camera.main.GetComponent<_MainCameraController>().OnCapturePicture();
        _Hero.Instance.TexLocalPath = texLocalPath;
        return new _ModelData()
        {
            PicName = texLocalPath,
            ItemIDs = _Hero.Instance.GetCurrentItemIds(),
            AvatorData = _Hero.Instance.GetCurrentAvatorData(),
            Volume = _Hero.Instance.GetCurrentVolume()
        };
}
}
