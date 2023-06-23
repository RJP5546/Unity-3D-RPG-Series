using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class JsonSavingSystem : MonoBehaviour
    {
        private const string extension = ".json";
        //a constant set so that the changing of any file extension only happens in one place

        /// <summary>
        /// Will load the last scene that was saved and restore the state. This
        /// must be run as a coroutine.
        /// </summary>
        /// <param name="saveFile">The save file to consult for loading.</param>
        public IEnumerator LoadLastScene(string saveFile)
        {
            JObject state = LoadJsonFromFile(saveFile);
            IDictionary<string, JToken> stateDict = state;
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (stateDict.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int)stateDict["lastSceneBuildIndex"];
            }
            yield return SceneManager.LoadSceneAsync(buildIndex);
            RestoreFromToken(state);
        }

        /// <summary>
        /// Save the current scene to the provided save file.
        /// </summary>
        public void Save(string saveFile)
        {
            JObject state = LoadJsonFromFile(saveFile);
            //loads the JObject from the save file
            CaptureAsToken(state);
            SaveFileAsJSon(saveFile, state);
        }

        /// <summary>
        /// Delete the state in the given save file.
        /// </summary>
        public void Delete(string saveFile)
        {
            print("Deleting " + saveFile);
            File.Delete(GetPathFromSaveFile(saveFile));
        }

        public void Load(string saveFile)
        {
            RestoreFromToken(LoadJsonFromFile(saveFile));
        }

        public IEnumerable<string> ListSaves()
        {
            foreach (string path in Directory.EnumerateFiles(Application.persistentDataPath))
            {
                if (Path.GetExtension(path) == extension)
                {
                    yield return Path.GetFileNameWithoutExtension(path);
                }
            }
        }

        // PRIVATE

        private JObject LoadJsonFromFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            //gets a refrence to the file save path
            if (!File.Exists(path))
                //checks if the file path exists or not. If not, makes a new empty JObject
            {
                return new JObject();
            }

            using (var textReader = File.OpenText(path))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    reader.FloatParseHandling = FloatParseHandling.Double;

                    return JObject.Load(reader);
                }
            }

        }

        private void SaveFileAsJSon(string saveFile, JObject state)
        {
            string path = GetPathFromSaveFile(saveFile);
            //gets a refrence to the file save path
            print("Saving to " + path);
            using (var textWriter = File.CreateText(path))
                //once done with the using statement, it automatically closes the file stream. Doesn allow access to
                //data inside the using brackets to anything outside. Prevents leaks
            {
                using (var writer = new JsonTextWriter(textWriter))
                {
                    writer.Formatting = Formatting.Indented;
                    state.WriteTo(writer);
                    //sends the state Jobject to the file writer
                }
            }
        }


        private void CaptureAsToken(JObject state)
        {
            IDictionary<string, JToken> stateDict = state;
            //maps a Dictionary<string, JToken> right onto the JObject.  Both state and stateDict point to the same object.
            foreach (JsonSaveableEntity saveable in FindObjectsOfType<JsonSaveableEntity>())
                //for each item that is marked as a JsonSavableEntity:
            {
                stateDict[saveable.GetUniqueIdentifier()] = saveable.CaptureAsJtoken();
                //assigns to the state dictionary the savable's unique identifier code, then calls CaptureAsJtoken() to save the
                //Jtokens as Jobjects, then assigning as the dictionary value.
            }

            stateDict["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
            //sets the active scene as the last scene
        }


        private void RestoreFromToken(JObject state)
        {
            IDictionary<string, JToken> stateDict = state;
            //maps a Dictionary<string, JToken> right onto the JObject.  Both state and stateDict point to the same object.
            foreach (JsonSaveableEntity saveable in FindObjectsOfType<JsonSaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                //gets the unique identifier for the savable object
                if (stateDict.ContainsKey(id))
                {
                    saveable.RestoreFromJToken(stateDict[id]);
                    //if the key is in the dictionary, restore the state value from that keys associated value
                }
            }
        }


        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + "save" + extension);
            //returns a file with the proper extension in the applications data path
            //Application.persistentDataPath takes the different data path from each platform
            //Path.Combine, then combines the savefile name and extension to it to give the file location
        }
    }
}
