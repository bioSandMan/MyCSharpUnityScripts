using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/*
This sets up our agent to have a goal in life
*/
namespace HydraGOAP
{
    public class CreatureAgent : BaseAgent

    {
        // Tell us what to do master!
        public string Goal = "collectResource";
        public override HashSet<KeyValuePair<string, object>> createGoalState()
        {
            HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
            // should be able to wrap these with if statements to get different goals from GM

            // this would be a base goal
			goal.Add(new KeyValuePair<string, object>(Goal, (unit.hunger < 2000)));

            return goal;
        }
    }
}
