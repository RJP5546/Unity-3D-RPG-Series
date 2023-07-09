using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;
        //local refrence to the damage text
        void Start()
        {
            Spawn(11);
        }

        public void Spawn(float damageAmmount)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
            //spawns the damage text prefab with the desired damage amount
        }
    }
}
