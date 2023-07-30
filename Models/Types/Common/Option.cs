namespace Models.Types.Common;

public abstract class Option<T>
{

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