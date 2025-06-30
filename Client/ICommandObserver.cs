namespace Foundation;

public interface ICommandObserver
{
    void AfterInvokeCommand(object command);
    void BeforeInvokeCommand(object command);
    bool CanInvokeCommand(object command);
}
