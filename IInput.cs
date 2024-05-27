public interface IInput<T>
{
    bool HasData();
    T Read();
}