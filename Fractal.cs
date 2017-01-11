// Set this script on an empty gameobject

using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

	// define the directions for child objects to be placed
	private static Vector3[] childDirections = {
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.forward,
		Vector3.back
	};

	// define orientation of child objects so they don't stack like blocks
	private static Quaternion[] childOrientations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 0f, -90f),
		Quaternion.Euler(0f, 0f, 90f),
		Quaternion.Euler(90f, 0f, 0f),
		Quaternion.Euler(-90f, 0f, 0f)
	};

	public Mesh[] meshes;						// array of meshes that can be used
	public Material material;					// single material for every mesh
	public int maxDepth;						// maximum depth of child objects
	public float childScale;					// 0.5 is best size so child objects don't touch
	public float spawnProbability;				// probability of selecting next mesh as child object
	public float maxRotationSpeed;				// speed of rotation for each individual object
	public float maxTwist;						// twist in orientation

	private float rotationSpeed;
	private int depth;
	private Material[,] materials;

	private void InitializeMaterials () {
		materials = new Material[maxDepth + 1, 2];
		for (int i = 0; i <= maxDepth; i++) {
			float t = i / (maxDepth - 1f);
			t *= t;
			materials[i, 0] = new Material(material);
			materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
			materials[i, 1] = new Material(material);
			materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
		}
		materials[maxDepth, 0].color = Color.magenta;
		materials[maxDepth, 1].color = Color.red;
	}

	private void Start () {
		rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
		transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);
		if (materials == null) {
			InitializeMaterials();
		}
		gameObject.AddComponent<MeshFilter> ().mesh = meshes [0];  // changed this so the center object is first mesh
			//meshes[Random.Range(0, meshes.Length)];
		gameObject.AddComponent<MeshRenderer>().material =
			materials[depth, Random.Range(0, 2)];
		if (depth < maxDepth) {
			StartCoroutine(CreateChildren());
		}
	}

	private IEnumerator CreateChildren () {
		for (int i = 0; i < childDirections.Length; i++) {
			if (Random.value < spawnProbability) {
				yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
				new GameObject("Fractal Child").AddComponent<Fractal>().
					Initialize(this, i);
			}
		}
	}

	private void Initialize (Fractal parent, int childIndex) {
		meshes = parent.meshes;
		materials = parent.materials;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		spawnProbability = parent.spawnProbability;
		maxRotationSpeed = parent.maxRotationSpeed;
		maxTwist = parent.maxTwist;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition =
			childDirections[childIndex] * (0.5f + 0.5f * childScale);
		transform.localRotation = childOrientations[childIndex];
	}


			public void UpdateParameters(
				Mesh[] meshes,
				Material material,
				int maxDepth,
				float childScale,
			 	float spawnProbability,
				float maxRotationSpeed,
				float maxTwist)
			{

				this.meshes = meshes;
				this.material = material;
				this.maxDepth = maxDepth;
				this.childScale = childScale;
				this.spawnProbability = spawnProbability;
				this.maxRotationSpeed = maxRotationSpeed;
				this.maxTwist = maxTwist;

			}


	private void Update () {
		transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
	}
}
