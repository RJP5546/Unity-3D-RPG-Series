using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        Text healthText;
        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            //cache the players health component
            healthText = GetComponent<Text>();
        }

        private void Update()
        {
            healthText.text = String.Format("{0:0}%",health.GetPercentage());
            //updates the health text on the UI to the current percentage of health remaining
            //{0:0} gives the number with no decimal places, {0:0:0} yeilds one decimal place and so on
        }
    }

}