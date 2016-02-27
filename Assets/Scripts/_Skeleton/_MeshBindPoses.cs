using UnityEngine;
using System.Collections;

public class _MeshBindPoses : MonoBehaviour {
	public Texture2D _tex;
	void Start() {
		gameObject.AddComponent<Animation>();
		gameObject.AddComponent<SkinnedMeshRenderer>();

		SkinnedMeshRenderer rend = GetComponent<SkinnedMeshRenderer>();
		Animation anim = GetComponent<Animation>();

		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[] {new Vector3(-1, 0, 0), new Vector3(1, 0, 0), new Vector3(-1, 5, 0), new Vector3(1, 5, 0)};
		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1)};
		mesh.triangles = new int[] {0, 1, 2, 1, 3, 2};
		mesh.RecalculateNormals();


		BoneWeight[] weights = new BoneWeight[4];
		weights[0].boneIndex0 = 0;
		weights[0].weight0 = 1;
		weights[1].boneIndex0 = 0;
		weights[1].weight0 = 1;
		weights[2].boneIndex0 = 1;
		weights[2].weight0 = 1;
		weights[3].boneIndex0 = 1;
		weights[3].weight0 = 1;
		mesh.boneWeights = weights;

		Transform[] bones = new Transform[2];
		Matrix4x4[] bindPoses = new Matrix4x4[2];
		bones[0] = new GameObject("Lower").transform;
		bones[0].parent = transform;
		bones[0].localRotation = Quaternion.identity;
		bones[0].localPosition = Vector3.zero;
		bindPoses[0] = bones[0].worldToLocalMatrix * transform.localToWorldMatrix;
		bones[1] = new GameObject("Upper").transform;
		bones[1].parent = transform;
		bones[1].localRotation = Quaternion.identity;
		bones[1].localPosition = new Vector3(0, 5, 0);
		bindPoses[1] = bones[1].worldToLocalMatrix * transform.localToWorldMatrix;
		mesh.bindposes = bindPoses;

		rend.material = new Material(Shader.Find("Mobile/Diffuse"));
		rend.material.SetTexture(0, _tex);
		rend.bones = bones;
		rend.sharedMesh = mesh;

		AnimationCurve curve = new AnimationCurve();
		curve.keys = new Keyframe[] {new Keyframe(0, 0, 0, 0), new Keyframe(1, 3, 0, 0), new Keyframe(2, 0.0F, 0, 0)};
		AnimationClip clip = new AnimationClip();
		clip.SetCurve("Lower", typeof(Transform), "m_LocalPosition.z", curve);
		anim.AddClip(clip, "test");
		anim.Play("test");
	}
}
