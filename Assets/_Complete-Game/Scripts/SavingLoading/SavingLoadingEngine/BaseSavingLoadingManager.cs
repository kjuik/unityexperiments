using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
public abstract class BaseSavingLoadingManager<T> : MonoBehaviour where T : GameState
{
    protected virtual string SavePath => Application.persistentDataPath + "/QuickSave.sav";

    BinaryFormatter binaryFormatter;

    public BaseSavingLoadingManager() => InitFormatter();

    void InitFormatter()
    {
        binaryFormatter = new BinaryFormatter();
        var ss = new SurrogateSelector();

        //Needed, because Vector3 and Quaternion aren't serializable by default.
        ss.AddSurrogate(typeof(Vector3),
                        new StreamingContext(StreamingContextStates.All),
                        new Vector3SerializationSurrogate());

        ss.AddSurrogate(typeof(Quaternion),
                        new StreamingContext(StreamingContextStates.All),
                        new QuaternionSerializationSurrogate());

        binaryFormatter.SurrogateSelector = ss;
    }

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
            binaryFormatter.Serialize(stream, save);
        }
    }
    
    T ReadFromFile()
    {
        using (var stream = File.Open(SavePath, FileMode.Open, FileAccess.Read))
        {
            return binaryFormatter.Deserialize(stream) as T;
        }
    }
}