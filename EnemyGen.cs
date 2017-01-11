using UnityEngine;
using System.Collections;


// attach this to an empty game object
namespace HydraEnemyGenerator{
	[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
	[RequireComponent(typeof(GenFractal))]

	public class EnemyGen : MonoBehaviour {

		public enum ResistanceType {
			Fire,
			Cold,
			Electrical
		}

		public enum DamageType {
			Bite,
			Elemental,
			Projectile
		}
			
		public ResistanceType resistance;
		public DamageType damageType;

		// call this to create a number of orbiting objects
		private OrbOrbit orbiter;

		// Fractal body settings
		private GenFractal fractal;  // reference to the fractal function

		public Mesh[] fractalBody;
		public Material material;
		private Material[,] materials;
		public float spawnProbability = 70.0f;
		public float maxRotationSpeed = 35.0f;
		public float maxTwist = 25.0f;
		public int maxDepth = 2;
		public float childScale = 0.5f;

		public Mesh mesh;
		public Mesh armorMesh;
		public Mesh[] weapons;

		// Settings for the elemental 
		public GameObject elementalPrefab;

		// Settings for the orbiting object
		public GameObject armorPrefab;
		public Mesh orbitingMesh;

		// booleans for various selections
		public bool hasArmor;
		public bool hasResistance;
		public bool hasWeapon;
		public bool hasFractalCore;
		public bool isElemental;



		private void Awake()
		{
			if (hasFractalCore) {
				fractal = GetComponent<GenFractal>();
				InitFractalCore ();
			}

			if (hasArmor) {
				InitArmor ();
			}

			if (hasResistance) {
				InitResistance ();
			}

			if (hasWeapon) {
				InitWeapon ();
			}

			if (isElemental) {
				InitElemental ();
			}

		}
			

		private void Update () {
			
		}

		private void InitWeapon(){

		}
			

		private void InitResistance(){

		}


		private void InitArmor()
		{
			// instantiate a centered, angled and rotating empty parent and then a visible child armorMesh
			(Instantiate (armorPrefab, Vector3.up, Quaternion.identity) as GameObject).transform.parent = this.transform;

		}

		private void InitElemental(){
			// instantiate a prefab at Vector3.down
			(Instantiate (elementalPrefab, Vector3.down, Quaternion.identity) as GameObject).transform.parent = this.transform;

		}

		private void InitMaterial(){

		}

		private void InitFractalCore(){

			fractal.UpdateParameters(fractalBody, material, maxDepth, childScale, spawnProbability, maxRotationSpeed, maxTwist);

		}


	}
}
