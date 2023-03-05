using SobKichuBookStore.DataAccess.Data;
using SobKichuBookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SobKichuBookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CategoryRepo = new CategoryRepository(_db);
            ProductRepo= new ProductRepository(_db);
        }

        public ICategoryRepository CategoryRepo { get; set; }

        public IProductRepository ProductRepo { get; set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
