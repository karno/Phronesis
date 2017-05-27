using System.Threading.Tasks;

namespace Phronesis.Messaging
{
    public class ResponsiveMessageBase<T> : MessageBase
    {
        public TaskCompletionSource<T> CompletionSource { get; } = new TaskCompletionSource<T>();

        protected ResponsiveMessageBase(string key = null)
            : base(key)
        {
        }
    }
}