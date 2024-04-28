using GuitarShop.Data.DataAccess;
using GuitarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GuitarShop.Data
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
        public IEnumerable<Product> GetAllProductsWithCategoryDetails()
        {
            return _appDbContext.Products
                .Include(m => m.Category);
        }

        public IEnumerable<Product> GetAllProductsInCategory(string category)
        {
            Category cat = _appDbContext.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == category.ToLower());

            return _appDbContext.Products.Where(p => p.CategoryID == cat.CategoryID);
        }

       
    }
}
