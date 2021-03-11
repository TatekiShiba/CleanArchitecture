using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SampleApp.Core.Interface;

namespace SampleApp.Cli.Controllers
{
    /// <summary>
    /// コントローラー
    /// </summary>
    public class HelloController
    {
        // ロガー
        private ILogger<HelloController> _logger;

        // Hello(ユースケース)
        private readonly IHello _hello;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="hello"></param>
        public HelloController(
            ILogger<HelloController> logger,
            IHello hello)
        {
            _logger = logger;
            _hello = hello;
        }

        /// <summary>
        /// アクションメソッド
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task Hello(string userId)
        {
            _logger.LogInformation($"userId={userId}");
            // 入力をユースケースのInput型に変換
            var helloInput = new HelloInput
            {
                UserId = userId
            };

            // ユースケースの実行
            _logger.LogInformation("Invoking hello UseCase.");
            await _hello.Execute(helloInput);
        }
    }

}