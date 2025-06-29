namespace Foundation;

public interface IReducer<TState, TAction>
{
    TState Apply(TState state, TAction action);
}
