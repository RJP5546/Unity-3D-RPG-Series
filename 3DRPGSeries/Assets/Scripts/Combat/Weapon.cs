using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    //adds a new interface option into the editor when you right click, can now create new weapon
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        //component that will override the players animations into the animations of the weapon class
        [SerializeField] GameObject weaponPrefab = null;
        //prefab for equipped weapon

        public void Spawn(Transform handTransform, Animator animator)
        {
            Instantiate(weaponPrefab, handTransform);
            //instantiates the weapon at the set hand transform
            animator.runtimeAnimatorController = animatorOverride;
            //changes the animator controller to the animatorOverride controller
        }
    }
}
