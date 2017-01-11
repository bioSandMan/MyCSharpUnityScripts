using UnityEngine;
using System.Collections;

/*
A simple perspective sense
Attach a capsule collider to the agent
Make the capsule collider shape and center position
as if it is the agent's peripheral vision.
Leave a little space behind to act
as it's ability to hear behind.

    Agent must have a rigidbody.

*/

namespace HydraGOAP
{
	public class Awareness : MonoBehaviour
	{
	
		private Transform targetTrans = null;
		private Vector3 rayDirection;
		public Aspect.aspect targetAspect;
	
		//Detect perspective field of view for the AI Character
		void OnTriggerEnter (Collider other)
		{
			print ("Found somethin");
			// get the transform of the object
			targetTrans = other.GetComponent<Transform> ();
			// get the aspect of the object
			Aspect aspect = other.GetComponent<Aspect> ();
			if (aspect != null) {
				// Is this the droid we're looking for?
				if (aspect.aspectName == targetAspect) {
					print ("Enemy Detected");
				}
			}
		}
	}
}
 
