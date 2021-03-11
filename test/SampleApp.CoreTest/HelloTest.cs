using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using SampleApp.Core.Entities;
using SampleApp.Core.Interface;
using SampleApp.Core.Services;

namespace SampleApp.CoreTest
{
    public class StubRepository : IUserRepository
    {
        private List<User> _users = new List<User>();

        public StubRepository()
        {
            _users.Add(new User { UserId = "A", UserName = "山田 太郎" });
        }
        public async Task<User> FindByUserId(string userId)
        {
            return await Task<User>.Run(() => _users.SingleOrDefault(u => u.UserId == userId));
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

    public class MockPresenter : IHelloPresenter
    {
        public HelloCompleteOutput HelloCompleteOutput { get; set; }
        public void Complete(HelloCompleteOutput completeOutput)
        {
            this.HelloCompleteOutput = completeOutput;
        }
    }

    public class HelloTest
    {
        [Fact]
        public async Task Execute_不明ユーザー_失敗メッセージ()
        {
            // arrange
            var stubUserRepository = new StubRepository();
            var mockHelloPresenter = new MockPresenter();
            var target = new Hello(stubUserRepository, mockHelloPresenter);

            // act
            await target.Execute(new HelloInput { UserId = "B" });
            var actual = mockHelloPresenter.HelloCompleteOutput.Message;

            // assert
            Assert.Equal("Who are you?", actual);
        }

        [Fact]
        public async Task Execute_不明ユーザー_成功メッセージ()
        {
            // arrange
            var stubUserRepository = new StubRepository();
            var mockHelloPresenter = new MockPresenter();
            var target = new Hello(stubUserRepository, mockHelloPresenter);

            // act
            await target.Execute(new HelloInput { UserId = "A" });
            var actual = mockHelloPresenter.HelloCompleteOutput.Message;

            // assert
            Assert.Equal("Hello 山田 太郎!!", actual);
        }
    }
}
