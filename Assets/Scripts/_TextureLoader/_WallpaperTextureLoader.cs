using UnityEngine;
using System.Collections.Generic;

public class _WallpaperTextureLoader : _TextureLoader {

    public Transform textureGroup { set; get; }

    public _WallpaperTextureLoader(string _texName, Texture2D _defaultTex, int _id, Transform _texGroup) : base(_texName, _defaultTex, _id)
    {
        textureGroup = _texGroup;
        OnDefault();
    }

    /*
        for(int i=0; i<renderers.Length; i++){
			renderers[i].material.SetTexture("_MainTex", tex); n
		}
    */
    public override void SetTexture()
    {

        if (currentTex.wrapMode != TextureWrapMode.Repeat)
            currentTex.wrapMode = TextureWrapMode.Repeat;

        MeshRenderer[] renderers = textureGroup.GetComponentsInChildren<MeshRenderer>();

        /*Debug.Log("_WallpaperTextureLoader.SetTexture : Current Item ID:" + currentItem.id);*/

        /*Debug.Log("_WallpaperTextureLoader.SetTexture : renderers length :" + renderers.Length);*/
        for (int i = 0; i < renderers.Length; i++)
        {
            /*Debug.Log("renderer -  :" + i + " : " + renderers[i].name);*/
            renderers[i].material.SetTexture(texName, currentTex);
        }
    }
}
