using Microsoft.EntityFrameworkCore;

namespace Ddd.Logic.Common
{
    public abstract class Repository<T> where T : AggregateRoot
    {
        public DbSet<T> DbSet { get; set; }
        public DddDbContext DddDbContext { get; set; }

        public Repository(DddDbContext dbContext)
        {
            DddDbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public T? GetById(long id)
        {
            return DbSet.Find(id);
        }

        public void Save()
        {
            DddDbContext.SaveChanges();
        }
    }
}
