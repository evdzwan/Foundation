namespace Foundation;

public interface IState
{
    object Model { get; }
    Type ModelType { get; }

    internal void SetModel(object model);
}
