
namespace Ddd.Logic.Common
{
    public interface IHandler<T> where T : IDomainEvent
    {
        void Handle(T entity);
    }
}
