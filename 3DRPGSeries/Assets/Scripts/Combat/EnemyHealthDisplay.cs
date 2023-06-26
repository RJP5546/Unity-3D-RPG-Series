using RPG.Attributes;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Text healthText;
        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            //cache the players fighter component
            healthText = GetComponent<Text>();
        }

        private void Update()
        {
            if(fighter.GetTarget() == null) 
            {
                healthText.text = "N/A";
                //set the text to display there is no target
                return;
            }
            Health health = fighter.GetTarget();
            //cache the targets health component
            healthText.text = String.Format("{0:0}%",health.GetPercentage());
            //updates the health text on the UI to the current percentage of health remaining
            //{0:0} gives the number with no decimal places, {0:0:0} yeilds one decimal place and so on
        }
    }

}