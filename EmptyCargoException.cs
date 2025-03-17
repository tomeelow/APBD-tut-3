namespace container_manager;

public class EmptyCargoException : Exception
{
    public EmptyCargoException() : base() { }
    public EmptyCargoException(string message) : base(message) { }
    public EmptyCargoException(string message, Exception inner) : base(message, inner) { }
}