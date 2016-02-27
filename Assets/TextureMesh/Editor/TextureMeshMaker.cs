using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class TextureMeshMaker : EditorWindow
{
	public enum ScaleBy
	{
		Pixel,
		Width,
		Height,
	}

	public enum AnchorType
	{
		TopLeft,
		Top,
		TopRight,
		Left,
		Center,
		Right,
		BottomLeft,
		Bottom,
		BottomRight,
	}

	public enum ShaderType
	{
		UnlitTexture,
		UnlitTransparent,
		UnlitTransparentColor,
		UnlitTransparentCutout,
		UnlitTransparentCutoutColor,
	}

	string _GetShaderName()
	{
		switch( _shaderType ) {
		case ShaderType.UnlitTexture:					return "Unlit/Texture";
		case ShaderType.UnlitTransparent:				return "Unlit/Transparent";
		case ShaderType.UnlitTransparentColor:			return "Unlit/Transparent Color";
		case ShaderType.UnlitTransparentCutout:			return "Unlit/Transparent Cutout";
		case ShaderType.UnlitTransparentCutoutColor:	return "Unlit/Transparent Cutout Color";
		}
		return "";
	}

	Texture _texture;
	ShaderType _shaderType = ShaderType.UnlitTransparentColor;
	AnchorType _anchorType = AnchorType.Center;
	ScaleBy _scaleBy = ScaleBy.Height;
	float _size = 1.0f;
	bool _isCreateAsset = false;

	[MenuItem ("Window/Texture Mesh Maker")]
	static void Init()
	{
		EditorWindow.GetWindow(typeof(TextureMeshMaker));
	}
	
	void OnGUI()
	{
		GUILayout.Label( "Settings", EditorStyles.boldLabel );
		_texture = (Texture)EditorGUILayout.ObjectField( "Texture", _texture, typeof(Texture), false );

		_shaderType = (ShaderType)EditorGUILayout.EnumPopup( "Shader Type", _shaderType );
		_anchorType = (AnchorType)EditorGUILayout.EnumPopup( "Anchor Type", _anchorType );
		_scaleBy = (ScaleBy)EditorGUILayout.EnumPopup( "Scale By", _scaleBy );
		_size = EditorGUILayout.FloatField( "Size", _size );
		_isCreateAsset = EditorGUILayout.Toggle( "Create Asset", _isCreateAsset );

		EditorGUILayout.Separator();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUI.enabled = (_texture != null);
		if( GUILayout.Button("Process") ) {
			_Process();
		}
		GUI.enabled = true;
		EditorGUILayout.EndHorizontal();
	}

	void _Process()
	{
		Texture2D texture = _texture as Texture2D;
		if( texture == null ) {
			Debug.LogError( "Texture is not Texture2D." );
			return;
		}

		string assetPath = AssetDatabase.GetAssetPath( texture );
		TextureImporter textureImporter = (TextureImporter)TextureImporter.GetAtPath( assetPath );
		bool overrideTextureFormat = false;
		bool overrideNPOTScale = false;
		bool overrideGenerateMipmap = false;
		bool overrideMaxTextureSize = false;
		TextureImporterFormat textureImporterFormat = TextureImporterFormat.ARGB32;
		TextureImporterNPOTScale textureImporterNPOTScale = TextureImporterNPOTScale.None;
		int maxTextureSize = 0;
		if( textureImporter.textureFormat != TextureImporterFormat.ARGB32 ) {
			textureImporterFormat = textureImporter.textureFormat;
			textureImporter.textureFormat = TextureImporterFormat.ARGB32;
			overrideTextureFormat = true;
		}
		if( textureImporter.generateMipsInLinearSpace ) {
			textureImporter.generateMipsInLinearSpace = false;
			overrideGenerateMipmap = true;
		}
		if( textureImporter.npotScale != TextureImporterNPOTScale.None ) {
			textureImporterNPOTScale = textureImporter.npotScale;
			textureImporter.npotScale = TextureImporterNPOTScale.None;
			overrideNPOTScale = true;
		}
		if( textureImporter.maxTextureSize != 4096 ) {
			maxTextureSize = textureImporter.maxTextureSize;
			textureImporter.maxTextureSize = 4096;
			overrideMaxTextureSize = true;
		}
		if( overrideTextureFormat || overrideGenerateMipmap || overrideNPOTScale || overrideMaxTextureSize ) {
			AssetDatabase.WriteImportSettingsIfDirty( assetPath );
			AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
		}
		
		int width = texture.width;
		int height = texture.height;

		if( overrideTextureFormat || overrideGenerateMipmap || overrideNPOTScale || overrideMaxTextureSize ) {
			if( overrideTextureFormat ) {
				textureImporter.textureFormat = textureImporterFormat;
			}
			if( overrideGenerateMipmap ) {
				textureImporter.generateMipsInLinearSpace = true;
			}
			if( overrideNPOTScale ) {
				textureImporter.npotScale = textureImporterNPOTScale;
			}
			if( overrideMaxTextureSize ) {
				textureImporter.maxTextureSize = maxTextureSize;
			}
			AssetDatabase.WriteImportSettingsIfDirty( assetPath );
			AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
		}

		GameObject go = new GameObject( texture.name );
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = Vector3.one;

		if( string.IsNullOrEmpty( assetPath ) ) {
			Debug.LogError("");
			return;
		}

		string directoryName = Path.GetDirectoryName( assetPath );
		string fileName = Path.GetFileNameWithoutExtension( assetPath );
		string baseAssetPath = Path.Combine( directoryName, fileName );
		string materialPath = baseAssetPath + ".mat";

		Material material = null;
		bool isExistsAssetMaterial = false;
		if( _isCreateAsset ) {
			material = (Material)AssetDatabase.LoadAssetAtPath( materialPath, typeof(Material) );
			isExistsAssetMaterial = (material != null);
			if( isExistsAssetMaterial ) {
				material.shader = Shader.Find( _GetShaderName() );
			}
		}

		if( material == null ) {
			material = new Material( Shader.Find( _GetShaderName() ) );
		}

		material.mainTexture = texture;

		System.Text.StringBuilder postfixMeshName = new System.Text.StringBuilder();
		
		postfixMeshName.Append( "_" );
		if( _anchorType.ToString().StartsWith("Top") ) {
			postfixMeshName.Append( "t" );
		} else if( _anchorType.ToString().StartsWith("Bottom") ) {
			postfixMeshName.Append( "b" );
		} else {
			postfixMeshName.Append( "c" );
		}
		
		if( _anchorType.ToString().StartsWith("Left") ) {
			postfixMeshName.Append( "l" );
		} else if( _anchorType.ToString().StartsWith("Right") ) {
			postfixMeshName.Append( "r" );
		} else {
			postfixMeshName.Append( "c" );
		}
		
		postfixMeshName.Append( "_" );
		postfixMeshName.Append( _scaleBy.ToString().Substring( 0, 1 ).ToLower() );
		postfixMeshName.Append( _size.ToString() );
		string meshPath = baseAssetPath + postfixMeshName.ToString() + ".asset";

		Mesh mesh = null;
		bool isExistsAssetMesh = false;
		if( _isCreateAsset ) {
			mesh = (Mesh)AssetDatabase.LoadAssetAtPath( meshPath, typeof(Mesh) );
			isExistsAssetMesh = (mesh != null);
		}

		float w = (float)width;
		float h = (float)height;
		switch( _scaleBy ) {
		case ScaleBy.Pixel:
			w *= _size;
			h *= _size;
			break;
		case ScaleBy.Width:
			h *= _size / w;
			w = _size;
			break;
		case ScaleBy.Height:
			w *= _size / h;
			h = _size;
			break;
		}
		float x = 0.0f, y = 0.0f;
		switch( _anchorType ) {
		case AnchorType.TopLeft:
			x = 0.0f;
			y = -h;
			break;
		case AnchorType.Top:
			x = -w * 0.5f;
			y = -h;
			break;
		case AnchorType.TopRight:
			x = -w;
			y = -h;
			break;
		case AnchorType.Left:
			x = 0.0f;
			y = -h * 0.5f;
			break;
		case AnchorType.Center:
			x = -w * 0.5f;
			y = -h * 0.5f;
			break;
		case AnchorType.Right:
			x = -w;
			y = -h * 0.5f;
			break;
		case AnchorType.BottomLeft:
			x = 0.0f;
			y = 0.0f;
			break;
		case AnchorType.Bottom:
			x = -w * 0.5f;
			y = 0.0f;
			break;
		case AnchorType.BottomRight:
			x = -w;
			y = 0.0f;
			break;
		}

		if( mesh == null ) {
			mesh = new Mesh();
			mesh.vertices = new Vector3[4] {
				new Vector3( x,      y + h, 0.0f ),
				new Vector3( x + w,  y + h, 0.0f ),
				new Vector3( x,      y, 0.0f ),
				new Vector3( x + w,  y, 0.0f ),
			};
			mesh.uv = new Vector2[4] {
				new Vector2( 0.0f, 1.0f ),
				new Vector2( 1.0f, 1.0f ),
				new Vector2( 0.0f, 0.0f ),
				new Vector2( 1.0f, 0.0f ),
			};
			mesh.triangles = new int[6] {
				0, 1, 2, 3, 2, 1,
			};
			
			mesh.RecalculateBounds();
			mesh.RecalculateNormals();
		}

		MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
		meshRenderer.castShadows = false;
		meshRenderer.receiveShadows = false;
		meshRenderer.sharedMaterial = material;
		MeshFilter meshFilter = go.AddComponent<MeshFilter>();
		meshFilter.sharedMesh = mesh;

		if( _isCreateAsset ) {
			if( !isExistsAssetMaterial ) {
				AssetDatabase.CreateAsset( material, materialPath );
			} else {
				EditorUtility.SetDirty( material );
				AssetDatabase.SaveAssets();
			}

			if( !isExistsAssetMesh ) {
				AssetDatabase.CreateAsset( mesh, meshPath );
			}
		}
	}
}
