namespace Models.Types.Common;

public static class OptionExtensions
{
    public static Option<TResult> Map<T,TResult>(this Option<T> obj, Func<T, TResult> map) =>
        throw new NotImplementedException();

    public static Option<T> Filter<T>(this Option<T> obj, Func<T, bool> predicate) =>
        throw new NotImplementedException();

    public static T Reduce<T>(this Option<T> obj, T substitute) =>
        throw new NotImplementedException();

    public static T Reduce<T>(this Option<T> obj, Func<T> substitute) =>
        throw new NotImplementedException();
}