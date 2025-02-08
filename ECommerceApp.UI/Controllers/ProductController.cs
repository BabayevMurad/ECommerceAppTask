using ECommerceApp.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page = 1, int category = 0, int priceOrder = -1, int nameOrder = 0)
        {
            var items = await _productService.GetAllByCategoryId(category);

            //Name Order
            if (nameOrder == 0)
                items = items.OrderBy(p => p.ProductName).ToList();
            else if (nameOrder == 1)
                items = items.OrderByDescending(p => p.ProductName).ToList();


            //Price Order
            if (priceOrder == 0)
                items = items.OrderBy(p => p.UnitPrice).ToList();
            else if (priceOrder == 1) 
                items = items.OrderByDescending(p => p.UnitPrice).ToList();


            var pageSize = 10;
            var model = new ProductListViewModel
            {
                Products=items.Skip((page-1)*pageSize).Take(pageSize).ToList(),
                CurrentPage=page,
                PageSize=pageSize,
                PageCount=(int)Math.Ceiling(items.Count/(decimal)pageSize),
                CurrentCategory=category,
                PriceOrder=priceOrder,
                NameOrder=nameOrder,
            };
            return View(model);
        }

        public IActionResult AddWarning(string productName)
        {
            TempData.Add("messageAlert", $"{productName} is out of stock.");

            return RedirectToAction("Index", "Product");
        }
    }
}
