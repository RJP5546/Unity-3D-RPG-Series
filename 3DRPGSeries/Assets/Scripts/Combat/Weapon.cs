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

        public void Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            if(equippedPrefab != null)
            {
                Transform handTransform;
                if(isRightHanded) { handTransform = rightHand; }
                else { handTransform = leftHand; }
                //sets if the weapon needs to spawn in the left or right hand
                Instantiate(equippedPrefab, handTransform);
                //instantiates the equipped weapon at the set hand transform
            }
            if(animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
                //changes the animator controller to the animatorOverride controller
            }
        }
        
        public float GetRange()
        {
            return weaponRange;
        }
        public float GetDamage()
        {
            return weaponDamage;
        }
    }
}
