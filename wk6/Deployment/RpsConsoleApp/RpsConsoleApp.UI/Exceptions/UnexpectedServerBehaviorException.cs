using System.Runtime.Serialization;

namespace RpsConsoleApp.UI.Exceptions
{
    [Serializable]
    internal class UnexpectedServerBehaviorException : Exception
    {
        public UnexpectedServerBehaviorException()
        {
        }

        public UnexpectedServerBehaviorException(string? message) : base(message)
        {
        }

        public UnexpectedServerBehaviorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnexpectedServerBehaviorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
