using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class OutlineTextureMaker : EditorWindow
{
	Texture _texture;
	Color _outlineColor = Color.black;
	float _outlineSize = 4.0f;
	float _outlineFadeSize = 1.0f;

	[MenuItem ("Window/Outline Texture Maker")]
	static void Init()
	{
		EditorWindow.GetWindow(typeof(OutlineTextureMaker));
	}

	void OnGUI()
	{
		GUILayout.Label( "Settings", EditorStyles.boldLabel );
		_texture = (Texture)EditorGUILayout.ObjectField( "Texture", _texture, typeof(Texture), false );
		_outlineColor = EditorGUILayout.ColorField( "Outline Color", _outlineColor );
		_outlineSize = EditorGUILayout.FloatField( "Outline Size", _outlineSize );
		_outlineFadeSize = EditorGUILayout.FloatField( "Outline Fade Size", _outlineFadeSize );

		if( _outlineSize < 0.0f ) {
			_outlineSize = 0.0f;
		}
		if( _outlineFadeSize < 0.0f ) {
			_outlineFadeSize = 0.0f;
		}

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

	static Color _ColorA0( ref Color c )
	{
		return new Color( c.r, c.g, c.b, 0.0f );
	}

	void _Process()
	{
		Texture2D texture = _texture as Texture2D;
		if( texture == null ) {
			Debug.LogError( "Texture is not Texture2D." );
			return;
		}

		if( _outlineSize <= 0.0f ) {
			Debug.LogError( "_outlineSize is 0 or less." );
			return;
		}

		string assetPath = AssetDatabase.GetAssetPath( texture );
		TextureImporter textureImporter = (TextureImporter)TextureImporter.GetAtPath( assetPath );
		bool overrideReadable = false;
		bool overrideTextureFormat = false;
		bool overrideNPOTScale = false;
		bool overrideGenerateMipmap = false;
		bool overrideMaxTextureSize = false;
		TextureImporterFormat textureImporterFormat = TextureImporterFormat.ARGB32;
		TextureImporterNPOTScale textureImporterNPOTScale = TextureImporterNPOTScale.None;
		int maxTextureSize = 0;
		if( !textureImporter.isReadable ) {
			textureImporter.isReadable = true;
			overrideReadable = true;
		}
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
		if( overrideReadable || overrideTextureFormat || overrideGenerateMipmap || overrideNPOTScale || overrideMaxTextureSize ) {
			AssetDatabase.WriteImportSettingsIfDirty( assetPath );
			AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
		}

		int width = texture.width;
		int height = texture.height;
		Color[] pixels = texture.GetPixels();
		
		if( overrideReadable || overrideTextureFormat || overrideGenerateMipmap || overrideNPOTScale || overrideMaxTextureSize ) {
			if( overrideReadable ) {
				textureImporter.isReadable = false;
			}
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
			AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
		}

		int extentSize = (int)(_outlineSize + _outlineFadeSize + 0.5f + 1.0f);
		int modifyWidth = width + extentSize * 2;
		int modifyHeight = height + extentSize * 2;
		Texture2D modifyTexture = new Texture2D(
			modifyWidth,
			modifyHeight,
			TextureFormat.ARGB32,
			true, true );

		Color[] modifyPixels = modifyTexture.GetPixels();

		Color outlineColorA0 = _ColorA0( ref _outlineColor );
		for( int i = 0; i != modifyPixels.Length; ++i ) {
			modifyPixels[i] = outlineColorA0;
		}

		for( int y = 0; y != height; ++y ) {
			for( int x = 0; x != width; ++x ) {
				modifyPixels[(y + extentSize) * modifyWidth + x + extentSize] = pixels[y * width + x];
			}
		}

		int extentLengthSq = extentSize * extentSize;

		// Spread outline.
		for( int y = 0; y != modifyHeight; ++y ) {
			for( int x = 0; x != modifyWidth; ++x ) {
				int nearestLengthSq = -1;
				float nearestAlpha = -1.0f;

				int rx = x - extentSize;
				int ry = y - extentSize;
				for( int ny = -extentSize; ny <= extentSize; ++ny ) {
					for( int nx = -extentSize; nx <= extentSize; ++nx ) {
						int rrx = rx + nx;
						int rry = ry + ny;
						if( rrx >= 0 && rry >= 0 && rrx < width && rry < height ) {
							Color c = pixels[rry * width + rrx];
							if( c.a > 0.0f ) {
								int mx = x + nx;
								int my = y + ny;
								int lengthSq = (mx - x) * (mx - x) + (my - y) * (my - y);
								if( lengthSq <= extentLengthSq ) {
									if( nearestAlpha < 0.0f || nearestAlpha < c.a ) {
										nearestAlpha = c.a;
										nearestLengthSq = lengthSq;
									} else if( Mathf.Abs(nearestAlpha - c.a) <= Mathf.Epsilon && nearestLengthSq > lengthSq ) {
										nearestLengthSq = lengthSq;
									}
								}
							}
						}
					}
				}

				if( nearestAlpha >= 0.0f ) {
					float targetAlpha = _outlineColor.a * nearestAlpha;
					float nearestLength = Mathf.Sqrt( (float)nearestLengthSq );
					if( nearestLength > _outlineSize ) {
						if( _outlineFadeSize > 0.0f ) {
							float r = 1.0f - Mathf.Clamp01( (nearestLength - _outlineSize) / _outlineFadeSize );
							targetAlpha *= r;
						}
					}

					Color destColor = new Color(
						_outlineColor.r,
						_outlineColor.g,
						_outlineColor.b,
						targetAlpha );

					if( rx >= 0 && ry >= 0 && rx < width && ry < height ) {
						Color sourceColor = pixels[ry * width + rx];

						Color blendColor = new Color(
							sourceColor.r * sourceColor.a + destColor.r * (1.0f - sourceColor.a),
							sourceColor.g * sourceColor.a + destColor.g * (1.0f - sourceColor.a),
							sourceColor.b * sourceColor.a + destColor.b * (1.0f - sourceColor.a),
							Mathf.Max( sourceColor.a, destColor.a ) );

						modifyPixels[y * modifyWidth + x] = blendColor;
					} else {
						modifyPixels[y * modifyWidth + x] = destColor;
					}
				}
			}
		}

		modifyTexture.SetPixels( modifyPixels );

		string modifyPath = Path.Combine(
			Path.GetDirectoryName( assetPath ),
			Path.GetFileNameWithoutExtension( assetPath ) )
			+ "(Outline).png";

		_SaveTexture( modifyTexture, modifyPath );
	}

	void _SaveTexture( Texture2D texture, string assetPath )
	{
		if( texture == null || assetPath == null ) {
			return;
		}

		byte[] modifyBytes = texture.EncodeToPNG();
		File.WriteAllBytes( assetPath, modifyBytes );
		AssetDatabase.Refresh();

		TextureImporter textureImporter = (TextureImporter)TextureImporter.GetAtPath( assetPath );
		textureImporter.alphaIsTransparency = true;

		AssetDatabase.WriteImportSettingsIfDirty( assetPath );
		AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport );
	}
}
