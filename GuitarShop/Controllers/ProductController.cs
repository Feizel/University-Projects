using GuitarShop.Data;
using GuitarShop.Data.DataAccess;
using GuitarShop.Models;
using GuitarShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        public ProductController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public IActionResult List(string id = "all", string sortBy = "name")
        {
            IEnumerable<Product> products;
            Expression<Func<Product, Object>> orderBy;
            string orderByDirection;

            ViewData["NameSortParam"] = sortBy == "name" ? "name_desc" : "name";
            ViewData["PriceSortParam"] = sortBy == "price" ? "price_desc" : "price";

            if (sortBy.EndsWith("_desc"))
            {
                sortBy = sortBy.Substring(0, sortBy.Length - 5);
                orderByDirection = "desc";
            }
            else
            {

                orderByDirection = "asc";
            }

            //Route values are set to lower case and does not match actual property name
            //Do the following to convert the first letter to uppercase (name -> Name)
            string sPropertyName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sortBy);

            orderBy = p => EF.Property<object>(p, sPropertyName);  //e.g. p => p.Name

            if (id == "all")
            {
                products = _repo.Product.FindWithOptions(new QueryOptions<Product>
                {
                    OrderBy = orderBy,
                    OrderByDirection = orderByDirection
                });
            }
            else
            {
                Category cat = _repo.Category.FindByCondition(c => c.CategoryName.ToLower() == id.ToLower())
                               .FirstOrDefault();
                products = _repo.Product.FindWithOptions(new QueryOptions<Product>
                {
                    OrderBy = orderBy,
                    OrderByDirection = orderByDirection,
                    Where = p => p.CategoryID == cat.CategoryID
                }); ;
            }

            var model = new ProductListViewModel
            {
                Products = products,
                SelectedCategory = id
            };

            // bind products to view
            return View(model);
        }


        public IActionResult Details(int id)
        {
            Product product = _repo.Product.GetById(id);
            if (product != null)
            {
                // use ViewBag to pass data to view
                ViewBag.ImageFilename = product.Code + "_m.png";

                // bind product to view
                return View(product);
            }
            else
                return RedirectToAction("List");
        }

    }
}
