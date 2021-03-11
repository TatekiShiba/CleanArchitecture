using System.Threading.Tasks;

namespace SampleApp.Core.Interface
{
    /// <summary>
    /// HelloInput
    /// Input Data
    /// </summary>
    public class HelloInput
    {
        public string UserId { get; set; }
    }

    /// <summary>
    /// IHello
    /// Input Boundary
    /// </summary>
    public interface IHello
    {
        Task Execute(HelloInput input);
    }

    /// <summary>
    /// HelloCompleteOutput
    /// Output Data
    /// </summary>
    public class HelloCompleteOutput
    {
        public string Message { get; set; }
    }

    /// <summary>
    /// IHelloWorldPresenter
    /// Output Boundary
    /// </summary>
    public interface IHelloPresenter
    {
        void Complete(HelloCompleteOutput completeOutput);
    }
}