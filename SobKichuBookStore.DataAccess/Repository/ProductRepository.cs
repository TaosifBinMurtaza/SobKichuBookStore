using SobKichuBookStore.DataAccess.Data;
using SobKichuBookStore.DataAccess.Repository.IRepository;
using SobKichuBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SobKichuBookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            var product = _db.Products.FirstOrDefault(u=>u.Id== obj.Id);
            if (product != null)
            {
                product.Name = obj.Name;
                product.Description = obj.Description;
                product.Price = obj.Price;
                product.ISBN= obj.ISBN;
                product.Author= obj.Author;
                product.ListPrice= obj.ListPrice;
                product.Price = obj.Price;
                product.Price50 = obj.Price50;
                product.CategoryId=obj.CategoryId;
                if(obj.ImageUrl!=null)
                {
                    product.ImageUrl = obj.ImageUrl;
                }


            }
        }
    }
}
