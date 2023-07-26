namespace Application.Persistence;

public interface IReadOnlyRepository<T>
{
    IEnumerable<T> GetAll();
}
