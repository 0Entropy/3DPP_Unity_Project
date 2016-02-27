using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class _FeatureTextureLoader : _TextureLoader
{

	SkinnedMeshRenderer render{set; get;}

    public _FeatureTextureLoader(string _texName, Texture2D _defaultTex, int _id, SkinnedMeshRenderer _render) : base(_texName, _defaultTex, _id)
    {
        render = _render;
        OnDefault();
    }

    public override void SetTexture()
    {
        if(currentTex.wrapMode != TextureWrapMode.Clamp)
            currentTex.wrapMode = TextureWrapMode.Clamp;
        render.material.SetTexture(texName, currentTex);
    }
}
