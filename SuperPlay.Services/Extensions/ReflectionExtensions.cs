using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Services.Extensions;

public static class ReflectionExtensions
{
    public static async Task<IBaseResponse> InvokeHandleAsync(
        this object context,
        Type handlerType,
        IBaseRequest request,
        CancellationToken token)
    {
        var method = handlerType.GetMethod("HandleAsync");
        var task = (Task)method.Invoke(context, new object[] { request, token });

        await task.ConfigureAwait(false);

        var resultProperty = task.GetType().GetProperty("Result");

        return (IBaseResponse)resultProperty.GetValue(task);
    }
}