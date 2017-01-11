
using System;
using UnityEngine;

namespace HydraGOAP
{
	public class PickUpResourceAction : GoapAction
	{
		private bool hasEaten = false;
		private Target targetSupply; // where we get the food from

        public PickUpResourceAction () {
            addEffect("collectResource", true); // this is the goal to acheive when hungry
		}
			
		public override void reset ()
		{
			hasEaten = false;
			targetSupply = null;
		}

		public override bool isDone ()
		{
			return hasEaten;
		}

		public override bool requiresInRange ()
		{
			return true; // yes we need to be near a supply pile so we can eat it
		}

		public override bool checkProceduralPrecondition (GameObject agent)
		{
			// find the nearest target Supplyply pile of civs that has spare civs
			Target[] supplyPiles = (Target[]) UnityEngine.GameObject.FindObjectsOfType ( typeof(Target) );
			Target closest = null;
			float closestDist = 0;

			foreach (Target supply in supplyPiles) {
				if (closest == null) {
					// first one, so choose it for now. not the best way to do this
					closest = supply;
					closestDist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
				} else {
					// is this one closer than the last?
					float dist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
					if (dist < closestDist) {
						// we found a closer one, use it
						closest = supply;
						closestDist = dist;
					}
				}
			}

			if (closest == null)
				return false;

			targetSupply = closest;
			target = targetSupply.gameObject;

			return closest != null;
		}

		public override bool perform (GameObject agent)
		{
            if (target != null) {
                Destroy(target);
				hasEaten = true; // tell goap that we're done
				CreatureUnit unit = (CreatureUnit)agent.GetComponent(typeof(CreatureUnit));
				unit.AddGamete ();

				unit.hunger += 100;
				return true;
			} else {
                // we got there but there was no civ available! Someone got there first. Cannot perform action
				return false;
			}
		}
	}
}