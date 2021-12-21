
namespace NickUnit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InlineDataAttribute : Attribute
    {
        public object[] Parameters { get; }

        public InlineDataAttribute(params object[] parameters)
        {
            Parameters = parameters;
        }
    }
}
