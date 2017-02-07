namespace BLL.Infrastructure
{
    public class IdentifierGenerator : IIdentifierGenerator
    {
        public int GenerateNextId(int previousId)
        {
            return previousId++;
        }
    }
}
