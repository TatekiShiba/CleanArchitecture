using System.Threading.Tasks;
using SampleApp.Core.Entities;
using SampleApp.Core.Interface;

namespace SampleApp.Cli
{
    /// <summary>
    /// リポジトリのスタブ実装
    /// </summary>
    public class StubUserRepository : IUserRepository
    {
        public async Task<User> FindByUserId(string userId)
        {
            return await Task<User>.Run(() =>
            {
                User user = null;
                if (userId == "A")
                {
                    user = new User()
                    {
                        UserId = userId,
                        UserName = "Tateki"
                    };
                }

                return user;
            });
        }

        public Task<User> Add(User user)
        {
            return Task<User>.Run(() => new User());
        }
        public Task Save()
        {
            return Task.Run(() => { });
        }
    }
}