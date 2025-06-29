namespace Foundation;

public interface IEffect<TAction>
{
    Task Apply(TAction action, IDispatcher dispatcher);
}
