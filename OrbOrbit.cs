using UnityEngine;
using System.Collections;

// Create an empty game object -- this will be the center object
// add a child gameObject -- this will be the orbiting object
// set the x and y position of the gameObject
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]

public class OrbOrbit : MonoBehaviour {


	public GameObject centerObject;   			// empty gameobject.  probably replace with "this"
	public Transform orbitObject;  				// orbiting object
	private Mesh orbitMesh;
	public Vector3 rotationAxis = Vector3.up;   // what axis are we orbiting about. 
	//public Vector3 desiredPosition;
	public float radius = 2.0f;
	public float radiusSpeed = 0.5f;
	public float rotationSpeed = 80.0f;

	void Start () {
		orbitObject = centerObject.transform;
		transform.position = (transform.position - orbitObject.position).normalized * radius + orbitObject.position;
		radius = 2.0f;
	}

	void Update () {
		transform.RotateAround (orbitObject.position, rotationAxis, rotationSpeed * Time.deltaTime);

		// if we wanted to use the orbiting object as a weapon that moves from the enemy creature we could use
		// the following.  the desired position would be the position of the player. it's currently set to
		// move towards the centerObject which we would need when we want it to come back to the creature
		//desiredPosition = (transform.position - orbitObject.position).normalized * radius + orbitObject.position;
		//transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
	}

	// once I figure out how to do this, we can change the mesh and material of the orbiting object
	void UpdateParameters(Mesh newMesh){

		//orbitMesh = newMesh;
	}
}