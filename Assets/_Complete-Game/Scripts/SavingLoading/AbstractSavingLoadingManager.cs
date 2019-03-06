using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public abstract class AbstractSavingLoadingManager<T> : MonoBehaviour where T: GameSave
{
    string SavePath => Application.persistentDataPath + "/QuickSave.sav";

    public void SaveGame() => WriteToFile(CreateGameSave());

    public void LoadGame() => ApplyGameSave(ReadFromFile());

    protected abstract T CreateGameSave();
    protected abstract void ApplyGameSave(T save);

    void WriteToFile(T save)
    {
        using (var stream = File.Open(SavePath, FileMode.Create, FileAccess.Write))
        {
            new XmlSerializer(typeof(T)).Serialize(stream, save);
        }
    }
    
    T ReadFromFile()
    {
        using (var stream = File.Open(SavePath, FileMode.Open, FileAccess.Read))
        {
            return new XmlSerializer(typeof(T)).Deserialize(stream) as T;
        }
    }

    
    
}