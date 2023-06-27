using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(0, 99)]
        //the range of possible level the player can be
        [SerializeField] int startingLevel = 1;
        //character level
        [SerializeField] CharacterClass characterClass;
        //The type of class the character is, refrenced enum from CharacterClass file
        [SerializeField] Progression progression = null;
        //set the progression aspect in the editor
        [SerializeField] GameObject levelUpParticleEffect = null;
        //set the particle effect to be generated upon leveling up

        public event Action OnLevelUp;
        //action to be called on level up

        int currentLevel = 0;
        //initialize the level at an invalid value, to ensure proper initialization

        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            //refrence to the expierence component
            if(experience != null )
            {
                experience.onExpierenceGained += UpdateLevel;
                //adds update level to the list of methods activated on the action
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            //checks to see if we have a new level
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                //set the new level as current
                LevelUpEffect();
                //spawn the level up effect
                OnLevelUp();
                //Update anything based on level up event
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
            //calls the GetStat() method from Progression and returns the float, this chain refrence prevents circular dependancies
        }

        public int GetLevel()
        {
            if(currentLevel < 1) { CalculateLevel(); }
            //if the current level isnt initialized, initialize it
            return currentLevel;
        }

        public int CalculateLevel() 
        {
            Experience experience = GetComponent<Experience>();
            //refrence to the expierence component
            if(experience == null) { return startingLevel; }
            //for enemies that dont level up

            float currentXP = experience.GetExperience();
            //gets the players current expierence
            int maxLevelPossible = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            //gets how many possible level there are
            for (int level = 1; level <= maxLevelPossible; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                    //gets the xp required to level up
                if (XPToLevelUp > currentXP)
                {
                    return level;
                    //returns the current level
                }
            }
            return maxLevelPossible + 1;
        }
    }
}
