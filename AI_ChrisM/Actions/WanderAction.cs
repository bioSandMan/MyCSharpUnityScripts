using UnityEngine;
using System.Collections;

namespace HydraGOAP
{
	public class WanderAction : GoapAction
	{

		public bool hasRandomTarget = false;
		public float range = 30.0f;
		private Vector3 result;

		public WanderAction () 
		{
			addPrecondition ("hasRandomTarget", false); // don't get food if we already have one
			addEffect ("hasRandomTarget", true); // we now have a food
		}
			
		public override void reset ()
		{
			hasRandomTarget = false;
			result = Vector3.zero;
		}

		public override bool isDone ()
		{
			return hasRandomTarget;
		}

		public override bool requiresInRange ()
		{
			return false; // just need to be out in the open
		}

		public override bool checkProceduralPrecondition (GameObject agent)
		{

			RandomPoint (transform.position, range, out result);

			if (result == Vector3.zero)
				return false;
			
			target.transform.position = result;
			return result != Vector3.zero;
		}

		public override bool perform (GameObject agent)
		{
			if(target.transform.position != Vector3.zero) {
				hasRandomTarget = true;
				return true;
			} else { // some reason we didn't get a target
				return false;
			}
		}

		// http://docs.unity3d.com/ScriptReference/NavMesh.SamplePosition.html
		bool RandomPoint (Vector3 center, float range, out Vector3 point)
		{
			for (int i = 0; i < 30; i++) {
				Vector3 randomPoint = center + Random.insideUnitSphere * range;
				NavMeshHit hit;
				if (NavMesh.SamplePosition (randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
					point = hit.position;
					return true;
				}
			}
			point = Vector3.zero;
			return false;
		}
	}
}


