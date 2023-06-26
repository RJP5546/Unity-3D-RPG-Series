using RPG.Stats;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
//adds a new interface option into the editor when you right click, can now create new progression scriptable object
public class Progression : ScriptableObject
{
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;
    //select the character class from the editor to mark what progression tree the object will follow

    public float GetHealth(CharacterClass characterClass, int level)
    {
        foreach (ProgressionCharacterClass progressionClass in characterClasses)
        {
            if (progressionClass.characterClass == characterClass)
            {
                //return progressionClass.health[level - 1];
            }
        }
        return 0;
    }

    [System.Serializable]
    //marks the ProgressionCharacterClass as serializable, and makes the classes appear in the editor
    class ProgressionCharacterClass
    {
        public CharacterClass characterClass;
        //select the character class
        public ProgressionStat[] stats;
        //shows the list of stats
        //public float[] health;
        //health array for health values as object levels up
    }

    [System.Serializable]
    class ProgressionStat
    {
        public Stat stat;
        //tells us what stat it is
        public float[] levels;
        //adds the level array for the stat
    }
}