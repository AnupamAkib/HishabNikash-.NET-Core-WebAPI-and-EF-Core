using Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> userService;
        private readonly Lazy<IHishabService> hishabService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            userService = new Lazy<IUserService>(() => new UserService(repositoryManager));
            hishabService = new Lazy<IHishabService>(() => new HishabService(repositoryManager));
        }

        public IUserService UserService => userService.Value;
        public IHishabService HishabService => hishabService.Value;
    }
}
