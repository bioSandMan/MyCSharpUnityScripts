using UnityEngine;
using System.Collections;

public class LobMonGen : MonoBehaviour {

	// Instantiates a prefab in a grid

	public GameObject prefab;
	public GameObject parent;
	public Mesh head;
	public Mesh body;
	public Mesh legs;
	public Material material;
	public int maxDepth;
	public float childScale;
	public float spacing = 2f;

	void Start() {
		parent.AddComponent<MeshFilter> ().mesh = head;
		parent.AddComponent<MeshRenderer>().material = material;
		for (int y = 0; y < maxDepth; y++) {
			int z = y + 1;
			Vector3 pos = new Vector3 (0, 0, z) * spacing*childScale;
			GameObject bodyObject = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
			bodyObject.transform.parent = parent.transform;
			bodyObject.AddComponent<MeshFilter> ().mesh = body;
			bodyObject.transform.localScale = Vector3.one * childScale;
		}
	}
}
