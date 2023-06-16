using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;
        //sets the players weapon range, or the distance away from the enemy that the player stops to attack.
        Transform target;
        //the transform of the combat target
        private void Update()
        {
            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;
            //if the distance between self, and target position is in range, set true.
            if(target != null && !isInRange)
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            //if the fighter has an active target and is in range, move to the target
            else
            {
                GetComponent<Mover>().Stop();
            }
        }
        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            print("Die you GameObject!");
        }
    }
}

