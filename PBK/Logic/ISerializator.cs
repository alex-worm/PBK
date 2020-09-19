namespace PBK.Logic
{
    public interface ISerializator <T>
    {
        void Serialize(T entity);

        T Deserialize(string fileName);
    }
}
