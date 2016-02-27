Shader "Custom/FeatureShader" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
//	_DecalTex ("Decal (RGBA)", 2D) = "black" {}
	_BrowTex ("Base (RGB) Alpha (A)", 2D) = "white" {}//("Brow (RGBA)", 2D) = "black" {}
	_EyeTex ("Base (RGB) Alpha (A)", 2D) = "white" {}//("Eye (RGBA)", 2D) = "black" {}
	_MouthTex ("Base (RGB) Alpha (A)", 2D) = "white" {}//("Mouth (RGBA)", 2D) = "black" {}
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 250
	
CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
//sampler2D _DecalTex;
sampler2D _BrowTex;
sampler2D _EyeTex;
sampler2D _MouthTex;

fixed4 _Color;

struct Input {
	float2 uv_MainTex;
//	float2 uv_DecalTex;
	float2 uv_BrowTex;
	float2 uv_EyeTex;
	float2 uv_MouthTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
//	half4 decal = tex2D(_DecalTex, IN.uv_DecalTex);
//	c.rgb = lerp (c.rgb, decal.rgb, decal.a);
	half4 brow = tex2D(_BrowTex, IN.uv_BrowTex);
	c.rgb = lerp (c.rgb, brow.rgb, brow.a);
	half4 eye = tex2D(_EyeTex, IN.uv_EyeTex);
	c.rgb = lerp (c.rgb, eye.rgb, eye.a);
	half4 mouth = tex2D(_MouthTex, IN.uv_MouthTex);
	c.rgb = lerp (c.rgb, mouth.rgb, mouth.a);
	c *= _Color;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}
ENDCG
}

Fallback "Diffuse"
}