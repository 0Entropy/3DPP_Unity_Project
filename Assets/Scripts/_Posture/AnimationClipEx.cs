using UnityEngine;
using System.Collections;

public class AnimationClipEx : ScriptableObject
{
	public AnimationClip 	animationClip;
	public string 			name;
	public float 			frameRate;
	public float 			length;
	public bool 			isLooping;
	public bool 			isHumanMotion;
	
	//隐式类型转换 AnimationClipEx->AnimationClip
	public static implicit operator AnimationClip(AnimationClipEx animationClipEx)
	{
		return animationClipEx.animationClip;
	}

	public void SetAnimationClip(AnimationClip clip){
		animationClip = clip;
		name = clip.name;
		frameRate = clip.frameRate;
		length = clip.length;
//#if UNITY_EDITOR
//		isLooping = clip.isLooping;
//		isHumanMotion = clip.isHumanMotion;
//#endif
	}

	public override string ToString ()
	{
		return string.Format ("AnimationClipEx: [name:{0}; frameRate:{1}; legth:{2}]",//; isLooping:{3}; isHumanMotion:{4}]",
		                      name, frameRate, length);//, isLooping, isHumanMotion);
	}
}

