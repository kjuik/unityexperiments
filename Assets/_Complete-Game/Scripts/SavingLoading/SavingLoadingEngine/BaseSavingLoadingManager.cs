using System.IO;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Parent class of your game-specific SavingLoadingManagers.
/// Defines the low-level serialization of save files.
/// Exposes public SaveGame and LoadGame methods.
/// Inherit from this class, to specify game-specific saving logic.
/// </summary>
/// <typeparam name="T">
/// The subclass of GameState that holds your game-specific save data.
/// </typeparam>
public abstract class BaseSavingLoadingManager<T> : MonoBehaviour where T: GameState
{
    protected virtual string SavePath => Application.persistentDataPath + "/QuickSave.sav";

    public void SaveGame() => WriteToFile(GenerateGameState());
    public void LoadGame() => ApplyGameState(ReadFromFile());

    /// <summary>
    /// Override this method to define game-specific Game State saving.
    /// </summary>
    /// <returns>Object holding the current state of your game</returns>
    protected abstract T GenerateGameState();

    /// <summary>
    /// Override this method to define game-specific Game State loading.
    /// </summary>
    /// <param name="save">Object holding the game state you want to load</param>
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