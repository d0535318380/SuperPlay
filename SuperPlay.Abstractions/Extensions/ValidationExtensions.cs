namespace SuperPlay.Abstractions.Extensions;

public static class ValidationExtensions
{
    public  static T ThrowIfNull<T>(this T context, string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(context, parameterName);
        return context;
    }
}