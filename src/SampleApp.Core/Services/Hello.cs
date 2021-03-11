using System.Threading.Tasks;
using SampleApp.Core.Interface;

namespace SampleApp.Core.Services
{
    /// <summary>
    /// Hello
    /// Use Case Interactor
    /// </summary>
    public class Hello : IHello
    {
        private readonly IUserRepository _userRepository;
        private readonly IHelloPresenter _presenter;

        public Hello(
            IUserRepository userRepository,
            IHelloPresenter presenter)
        {
            _userRepository = userRepository;
            _presenter = presenter;
        }

        public async Task Execute(HelloInput input)
        {
            var output = new HelloCompleteOutput();

            var user = await _userRepository.FindByUserId(input.UserId);
            if (user == null)
            {
                output.Message = "Who are you?";
            }
            else
            {
                output.Message = $"Hello {user.UserName}!!";
            }

            _presenter.Complete(output);
        }
    }
}