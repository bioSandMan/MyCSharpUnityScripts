using UnityEngine;
using System.Collections;
using System;

public class Attack : GoapAction
{
    public bool isIdle = false;
    public bool hasTarget = false;
    public bool isWandering = false;
    public bool hasEnteredTrigger = false;
    public bool randomProbability = false;
    public bool hasReceivedEvent = false;
    public bool isProvoked = false;
    public bool isFleeing = false;
    public bool isUnderAttack = false;
    public bool isHunting = false;
    public bool isEating = false;

    public Attack()
    {
        // an attack requires provocation
        // hunger could provoke an attack
        addPrecondition("isProvoked", true);
        addPrecondition("isFleeing", false);
        addPrecondition("isStunned", false);
        addEffect("isUnderAttack", true);
        addEffect("isIdle", false);
        addEffect("isWandering", false);
        addEffect("isHunting", false);
        addEffect("isEating", true);
        addEffect("hasTarget", true);

    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        throw new NotImplementedException();
    }

    public override bool isDone()
    {
        throw new NotImplementedException();
    }

    public override bool perform(GameObject agent)
    {
        throw new NotImplementedException();
    }

    public override bool requiresInRange()
    {
        throw new NotImplementedException();
    }

    public override void reset()
    {
        throw new NotImplementedException();
    }
}
