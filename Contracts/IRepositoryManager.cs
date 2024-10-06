namespace Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User {  get; }
        IHishabRepository Hishab { get; }
        void Save();
    }
}
