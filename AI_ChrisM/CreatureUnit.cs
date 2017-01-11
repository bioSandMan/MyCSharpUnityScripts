using UnityEngine;
using System.Collections;

namespace HydraGOAP
{
    /* Creature class requires
     Creature Agent
     NavMeshAgent
     Goap Agent
     It also requires actions relavent to the goals
     */
    [RequireComponent(typeof(CreatureAgent))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(GoapAgent))]
    public class CreatureUnit : Unit
	{

		ManagerOfEnemies MoE;
        Animator anim;                      // Reference to the animator
        ParticleSystem hitParticles;        // Reference to the particle system that plays when the enemy is damaged
        bool isSinking;                     // whether the enemy has started sinking through the floor
        public float sinkSpeed = 0.5f;      // the speed at which the enemy sinks into the floor when dead
        CapsuleCollider capsuleCollider;    // Reference to the object box collider


        public override void Awake()
        {
            base.Awake();
            anim = GetComponent<Animator>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            thisObj = gameObject;
            MoE  = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<ManagerOfEnemies>();

        }

        // Use this for initialization
        public override void Start ()
		{
			base.Start ();

		}

		// Update is called once per frame
		public override void Update ()
		{
			base.Update ();


            if (isSinking)
            {
                // move the enemy down by the sinkSpeek per second
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime/3);
            }

        }

		public void AddGamete() {
			Enemy e = this.GetComponent<Enemy> ();
			if (e == null) {
				Debug.Log ("SHit");
			}
			if (MoE == null) {
				Debug.Log ("RAWRRRR");
			}

			MoE.addGamete (this.GetComponentInParent<Enemy>().genome.getChromosome());
		}

		// drain only applies to the enemy creature
		public override void FixedUpdate()
		{
			// energy should drain based on actions/use

			// actions should have an energy cost

			// fullness should drain as a function of action and time

			// fullness has a steady decay
			hunger -= hungerDrainAmnt;

			if (hunger <= fullHunger/3)
			{
				energy -= energyDrainAmnt ; // it's like reacting to low blood sugar
                health -= healthDrainAmnt; // helath should probably drain a little bit as well
                isHungry = true;
            }
            else
			{
				energy += energyRegenAmnt - healthRegenAmnt; // regen health at the cost of energy
				hunger -= energyRegenAmnt; // hunger should change by energy consumption
				health += healthRegenAmnt; // we should get a benefit for being full
                isHungry = false;
			}

			// unit can starve to death
			if (energy <= 0) ApplyDamage(0, energyDrainAmnt);
			if (health <= 0) Dead();


        }


        public override void ApplyDamage(_DamageType damage, float dmg)
        {
            base.ApplyDamage(damage, dmg);
            // play the particles.
            hitParticles.Play();
        }

        public override void Dead()
        {
            base.Dead();

            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.enabled = false;

            // Tell the animator that the enemy is dead.
            anim.Play("Death");

            // start sinking the enemy object into the dust from which it came
            StartSinking();
        }

        public void StartSinking()
        {
            // Find and disable the Nav Mesh Agent.
            GetComponent<NavMeshAgent>().enabled = false;

            // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
            GetComponent<Rigidbody>().isKinematic = true;

            // The enemy should now sink.
            isSinking = true;

            // Update the enemy manager system
            MoE.decrementCreatures();

            // After 5 seconds destory the enemy.
            Destroy(gameObject, 5f);
        }
    }
}
