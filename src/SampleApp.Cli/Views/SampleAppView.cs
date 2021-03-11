using System;
using Microsoft.Extensions.Logging;
using SampleApp.Cli.Models;

namespace SampleApp.Cli.Views
{
    /// <summary>
    /// ビュー
    /// </summary>
    public class SampleAppView
    {
        private readonly ILogger<SampleAppView> _logger;
        private readonly HelloViewModel _helloViewModel;

        public SampleAppView(
            ILogger<SampleAppView> logger,
            HelloViewModel helloViewModel)
        {
            _logger = logger;
            _helloViewModel = helloViewModel;

            _helloViewModel.PropertyChanged += (sender, e) =>
            {
                _logger.LogInformation($"Recived {e.PropertyName} changed.");
                // ビューモデルの変更を受けたときの動作
                if (e.PropertyName == "Message")
                {
                    Console.WriteLine(_helloViewModel.Message);
                }
            };
        }

        public event EventHandler<string> UserIdInput;

        public void Prompt()
        {
            Console.Write("Enter your UserID > ");
            var input = Console.ReadLine();
            if (input == ".q") Environment.Exit(0);
            UserIdInput?.Invoke(this, input);
        }
    }
}