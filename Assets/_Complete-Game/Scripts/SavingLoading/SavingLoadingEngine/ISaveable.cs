/// <summary>
/// Interface implemented by each class in your game, that can save and load its state.
/// </summary>
/// <typeparam name="T">
/// Subclass of ObjectState, that specifies the data saved and loaded as state of your object.
/// </typeparam>
public interface ISaveable<T> where T: ObjectState
{
    /// <summary>
    /// Implement this method, to specify how your object saves its state.
    /// Call it in your SavingLoadingManager.GenerateGameState method.
    /// </summary>
    /// <returns>Current state of your object</returns>
    T Save();

    /// <summary>
    /// Implement this method, to specify how your object loads its state.
    /// Call it in your SavingLoadingManager.ApplyGameState method.
    /// </summary>
    /// <returns>State of the object, that you want to recreate</returns>
    void Load(T save);
}
