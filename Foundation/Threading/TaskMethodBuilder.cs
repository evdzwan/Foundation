using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Foundation.Threading;

[StructLayout(LayoutKind.Auto), EditorBrowsable(EditorBrowsableState.Never)]
public struct TaskMethodBuilder(AsyncTaskMethodBuilder builder)
{
    public ITask Task => builder.Task.AsITask();

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine => builder.AwaitOnCompleted(ref awaiter, ref stateMachine);
    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine => builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);

    public void SetException(Exception ex) => builder.SetException(ex);
    public void SetStateMachine(IAsyncStateMachine stateMachine) => builder.SetStateMachine(stateMachine);

    public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine => builder.Start(ref stateMachine);
    public static TaskMethodBuilder Create() => new(AsyncTaskMethodBuilder.Create());
}
