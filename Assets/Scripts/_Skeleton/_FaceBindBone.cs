using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _FaceBindBone : MonoBehaviour {

	public Mesh originMesh;
	public Texture2D renderTexture;

	public _BoneFragment leftEye, rightEye, nose, mouth;
	public Transform upper, lower, lefter, righter, leftMouthCorner, rightMouthCorner;

	float origin_w = 0;//, origin_h;
	float origin_c_y = 0;

	void Start () {

		origin_w = lefter.position.x - righter.position.x;
		origin_c_y = (upper.position.y + lower.position.y) * 0.5f;
		//origin_h = upper.position.y - lower.position.y;

		gameObject.AddComponent<Animation>();
		gameObject.AddComponent<SkinnedMeshRenderer>();
		SkinnedMeshRenderer rend = GetComponent<SkinnedMeshRenderer>();
		rend.quality = SkinQuality.Bone2;
//		Animation anim = GetComponent<Animation>();


		int size = originMesh.vertices.Length;
		Mesh mesh = new Mesh();
		
		////////////VERTS
		List<Vector3> verts = new List<Vector3>();
		for(int i = 0; i < originMesh.vertices.Length; i++)
		{
			Vector3 v = originMesh.vertices[i];
			verts.Add(new Vector3(v.x, v.y, v.z));
		}
		mesh.vertices = verts.ToArray();

		////////////UVS
		List<Vector2> uvs = new List<Vector2>();
		for(int i = 0; i < originMesh.uv.Length; i++)
		{
			Vector2 uv = originMesh.uv[i];
			uvs.Add(new Vector2(uv.x, uv.y));
		}
		mesh.uv = uvs.ToArray();
		
		////////////TRIS
		List<int> tris = new List<int>();
		for(int i = 0; i < originMesh.triangles.Length; i++)
		{
			int t = originMesh.triangles[i];
			tris.Add(t);
		}
		mesh.triangles = tris.ToArray();
		mesh.RecalculateNormals();

		///
		/// Binds the features.
		/// 0.left_eye 1.right_eye 
		/// 2.nose 
		/// 3.left_mouth_corner 4.right_mouth_corner
		/// 5.upper 6.lower 
		/// 7.left 8.right
		/// 
		Transform[] bones = new Transform[9];
		Matrix4x4[] bindPoses = new Matrix4x4[9];
		///leftEye
		bones[0] = leftEye.transform;
		bindPoses[0] = bones[0].worldToLocalMatrix * transform.localToWorldMatrix;

		///rightEye
		bones[1] = rightEye.transform;
		bindPoses[1] = bones[1].worldToLocalMatrix * transform.localToWorldMatrix;

		///nose
		bones[2] = nose.transform;
		bindPoses[2] = bones[2].worldToLocalMatrix * transform.localToWorldMatrix;

		///leftMouthCorner
		bones[3] = leftMouthCorner.transform;
		bindPoses[3] = bones[3].worldToLocalMatrix * transform.localToWorldMatrix;

		///rightMouthCorner
		bones[4] = rightMouthCorner.transform;
		bindPoses[4] = bones[4].worldToLocalMatrix * transform.localToWorldMatrix;

		///Upper
		bones[5] = upper;
		bindPoses[5] = bones[5].worldToLocalMatrix * transform.localToWorldMatrix;

		///Lower
		bones[6] = lower;
		bindPoses[6] = bones[6].worldToLocalMatrix * transform.localToWorldMatrix;

		///Lefter
		bones[7] = lefter;
		bindPoses[7] = bones[7].worldToLocalMatrix * transform.localToWorldMatrix;

		///Righter
		bones[8] = righter;
		bindPoses[8] = bones[8].worldToLocalMatrix * transform.localToWorldMatrix;

//		///Forward
//		bones[9] = new GameObject("Forward").transform;
//		bones[9].parent = transform;
//		bones[9].localRotation = Quaternion.identity;
//		bones[9].localPosition =  new Vector3(0f, 0f, 1.0f);//Vector3.zero;
//		bindPoses[9] = bones[9].worldToLocalMatrix * transform.localToWorldMatrix;
//		///Back
//		bones[10] = new GameObject("Back").transform;
//		bones[10].parent = transform;
//		bones[10].localRotation = Quaternion.identity;
//		bones[10].localPosition = new Vector3(0f, 0f, -1.4f);
//		bindPoses[10] = bones[10].worldToLocalMatrix * transform.localToWorldMatrix;

		mesh.bindposes = bindPoses;
		rend.bones = bones;
		rend.sharedMesh = mesh;
		rend.material = new Material(Shader.Find("Mobile/Diffuse"));
		rend.material.SetTexture(0, renderTexture);
//		
		//BONE WEIGHTS
		///
		List<BoneWeight> weights = new List<BoneWeight>();
		for(int i = 0; i < size; i++)
		{
			Vector3 v = verts[i];
			BoneWeight b = new BoneWeight();

			bool isForward = v.z > 0;
			if(leftEye.Contans(v) && isForward){
				float w = leftEye.AreaSize.x;
				float f = Mathf.Clamp(0.5f - Mathf.Abs (leftEye.DistanceXAxis(v)) / w, 0, 1);

				b.boneIndex0 = 0;
				b.weight0 = f;

				b.boneIndex1 = 7; //left
				b.weight1 = 1.0f - f;

			}
			else if(rightEye.Contans(v)  && isForward){
				float w = rightEye.AreaSize.x;
				float f = Mathf.Clamp(0.5f - Mathf.Abs (rightEye.DistanceXAxis(v)) / w, 0, 1);
				b.boneIndex0 = 1;
				b.weight0 = f;

				b.boneIndex1 = 8;//right
				b.weight1 = 1.0f - f;

			}
			else if(nose.Contans(v)  && isForward){
				float h = nose.AreaSize.y;
				float f =  Mathf.Clamp(0.5f - Mathf.Abs (nose.DistanceYAxis(v)) / h, 0, 1);
				b.boneIndex0 = 2;
				b.weight0 = f;

				if(v.y > 0){
					b.boneIndex1 = 5;//upper
				}else{
					b.boneIndex1 = 6;//upper
				}
				b.weight1 = 1.0f - f;

			}
			else if(mouth.Contans(v)  && isForward){
				float w = mouth.AreaSize.x;//
				float x = mouth.DistanceXAxis(v);
				float f =  Mathf.Clamp(Mathf.Abs (x) / w * 0.5f, 0, 1);
				if(x > 0){
					b.boneIndex0 = 3;//leftCorner
					b.weight0 = f;
					b.boneIndex1 = 7; //left
					b.weight1 = 1.0f - f;
				}
				else{
					b.boneIndex0 = 4;//leftCorner
					b.weight0 = f;
					b.boneIndex1 = 8; //left
					b.weight1 = 1.0f - f;
				}

			}
			else{
				if(v.y > 0){
					b.boneIndex0 = 5;
					b.weight0 = v.y / Mathf.Abs (upper.position.y);
				}
				else 
				{
					b.boneIndex0 = 6;
					b.weight0 = - v.y / Mathf.Abs (lower.position.y);
				}
				
				float t = 1.0f - b.weight0;
				if(t < 0) t = 0;
				if(v.x > 0){
					b.boneIndex1 = 7;
					b.weight1 = t * v.x / Mathf.Abs(lefter.position.x);
				}
				else 
				{
					b.boneIndex1 = 8;
					b.weight1 = - t * v.x / Mathf.Abs(righter.position.x);
				}
			}
//			float h = (upper.position.y - lower.position.y);
//			float upWeight = (upper.position.y - v.y) / h ;
//
//			b.boneIndex0 = 5;
//			b.weight0 = 1.0f - upWeight;
//			b.boneIndex1 = 6;
//			b.weight1 = upWeight;


			weights.Add(b);
		}
		mesh.boneWeights = weights.ToArray();

		//Test......
		//TODO....
//		OnUpdateFixedBones(new Vector2[]{////
//			new Vector2(213.3f, 254.5f),
//			new Vector2(411.0f, 263.0f),
//			new Vector2(312.0f, 371.5f),
//			new Vector2(213.0f, 485.0f),
//			new Vector2(403.0f, 494.0f),
//			new Vector2(104.0f, 368.0f),
//			new Vector2(495.0f, 375.0f),
//			new Vector2(301.0f, 626.0f),
//			new Vector2(317.0f, 145.5f)
//		});
	}

	public void OnUpdateBones(Vector2[] points){
		if(points.Length != 9){
			Debug.Log ("NEED　9 POINTS FOR FACE BIND BONES !!!");
			return;
		}

		List<Vector2> pts = new List<Vector2>(points);

		pts.Sort(delegate(Vector2 x, Vector2 y) {
			if(x.x < y.x) return -1;
			else if (x.x > y.x) return 1;
			else return 0;
		} );

		Vector2 r = pts[0];
		Vector2 l = pts[pts.Count - 1];

		pts.Sort(delegate(Vector2 x, Vector2 y) {
			if (x.y > y.y) return -1;
			else if(x.y < y.y) return 1;
			else return 0;
		} );

		Vector2 u = pts[0]; 
		Vector2 b = pts[pts.Count - 1];

		pts.Remove(l); 
		pts.Remove(r);
		pts.Remove(u); 
		pts.Remove(b);

		//float h = u.y - b.y;
		float w = l.x - r.x;

		float c_x = (l.x + r.x) * 0.5f;
		float c_y = (u.y + b.y) * 0.5f;



		//origin_c_x = 0 ......
		float factor = origin_w / w;

		Vector3 pos = new Vector3(-c_x * factor,  (-c_y + origin_c_y) * factor, 0);
		Vector3 s = Vector3.one * factor;

//		Matrix4x4 m = Matrix4x4.Scale(s);
		Matrix4x4 m = Matrix4x4.TRS(pos, Quaternion.identity, s);

		int i = 0;
		foreach(Vector2 v in pts){
			Vector3 trs_p = m.MultiplyPoint3x4((Vector3)v);
			pts[i] = (Vector2) trs_p;
			i++;
		}

		upper.position = m.MultiplyPoint3x4((Vector3)u); 
		lower.position = m.MultiplyPoint3x4((Vector3)b); 
		righter.position = m.MultiplyPoint3x4((Vector3)r); 
		lefter.position = m.MultiplyPoint3x4((Vector3)l); 

		rightEye.transform.position = pts[1];
		leftEye.transform.position = pts[0];
		nose.transform.position = pts[2];
		rightMouthCorner.transform.position = pts[4];
		leftMouthCorner.transform.position = pts[3];

	}

	public void OnUpdateFixedBones(Vector2[] points){
		if(points.Length != 9){
			Debug.Log ("NEED　9 POINTS FOR FACE BIND BONES !!!");
			return;
		}
		
//		List<Vector2> points = new List<Vector2>(points);
		
//		points.Sort(delegate(Vector2 x, Vector2 y) {
//			if(x.x < y.x) return -1;
//			else if (x.x > y.x) return 1;
//			else return 0;
//		} );
		
		Vector2 r = points[5];
		Vector2 l = points[6];
		
//		points.Sort(delegate(Vector2 x, Vector2 y) {
//			if (x.y > y.y) return -1;
//			else if(x.y < y.y) return 1;
//			else return 0;
//		} );
		
		Vector2 u = points[7]; ////
		Vector2 b = points[8];
		
//		points.Remove(l); 
//		points.Remove(r);
//		points.Remove(u); 
//		points.Remove(b);
		
		//float h = u.y - b.y;
		float w = l.x - r.x;
		
		float c_x = (l.x + r.x) * 0.5f;
		float c_y = (u.y + b.y) * 0.5f;
		
		
		
		//origin_c_x = 0 ......
		float factor = origin_w / w;
		
		Vector3 pos = new Vector3(-c_x * factor,  (-c_y + origin_c_y) * factor, 0);
		Vector3 s = Vector3.one * factor;
		
		//		Matrix4x4 m = Matrix4x4.Scale(s);
		Matrix4x4 m = Matrix4x4.TRS(pos, Quaternion.identity, s);

		Vector2[] pts = new Vector2[9];
		int i = 0;
		foreach(Vector2 v in points){
			Vector3 temp_p = m.MultiplyPoint3x4((Vector3)v);
			pts[i] = (Vector2) temp_p;
			i++;
		}
		
//		upper.position = m.MultiplyPoint3x4((Vector3)u); 
//		lower.position = m.MultiplyPoint3x4((Vector3)b); 
//		righter.position = m.MultiplyPoint3x4((Vector3)r); 
//		lefter.position = m.MultiplyPoint3x4((Vector3)l); 
		upper.position = pts[7]; 
		lower.position = pts[8]; 
		righter.position = pts[5]; 
		lefter.position = pts[6]; 

		rightEye.transform.position = pts[3];
		leftEye.transform.position = pts[4];
		nose.transform.position = pts[2];
		rightMouthCorner.transform.position = pts[0];
		leftMouthCorner.transform.position = pts[1];
		
	}

		
		// Update is called once per frame
	void Update () {
	
	}
}
