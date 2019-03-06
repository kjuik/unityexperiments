public interface ISaveable<T> where T: ObjectState
{
    T Save();
    void Load(T save);
}
