namespace Foundation;

sealed class LoggingObserver : ICommandObserver
{
    public void AfterInvokeCommand(object command)
        => Console.WriteLine($"After {command}");

    public void BeforeInvokeCommand(object command)
        => Console.WriteLine($"Before {command}");

    public bool CanInvokeCommand(object command)
    {
        Console.WriteLine($"Evaluation of {command} returns {true}");
        return true;
    }
}
