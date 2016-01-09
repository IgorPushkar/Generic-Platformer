using UnityEngine;
using System.Collections;

public class PickupRandomizer : MonoBehaviour {

	public Mesh[] meshes;

	private MeshFilter meshFilter;

	void Start () {
		meshFilter = GetComponent<MeshFilter> ();
		int index = Random.value > 0.5f ? 1 : 0;
		meshFilter.mesh = meshes [index];
	}
}
