using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace SampleApp.Cli.Models
{
    /// <summary>
    /// ビューモデル
    /// INotifyPropertyChangedを実装することでViewに変更を通知する
    /// </summary>
    public class HelloViewModel : INotifyPropertyChanged
    {
        private readonly ILogger<HelloViewModel> _logger;
        public HelloViewModel(ILogger<HelloViewModel> logger)
        {
            _logger = logger;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            _logger.LogInformation($"Notify {propertyName} change.");
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _message = "";
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }
    }
}