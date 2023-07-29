namespace Application.Persistence;

public interface IReadOnlyRepository<T>
{
    IEnumerable<T> GetAll();
    T Find(Guid id);
}