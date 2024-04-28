namespace GuitarShop.Models.ViewModels
{
    public class ProductListViewModel
    {
       
        public IEnumerable<Product> Products { get; set; }
        public string SelectedCategory { get; set; }
        
    }
}
