using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HydraGOAP{

	/**
	 * Base creature agent. Other creatures may implement different goals.
	 * You should subclass this for specific agent classes and implement
	 * the createGoalState() method that will populate the goal for the GOAP
	 * planner.
	 */
	public abstract class BaseAgent : MonoBehaviour, IGoap
	{
		public CreatureUnit unit;      // Reference to the Creature class
        NavMeshAgent nav;      // Reference to the nav mesh agent.
        public float distance = 0;

		void Awake()
		{
		}

        void Start ()
		{
			nav = gameObject.GetComponent<NavMeshAgent> ();
            unit = gameObject.GetComponent<CreatureUnit>();
		}

		void Update ()
		{

		}

		/*
		Key-Value data that will feed the GOAP actions and system while planning.
        This is the state of the world as the agent knows.
		*/
		public HashSet<KeyValuePair<string,object>> getWorldState () {
			HashSet<KeyValuePair<string,object>> worldData = new HashSet<KeyValuePair<string,object>> ();

            worldData.Add(new KeyValuePair<string, object>("hasEaten", (!unit.isHungry)));


            return worldData;
		}

		/**
		 * Implement in subclasses
		 */
		public abstract HashSet<KeyValuePair<string,object>> createGoalState ();


		public void planFailed (HashSet<KeyValuePair<string, object>> failedGoal)
		{
			// Not handling this here since we are making sure our goals will always succeed.
			// But normally you want to make sure the world state has changed before running
			// the same goal again, or else it will just fail.
		}

		public void planFound (HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
		{
			// Yay we found a plan for our goal
			//Debug.Log ("<color=green>Plan found</color> "+GoapAgent.prettyPrint(actions));
		}

		public void actionsFinished ()
		{
			// Everything is done, we completed our actions for this gool. Hooray!
			//Debug.Log ("<color=blue>Actions completed</color>");
		}

		public void planAborted (GoapAction aborter)
		{
			// An action bailed out of the plan. State has been reset to plan again.
			// Take note of what happened and make sure if you run the same goal again
			// that it can succeed.
			//Debug.Log ("<color=red>Plan Aborted</color> "+GoapAgent.prettyPrint(aborter));
		}

		public bool moveAgent(GoapAction nextAction) {
            // move towards the NextAction's target using navMeshAgent
            if (!unit.isDead)
            {
                nav.SetDestination(nextAction.target.transform.position);
            }
            distance = Vector3.Distance(transform.position, nextAction.target.transform.position);
			if (distance <= 2.5) {
				// we are at the target location, we are done
				nextAction.setInRange(true);
				return true;
			} else
				return false;
		}
	}
}
