using Microsoft.Extensions.Logging;
using SampleApp.Core.Interface;
using SampleApp.Cli.Models;

namespace SampleApp.Cli.Presenters
{
    /// <summary>
    /// プレセンター
    /// </summary>
    public class HelloPresenter : IHelloPresenter
    {
        private readonly ILogger<HelloPresenter> _logger;
        private readonly HelloViewModel _viewModel;

        /// <summary>
        /// コンストラクタ
        /// ビューモデルをDIしてもらう
        /// </summary>
        /// <param name="viewModel"></param>
        public HelloPresenter(
            ILogger<HelloPresenter> logger,
            HelloViewModel viewModel)
        {
            _logger = logger;
            _viewModel = viewModel;
        }

        public void Complete(HelloCompleteOutput useCaseResult)
        {
            _logger.LogInformation("Recivied UseCase completed.");
            _logger.LogInformation("Changing ViewModel property.");
            // ビューモデルのプロパティを変更すると、
            // NotifyPropertyChangedイベントを通じて
            // Viewに変更が通知される
            _viewModel.Message = useCaseResult.Message;
        }
    }

}