namespace container_manager;

public class OverfillException : Exception
{
    public OverfillException() : base() { }
    public OverfillException(string message) : base(message) { }
    public OverfillException(string message, Exception inner) : base(message, inner) { }
}