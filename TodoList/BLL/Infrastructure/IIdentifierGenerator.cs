namespace BLL.Infrastructure
{
    public interface IIdentifierGenerator
    {
        int GenerateNextId(int previousId);
    }
}
