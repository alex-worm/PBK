namespace PBK.Logic
{
    public interface ISerializer<T>
    {
        void Serialize(T entity);

        T Deserialize(string fileName);
    }
}
