namespace ExpoAPI.Infrastructure.Adapters
{
    public interface IHashingAdapter
    {
        string HashPassword(string username);
    }
}
