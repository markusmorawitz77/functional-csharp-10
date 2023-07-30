namespace Models.Types.Common;

public static class OptionExtensions
{
    public static Option<TResult> Map<T,TResult>(this Option<T> obj, Func<T, TResult> map) =>
        obj is Some<T> some ? new Some<TResult>(map(some.Content)) : new None<TResult>();

    public static Option<T> Filter<T>(this Option<T> obj, Func<T, bool> predicate) =>
        obj is Some<T> some && !predicate(some.Content) ? new None<T>() : obj;

    public static T Reduce<T>(this Option<T> obj, T substitute) =>
        obj is Some<T> some ? some.Content : substitute;

    public static T Reduce<T>(this Option<T> obj, Func<T> substitute) =>
        obj is Some<T> some ? some.Content : substitute();
}