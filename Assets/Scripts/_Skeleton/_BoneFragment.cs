using UnityEngine;
using System.Collections;

public class _BoneFragment : MonoBehaviour {

//	public PolyMesh Area{set; get;}

	public bool Contans(Vector3 v){
		Vector3 p = (transform.parent.localToWorldMatrix*transform.worldToLocalMatrix).MultiplyPoint3x4(v);
		return CGAlgorithm.WN_PnPoly(p, GetComponentInChildren<PolyMesh>().GetEdgePoints());
	}

	public float DistanceXAxis(Vector3 v){
		Vector3 p = (transform.parent.localToWorldMatrix*transform.worldToLocalMatrix).MultiplyPoint3x4(v);
		return p.x;//Mathf.Abs(p.x - transform.position.x);

	}

	public float DistanceYAxis(Vector3 v){
		Vector3 p = (transform.parent.localToWorldMatrix*transform.worldToLocalMatrix).MultiplyPoint3x4(v);
		return p.y;//Mathf.Abs(p.y - transform.position.y);
		
	}

	public Vector3 AreaSize{
		get{
			return GetComponentInChildren<MeshFilter>().sharedMesh.bounds.size;
		}
	}
}
