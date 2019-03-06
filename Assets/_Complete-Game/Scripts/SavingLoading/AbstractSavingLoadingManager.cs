using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public abstract class AbstractSavingLoadingManager<T> : MonoBehaviour where T: GameState
{
    string SavePath => Application.persistentDataPath + "/QuickSave.sav";

    public void SaveGame() => WriteToFile(GenerateGameState());
    public void LoadGame() => ApplyGameState(ReadFromFile());

    protected abstract T GenerateGameState();
    protected abstract void ApplyGameState(T save);

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