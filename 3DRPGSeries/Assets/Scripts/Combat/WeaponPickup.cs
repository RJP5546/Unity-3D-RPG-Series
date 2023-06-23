using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        //a refrence to what weapon is on the ground

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                //get the players fighter component and run EquipWeapon with the passed weapon type
                Destroy(gameObject);
                //destroy the weapon on the ground
            }
        }
    }
}
