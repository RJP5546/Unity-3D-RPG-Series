using UnityEngine;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using GameDevTV.Utils;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, IJsonSaveable
    {

        [SerializeField] float regerationPercentage = 70;
        //percentage of max health that the player regenerates to on level up

        LazyValue<float> healthPoints;
        //uses the lazy value wrapper class to ensure initialization before refrence
        bool isDead = false;
        //default is alive

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
            //passes getInitialHealth to the lazy value, doesnt call on awake but passes the value for when it does initalize
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
            //returns the inital health value based on stats
        }

        public void Start()
        {
            healthPoints.ForceInit();
            //if health hasnt been accessed yet, this forces it to initialize
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regerationPercentage / 100);
            //sets regenHealthPoints to the new proper value for their level up, multiplied by the regen percentage
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);

        }

        public bool IsDead()
        {
            return isDead;
            //lets other classes know if object is dead.
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took Damage: "+ damage);

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0f);
            //sets healthPoints to whats higher, either healthPoints- damage, or 0. This prevents healthPoints from going below 0
            if(healthPoints.value == 0f)
            {
                Die();
                //when the object health hits 0, call Die().
                AwardExperience(instigator);
                //award experience to the instagator
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }
        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
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
            healthPoints.value = state.ToObject<float>();
            if (healthPoints.value <= 0f)
            {
                Die();
            }
        }
    }
}

