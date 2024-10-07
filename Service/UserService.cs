using Contracts;
using Service.Contracts;

namespace Service
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager repositoryManager;

        public UserService(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }
    }
}
