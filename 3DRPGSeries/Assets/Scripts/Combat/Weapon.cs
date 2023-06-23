using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    //adds a new interface option into the editor when you right click, can now create new weapon
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        //component that will override the players animations into the animations of the weapon class
        [SerializeField] GameObject equippedPrefab = null;
        //prefab for equipped weapon
        [SerializeField] float weaponRange = 2f;
        //sets the players weapon range, or the distance away from the enemy that the player stops to attack.
        [SerializeField] float weaponDamage = 5f;
        //sets the damage of player attacks, will be replaced by weapon properties later
        [SerializeField] bool isRightHanded = true;
        //set if the weapon is left or right handed
        [SerializeField] Projectile projectile = null;
        //sets the weapons projectile
        public void Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            if(equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                Instantiate(equippedPrefab, handTransform);
                //instantiates the equipped weapon at the set hand transform
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
                //changes the animator controller to the animatorOverride controller
            }
        }

        public bool HasProjectile()
        {
            return projectile != null;
            //lets us know if we have a projectile equipped
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            //creates a projectile from the proper hand and rotation
            projectileInstance.SetTarget(target, weaponDamage);
            //sets the target for the projectile and its damage
        }
        
        public float GetRange()
        {
            return weaponRange;
        }
        public float GetDamage()
        {
            return weaponDamage;
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) { handTransform = rightHand; }
            else { handTransform = leftHand; }
            //sets if the weapon needs to spawn in the left or right hand
            return handTransform;
        }
    }
}
