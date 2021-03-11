using System.Threading.Tasks;
using SampleApp.Core.Entities;

namespace SampleApp.Core.Interface
{
    public interface IUserRepository
    {
        Task<User> FindByUserId(string userId);
        Task<User> Add(User user);
        Task Save();
    }
}