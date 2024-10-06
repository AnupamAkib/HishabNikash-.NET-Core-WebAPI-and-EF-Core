using Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext repositoryContext;
        private readonly Lazy<IUserRepository> userRepository;
        private readonly Lazy<IHishabRepository> hishabRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            userRepository = new Lazy<IUserRepository>(() => new  UserRepository(repositoryContext));
            hishabRepository = new Lazy<IHishabRepository>(() => new HishabRepository(repositoryContext));
        }

        public IUserRepository User => userRepository.Value;
        public IHishabRepository Hishab => hishabRepository.Value;

        public void Save()
        {
            repositoryContext.SaveChanges();
        }
    }
}
