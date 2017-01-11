using UnityEngine;
using System;
using System.Collections;


/* 
Basic Attributes: Energy, Health, Hunger, Armor, Resistances
and the functions that change them
*/

namespace HydraGOAP
{

	public class Unit : MonoBehaviour {

        // Damage by type. Really only changes reach
        // These are to be used as flags to determine damage calculations
        // the value is it's binary power of 2
        [Flags]
        public enum _DamageType
        {
            None = 0,
            Bite = 1,
            Elemental = 2,
            Projectile = 4,
            Charge = 8,
            Strike = 16,
            Fire = 32,
            Cold = 64,
            Electrical = 128
        }

        public _DamageType resistance; // we'll use this for the resitance type 
        public _DamageType damageType; // this should be declared by unit


		public enum _TargetPriority{Nearest, Weakest, Toughest, Random};
		public _TargetPriority targetPriority=_TargetPriority.Nearest;


        // health can regenerate at the cost of energy -- Rest Action
        public float fullHealth=1000; // maximum value of health
		public float health=1000; // current value of health
		public float healthRegenAmnt = 0; // regen health by this amount
		public float healthDrainAmnt = 0; // how much damage over time

        // armor can grow back by using energy storage -- Rest Action
		public float fullArmor=0; // max shield value
		public float armor=0; // current sheild value
		public float armorRegenAmnt=1; // amnount shield regenerates by
		public float armorDrainAmnt=0; // armor is only drained by damage

        // fullness requires food intake -- Eat Action
        public float fullHunger = 100; // max fullness
        public float hunger = 100; //100 means full and doesn't need to eat
        public float hungerDrainAmnt = 0.01f;  // amount fullness drains over time -- metabolism

        // energy regenerates by draining fullness 
        public float fullEnergy = 1000; // max energy
        public float energy = 1000; // current energy
        public float energyDrainAmnt = 0.1f; // energy should drain based on activity
        public float energyRegenAmnt = 1; // energy should regen slowly

		// Immunities -- SelfSplanatory -- not currently being applied
		public bool immuneToCrit=false;
		public bool immuneToSlow=false;
		public bool immuneToStun=false;
		public bool immuneToFire = false;
		public bool immuneToElectric = false;
		public bool immuneToCold = false;

		// Possible states -- Updated by external actions
		public bool isDead=false;
		public bool isStunned=false;
        public bool isSlowed = false;
        public bool hasArmor = false;
        public bool hasResistance = false;
        public bool hasWeapon = false;
        public bool isElemental = false;

        // conditionals for goap agent to update and use
        public bool isHungry = false;
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
 
        // state durations and multipliers
        public float stunDuration=0;
		public float slowMultiplier=1;

		[HideInInspector] public GameObject thisObj;
		[HideInInspector] public Transform thisT;

		public virtual void Awake()
		{
			thisObj=gameObject;
			thisT=transform;
		}

		public void Init()
		{

		}
			
		public virtual void Start() 
		{

		}

		public virtual void Update () 
		{

		}


		public virtual void FixedUpdate() 
		{


		}

        // what do we do when the thing dies? 
		// make these abstract since this is general to civ and enemy
		// use override void to make changes
        public virtual void Dead()
        {
            isDead = true;
        }

        // Functions below are for updating stats based on world interactions
        // damage taken during combat
        public virtual void ApplyDamage(_DamageType damage, float dmg)
        {
            if(damage !=0 && resistance != damage) // simple for now
            {
                healthDrainAmnt += dmg; // drain additional damage over time
                health -= dmg*3 ; // no resistance incurs damage muliplier
            }
            else
            {
                //health -= dmg/3; // resistance is only 1/3 damage and no damage over time
				health -= (dmg * (1-armor));
            }

		}

        // call by external actions. value should be by action
		// only using these for creature/enemy
        public void DrainEnergy(float value)
        {
            energy -= value;
            if (energy <= 0) Dead(); // this could be changed to stunned
        }

        // drain fullness by some external force
        public void DrainFullness(float value)
        {
            hunger -= value;
        }

        // could something external increase energy?
        public void RestoreEnergy(float value)
        {
            energy += value;
            energy = Mathf.Clamp(energy, 0, fullEnergy);
        }

        // call this when action is eat
        public void RestoreFullness(float value)
        {
            hunger += value;
            hunger = Mathf.Clamp(hunger, 0, fullHunger);
        }

        // use this if we are somehow healed by an interaction
		public void RestoreHP(float value)
        {
            health += value;
            health = Mathf.Clamp(health, 0, fullHealth); // HP can't go higher than fullHP
		}

    }
}
