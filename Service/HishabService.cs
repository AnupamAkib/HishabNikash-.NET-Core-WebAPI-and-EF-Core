using Contracts;
using Service.Contracts;

namespace Service
{
    internal sealed class HishabService : IHishabService
    {
        private readonly IRepositoryManager repositoryManager;

        public HishabService(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }
    }
}
