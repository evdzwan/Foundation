namespace Foundation;

public interface ICommandDispatcher
{
    void Dispatch(object command);
}
