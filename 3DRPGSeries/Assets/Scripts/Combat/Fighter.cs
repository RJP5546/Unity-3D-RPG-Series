using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        //sets the players weapon range, or the distance away from the enemy that the player stops to attack.
        [SerializeField] float TimeBetweenAttacks;
        //sets the delay between player attacks, will be replaced by weapon properties later
        [SerializeField] float WeaponDamage;
        //sets the damage of player attacks, will be replaced by weapon properties later
        Health target;
        //the Health component of the combat target, gives us acess to health methods (like IsDead()).
        float timeSinceLastAttack;
        //the time since the player last attacked
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) { return; }
            //if there is no target, do none of this
            if (target.IsDead()) { return; }
            //if target is dead, do none of this
            if (target != null && !GetIsInRange())
            //putting the GetIsInRange after target != null, this prevents null refrence error, as the function will only be called if there is a target
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
                //move to the target
            }
            //if the fighter has an active target and is in range, move to the target
            else
            {
                GetComponent<Mover>().Cancel();
                //stop moving to the target
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if(timeSinceLastAttack > TimeBetweenAttacks) 
            {
                GetComponent<Animator>().SetTrigger("attack");
                //play the attack animation using attack trigger
                //This will trigger the Hit() event.
                timeSinceLastAttack = 0f;
                //resets time between attack
                
            }

        }

        void Hit()
        //hit trigger on attack animation
        {
            target.TakeDamage(WeaponDamage);
            //makes the healthPoints component take the desired amount of damage
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
            //if the distance between self, and target position is in range, set true.
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            //Starts the attack action
            target = combatTarget.GetComponent<Health>();
            //sets the target to our combat target
            print("Die you GameObject!");
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            //cancels any attack animation and return to locomotion
            target = null;
            //set current target to null
        }

        
    }
}

