using RPG.Stats;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
//adds a new interface option into the editor when you right click, can now create new progression scriptable object
public class Progression : ScriptableObject
{
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;
    //select the character class from the editor to mark what progression tree the object will follow

    public float GetStat(Stat stat, CharacterClass characterClass, int level)
    {
        foreach (ProgressionCharacterClass progressionClass in characterClasses)
        {
            if (progressionClass.characterClass != characterClass) { continue; }
            //if the progression class is not what was passed in, continue
            foreach (ProgressionStat progressionStat in progressionClass.stats)
            {
                if (progressionStat.stat != stat) { continue; }
                //if the progressionStat is not the stat that was passed in, continue

                if (progressionStat.levels.Length < level) { continue; }
                //if the stat has less indexes than the current player level, ignore and continue. Prevents index out of range

                return progressionStat.levels[level - 1];
                //return the stat and its value at the current level's index
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