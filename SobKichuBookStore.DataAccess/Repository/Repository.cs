using Microsoft.EntityFrameworkCore;
using SobKichuBookStore.DataAccess.Data;
using SobKichuBookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SobKichuBookStore.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbset = _db.Set<T>();
        }

        public void Add(T item)
        {
            dbset.Add(item);
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {

            IQueryable<T> query = dbset;
            if(includeProperties!=null)
            {
                query = query.Include(includeProperties);
            }
           
            return query;
        }

        public T GetFisrtOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbset.Where(filter);
            if (includeProperties != null)
            {
                query = query.Include(includeProperties);
            }
            return query.FirstOrDefault();
           
        }

        public void Remove(T item)
        {
            dbset.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
           dbset.RemoveRange(items);
        }
    }
}
