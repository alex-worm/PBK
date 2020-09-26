namespace PBK.Logic.Serializers
{
    public interface ISerializer<T>
    {
        void Serialize(T entity);

        T Deserialize(string fileName);
    }
}