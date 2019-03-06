using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace CompleteProject
{
    public class SavingLoadingManager : MonoBehaviour
    {
        string SavePath => Application.persistentDataPath + "/QuickSave.sav";

        public void SaveGame()
        {
            var save = new GameSave(); 
            WriteToFile(save);
        }

        public void LoadGame()
        {
            var save = ReadFromFile();
            Debug.Log(save.x);
        }

        void WriteToFile(GameSave save)
        {
            using (var stream = File.Open(SavePath, FileMode.Create, FileAccess.Write))
            {
                new XmlSerializer(typeof(GameSave)).Serialize(stream, save);
            }
        }

        GameSave ReadFromFile()
        {
            using (var stream = File.Open(SavePath, FileMode.Open, FileAccess.Read))
            {
                return new XmlSerializer(typeof(GameSave)).Deserialize(stream) as GameSave;
            }
        }

        [ContextMenu("TestSaveLoad")]
        public void TestSaveLoad()
        {
            SaveGame();
            LoadGame();
        }
    }
}