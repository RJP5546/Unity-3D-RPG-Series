using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using RPG.Saving;

namespace RPG.Core 
{
    public class Health : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] float healthPoints;
        //set the healthPoints value in the inspector, will be changed based on stats later
        bool isDead = false;
        //default is alive
        public bool IsDead()
        {
            return isDead;
            //lets other classes know if object is dead.
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0f);
            //sets healthPoints to whats higher, either healthPoints- damage, or 0. This prevents healthPoints from going below 0
            print(healthPoints);
            if(healthPoints == 0f)
            {
                Die();
                //when the object health hits 0, call Die().
            }
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

