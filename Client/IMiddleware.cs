namespace Foundation;

public interface IMiddleware
{
    void AfterDispatch(object action);
    void BeforeDispatch(object action);
    bool CanDispatch(object action);
}
