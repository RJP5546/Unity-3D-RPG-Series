using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        //a refrence to what weapon is on the ground
        [SerializeField] float respawnTime = 5f;
        //time the object will be hidden before becomeing active again

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                //get the players fighter component and run EquipWeapon with the passed weapon type
                StartCoroutine(HideForSeconds(respawnTime));
                //starts the respawn timer and functions
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            //disables or enables the collider
            foreach (Transform child in transform)
                //gets all the children
            {
                child.gameObject.SetActive(shouldShow);
                //toggles all the children
            }
        }
    }
}
