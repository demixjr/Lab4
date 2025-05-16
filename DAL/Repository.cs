using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    namespace DAL
    {
        public class Repository<T> : IRepository<T> where T : class
        {
            private DbContext _context;
            private DbSet<T> _dbSet;

            public Repository(DbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public T Get(int id)
            {
                return _dbSet.Find(id);
            }
            public IEnumerable<T> GetAll()
            {
                return _dbSet.ToList();
            }
            public T Find(Expression<Func<T, bool>> predicate)
            {
                return _dbSet.FirstOrDefault(predicate);
            }
            public IEnumerable<T> FindAll(Func<T, bool> predicate)
            {
                return _dbSet.Where(predicate).ToList();
            }

            public void Add(T entity)
            {
                _dbSet.Add(entity);
            }

            public void Update(T entity)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            public void Remove(T entity)
            {
                _dbSet.Remove(entity);
            }

            public void SaveChanges()
            {
                _context.SaveChanges();
            }
        }
    }


}
