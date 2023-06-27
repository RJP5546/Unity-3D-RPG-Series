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

        [SerializeField] float regerationPercentage = 70;
        //percentage of max health that the player regenerates to on level up

        float healthPoints = -1f;
        //set the healthPoints value to an unobtainable value on initialization
        bool isDead = false;
        //default is alive

        public void Start()
        {
            GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;
            //adds RegenerateHealth() to the list of items called on OnLevelUp
            if(healthPoints < 0f)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
                //prevents the overlap between save loading and the start funtion
            }
            
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regerationPercentage / 100);
            //sets regenHealthPoints to the new proper value for their level up, multiplied by the regen percentage
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);

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
                AwardExperience(instigator);
                //award experience to the instagator
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
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

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            //set refrence to the experience component
            if(experience == null) { return; }
            //if there is no experience component, ignore
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
            //gain experience from the BaseStats GetStat() method
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

