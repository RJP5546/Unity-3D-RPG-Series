using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        
        [SerializeField] float TimeBetweenAttacks;
        //sets the delay between player attacks, will be replaced by weapon properties later
        [SerializeField] Transform rightHandTransform = null;
        //transform of the players right hand that the weapon will be attaching to
        [SerializeField] Transform leftHandTransform = null;
        //transform of the players left hand that the weapon will be attaching to
        [SerializeField] Weapon defaultWeapon = null;
        //initialise the equipped weapon as null, can be assigned later
        
        Health target;
        //the Health component of the combat target, gives us acess to health methods (like IsDead()).
        float timeSinceLastAttack = Mathf.Infinity;
        //the time since the player last attacked, initialised as infinity so the first attack is always avalible without waiting
        Weapon currentWeapon = null;
        //track the players current weapon

        private void Start()
        {
            EquipWeapon(defaultWeapon);
            //spawn the default weapon in the players hand at the start
        }


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
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
                //move to the target, pass full speed fraction
            }
            //if the fighter has an active target and is in range, move to the target
            else
            {
                GetComponent<Mover>().Cancel();
                //stop moving to the target
                AttackBehavior();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            //set the current weapon
            Animator animator = GetComponent<Animator>();
            //gets local refrence to the animator
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            //tell the weapon class to spawn the weapon, passes the hand transform and animator for the object
        }

        private void AttackBehavior()
        {
            transform.LookAt(target.transform.position);
            //look at the target during attack
            if (timeSinceLastAttack > TimeBetweenAttacks)
            //if the time since last attack is greater than the cooldown between attacks, if true, able to attack.
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
                //resets time between attack

            }

        }

        private void TriggerAttack()
            //sets the animation triggers for attack to their proper values
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            //resets the trigger on stop attack, preventing a bug where it is stored upon exiting combat. If it is reset upon
            //entering combat, we wont have it already initialised upon initiating combat
            GetComponent<Animator>().SetTrigger("attack");
            //play the attack animation using attack trigger
            //This will trigger the Hit() event.
        }

        void Hit()
        //hit trigger on attack animation
        {
            if(target == null) { return; }
            //if there is no target when the event triggers, return null to prevent error
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
                //if the current weapon has a projectile, launch it (not hit the enemy event, its the animation trigger event)
            }
            else 
            {
                target.TakeDamage(currentWeapon.GetDamage());
                //makes the healthPoints component take the desired amount of damage 
            }

        }

        void Shoot()
            //trigger on bow animation attack, only here because we cant change the name in the animation
        {
            Hit();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
            //if the distance between self, and target position is in range, set true.
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            //Starts the attack action
            target = combatTarget.GetComponent<Health>();
            //sets the target to our combat target
            print("Die you GameObject!");
        }

        public void Cancel()
            //cancels attacking
        {
            StopAttack();
            target = null;
            //set current target to null
            GetComponent<Mover>().Cancel();
            //stop moving to the target
        }

        private void StopAttack()
        //sets the animation triggers for attack to their proper values upon exiting
        {
            GetComponent<Animator>().ResetTrigger("attack");
            //reset the attack tag to prevent improper storage upon exiting combat
            GetComponent<Animator>().SetTrigger("stopAttack");
            //cancels any attack animation and return to locomotion
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) { return false; }
            //if there is no target, object cannot attack it
            Health targetToTest = combatTarget.GetComponent<Health>();
            //sets the target to test as the current combat target
            return targetToTest != null && !targetToTest.IsDead();
            //returns if the target to test exists and returns if it is dead.
        }
        
    }
}

