using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat 
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health;
        //set the health value in the inspector, will be changed based on stats later

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0f);
            //sets health to whats higher, either health- damage, or 0. This prevents health from going below 0
            print(health);
        }

    }
}

