using UnityEngine;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] float healthPoints;
        //set the healthPoints value in the inspector, will be changed based on stats later
        bool isDead = false;
        //default is alive

        public void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }


        public bool IsDead()
        {
            return isDead;
            //lets other classes know if object is dead.
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0f);
            //sets healthPoints to whats higher, either healthPoints- damage, or 0. This prevents healthPoints from going below 0
            if(healthPoints == 0f)
            {
                Die();
                //when the object health hits 0, call Die().
                AwardExpierence(instigator);
                //award expierence to the instagator
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetHealth());
            //returns the health of the player as a percentage
        }

        private void Die()
        {
            if (isDead) { return; }
            //if dead ignore
            isDead = true;
            //if first time dead, set dead to true
            GetComponent<Animator>().SetTrigger("die");
            //playes the death animation
            GetComponent<ActionScheduler>().CancelCurrentAction();
            //stops the current action
        }

        private void AwardExpierence(GameObject instigator)
        {
            Expierence expierence = instigator.GetComponent<Expierence>();
            //set refrence to the expierence component
            if(expierence == null) { return; }
            //if there is no expierence component, ignore
            expierence.GainExpierence(GetComponent<BaseStats>().GetExpierenceReward());
            //gain expierence from the BaseStats GetExpierenceReward method
        }

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(healthPoints);
        }
        public void RestoreFromJToken(JToken state)
        {
            healthPoints = state.ToObject<float>();
            if (healthPoints <= 0f)
            {
                Die();
            }
        }
    }
}

