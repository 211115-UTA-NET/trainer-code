namespace MyNamespace
{
    public abstract class BaseClass
    {   
        protected string _baseString = "Base";
        public abstract void AbstractMethod();
        // public abstract string BaseString { get; }
        public virtual string BaseString
        {
            get
            {
                return _baseString;
            }
        }
    }
}