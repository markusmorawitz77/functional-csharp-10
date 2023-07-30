using Models.Types.Common;

namespace Application.Persistence;

public interface IReadOnlyRepository<T>
{
    IEnumerable<T> GetAll();
    Option<T> TryFind(Guid id);
}