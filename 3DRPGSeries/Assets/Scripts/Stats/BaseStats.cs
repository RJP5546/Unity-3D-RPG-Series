using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(0, 99)]
        //the range of possible levels the player can be
        [SerializeField] int startingLevel = 1;
        //character level
        [SerializeField] CharacterClass characterClass;
        //The type of class the character is, refrenced enum from CharacterClass file
        [SerializeField] Progression progression = null;
        //set the progression aspect in the editor

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
            //calls the GetHealth() method from Progression and returns the float, this chain refrence prevents circular dependancies
        }
    }
}
