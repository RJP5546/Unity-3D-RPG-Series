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
    }
}
