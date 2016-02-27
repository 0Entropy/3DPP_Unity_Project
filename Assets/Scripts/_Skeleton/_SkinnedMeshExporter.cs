using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class _SkinnedMeshExporter : MonoBehaviour {

	//source models skinnedMeshRenderer
	public SkinnedMeshRenderer s;
//	public Transform rootBone;

	void Start()
	{
		
//		gameObject.AddComponent<Animation>(); //using sources animation to test.
//		gameObject.AddComponent<SkinnedMeshRenderer>();
		SkinnedMeshRenderer renderer = gameObject.AddComponent<SkinnedMeshRenderer>();//GetComponent<SkinnedMeshRenderer>();
		Mesh mesh = new Mesh();
		
		////////////VERTS
		List<Vector3> verts = new List<Vector3>();
		for(int i = 0; i < s.sharedMesh.vertices.Length; i++)
		{
			Vector3 v = s.sharedMesh.vertices[i];
			verts.Add(new Vector3(v.x, v.y, v.z));
		}
		mesh.vertices = verts.ToArray();
		
		////////////UVS
		List<Vector2> uvs = new List<Vector2>();
		for(int i = 0; i < s.sharedMesh.uv.Length; i++)
		{
			Vector2 uv = s.sharedMesh.uv[i];
			uvs.Add(new Vector2(uv.x, uv.y));
		}
		mesh.uv = uvs.ToArray();
		
		////////////TRIS
		List<int> tris = new List<int>();
		for(int i = 0; i < s.sharedMesh.triangles.Length; i++)
		{
			int t = s.sharedMesh.triangles[i];
			tris.Add(t);
		}
		mesh.triangles = tris.ToArray();
		mesh.RecalculateNormals();
		
		renderer.material = s.material;//new Material(Shader.Find(" Diffuse"));
		
		//BONE WEIGHTS
		List<BoneWeight> weights = new List<BoneWeight>();
		for(int i = 0; i < s.sharedMesh.boneWeights.Length; i++)
		{
			BoneWeight b = new BoneWeight();
			b.boneIndex0 = s.sharedMesh.boneWeights[i].boneIndex0;
			b.weight0 = s.sharedMesh.boneWeights[i].weight0;
			
			b.boneIndex1 = s.sharedMesh.boneWeights[i].boneIndex1;
			b.weight1 = s.sharedMesh.boneWeights[i].weight1;
			
			b.boneIndex2 = s.sharedMesh.boneWeights[i].boneIndex2;
			b.weight2 = s.sharedMesh.boneWeights[i].weight2;
			
			b.boneIndex3 = s.sharedMesh.boneWeights[i].boneIndex3;
			b.weight3 = s.sharedMesh.boneWeights[i].weight3;
			
			weights.Add(b);
		}
		mesh.boneWeights = weights.ToArray();
		
		
		//BINDPOSES
		List<Matrix4x4> bindPoses = new List<Matrix4x4>();
		for(int i = 0; i < s.sharedMesh.bindposes.Length; i++)
		{
			Matrix4x4 m = new Matrix4x4();
			Matrix4x4 mm = s.sharedMesh.bindposes[i];
			m.m00 = mm.m00;		m.m01 = mm.m01;		m.m02 = mm.m02;		m.m03 = mm.m03;
			m.m10 = mm.m10;		m.m11 = mm.m11;		m.m12 = mm.m12;	    m.m13 = mm.m13;	
			m.m20 = mm.m20;		m.m21 = mm.m21;		m.m22 = mm.m22;		m.m23 = mm.m23;		
			m.m30 = mm.m30;		m.m31 = mm.m31;		m.m32 = mm.m32;		m.m33 = mm.m33;	
			
			bindPoses.Add(m);
		}
		mesh.bindposes = bindPoses.ToArray();
		
		//using sources bones...could make a copy here instead.
		renderer.bones = s.bones;
		renderer.sharedMesh = mesh;
//		renderer.rootBone = s.rootBone;//rootBone;
		//play an animation here to test
		transform.parent.animation.Play("Take 001");//("Idel_Normal");
		
		
	}
}
