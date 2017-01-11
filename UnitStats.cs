using UnityEngine;
using System.Collections;

// attach this to an empty game object
// as the unit's character sheet
// this needs a game manager that applies 
// damage and effects
namespace HydraEnemyGenerator{

	public class UnitStats : MonoBehaviour {


		// default stats
		public float defaultHP=10;
		public float fullHP=10;
		public float HP=10;
		public float HPRegenRate=0;
		public float HPStaggerDuration=10; // take damage over time
		private float currentHPStagger=0;

		public float defaultShield=0;
		public float fullShield=0;
		public float shield=0;
		public float shieldRegenRate=1;
		public float shieldStaggerDuration=1; // take damage over time
		private float currentShieldStagger=0;



		// Immunities
		public bool immuneToCrit=false;
		public bool immuneToSlow=false;
		public bool immuneToStun=false;
		public bool immuneToFire = false;
		public bool immuneToElectric = false;
		public bool immuneToCold = false;

		// Possible states
		public bool dead=false;
		public bool stunned=false;

		// state durations and multipliers
		private float stunDuration=0;
		public float slowMultiplier=1;

		public bool hasTarget = false;

		private void Awake()
		{
			dead=false;
			stunned=false;

			fullHP=GetFullHP();
			HP=fullHP;
			fullShield=GetFullShield();
			shield=fullShield;

			currentHPStagger=0;
			currentShieldStagger=0;

		}



		private void Update () 
		{

		}

		private float GetFullHP(){ return defaultHP; }
		private float GetFullShield(){ return defaultShield; }
		private float GetHPRegenRate(){ return HPRegenRate; }
		private float GetShieldRegenRate(){ return shieldRegenRate; }
		private float GetHPStaggerDuration(){ return HPStaggerDuration; }
		private float GetShieldStaggerDuration(){ return shieldStaggerDuration; }

		public void FixedUpdate() {

			// regenerate health over time
			if(HPRegenRate>0 && currentHPStagger<=0){
				HP+=GetHPRegenRate()*Time.fixedDeltaTime;
				HP=Mathf.Clamp(HP, 0, fullHP);
			}

			//regenerate shield over time
			if(fullShield>0 && shieldRegenRate>0 && currentShieldStagger<=0){
				shield+=GetShieldRegenRate()*Time.fixedDeltaTime;
				shield=Mathf.Clamp(shield, 0, fullShield);
			}


			currentHPStagger-=Time.fixedDeltaTime;
			currentShieldStagger-=Time.fixedDeltaTime;
		}

		//for ability and what not
		public void ApplyDamage(float dmg){
			DamageHP(dmg);
			if(HP<=0) Dead();
		}

		public void RestoreHP(float value){
			//new TextOverlay(thisT.position, value.ToString("f0"), new Color(0f, 1f, .4f, 1f));
			HP=Mathf.Clamp(HP+value, 0, fullHP);
		}


		//called when unit take damage
		void DamageHP(float dmg){
			HP-=dmg;
			//new TextOverlay(thisT.position, dmg.ToString("f1"), new Color(1f, 1f, 1f, 1f));
			//if(onDamagedE!=null) onDamagedE(this);

			currentHPStagger=HPStaggerDuration;
			currentShieldStagger=shieldStaggerDuration;
		}

		public void Dead(){
			dead=true;

		}


	}
}
