using UnityEngine;
using UnityEngine.UI;

public class NavigationBar : MonoBehaviour {

    public InputField inputName;

    public void HandleOnNameEditFinished()
    {
        // 		if(PlatformBridge.Instance != null)
        // 			PlatformBridge.Instance._UserActionData(inputName.text, " User finished name edit! ");
        if (inputName.text.Length == 0)
            inputName.text = _Hero.Instance.defaultAvatorData.name;

        _Hero.Instance.AvatorName = inputName.text;
    }

    public void OnInit()
    {
        string avatorName = _Hero.Instance.AvatorName;
        if (string.IsNullOrEmpty(avatorName))
            avatorName = _Hero.Instance.defaultAvatorData.name;//.DefaultAvator.name;
        else
            inputName.text = _Hero.Instance.AvatorName;
    }


}
