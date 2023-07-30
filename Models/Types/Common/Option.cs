namespace Models.Types.Common;

public abstract class Option<T>
{
    public static implicit operator Option<T>(None _) => new None<T>();
    public static implicit operator Option<T>(T value) => new Some<T>(value);
}

public static class Option
{
    public static Option<T> Optional<T>(this T obj) => new Some<T>(obj);
}


public sealed class Some<T> : Option<T>
{
    public T Content { get; }
    public Some(T content) => this.Content = content;

    public override string ToString() => $"Some {this.Content?.ToString() ?? "<null>"}";
}

public sealed class None<T> : Option<T>
{
    public override string ToString() => "None";
}

public class None
{
    public static None Value { get; } = new();
    public static None<T> Of<T>() => new();
}