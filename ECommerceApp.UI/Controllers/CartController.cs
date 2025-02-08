using ECommerce.Entities.Concrete;
using ECommerceApp.Business.Abstract;
using ECommerceApp.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ECommerceApp.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartSessionService _cartSessionService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartSessionService cartSessionService, ICartService cartService, IProductService productService)
        {
            _cartSessionService = cartSessionService;
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<IActionResult> AddToCart(int productId, int page, int category)
        {
            var productToBeAdded = await _productService.GetByIdAsync(productId);
            var cart = _cartSessionService.GetCart();

            _cartService.AddToCart(cart!, productToBeAdded);
            _cartSessionService.SetCart(cart!);

            TempData.Add("message", $"Your Product , {productToBeAdded.ProductName} was added successfully.");

            return RedirectToAction("Index", "Product", new { page = page, category = category });
        }

        public IActionResult List()
        {
            var cart = _cartSessionService.GetCart();
            var model = new CartListViewModel
            {
                Cart = cart!
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Complete()
        {
            var shippingDetailViewModel = new ShippingDetailsViewModel
            {
                ShippingDetails=new ShippingDetails()
            };
            return View(shippingDetailViewModel);   
        }

        [HttpPost]
        public IActionResult Complete(ShippingDetailsViewModel model)
        {
            if(!ModelState.IsValid) {
                return View(model);
            }
            TempData.Add("message", $"You {model.ShippingDetails.Firstname} your order is in progress.");
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Remove(int productId)
        {
            var productToBeRemoved = await _productService.GetByIdAsync(productId);

            var cart = _cartSessionService.GetCart();

            _cartService.RemoveFromCart(cart!, productId);

            _cartSessionService.SetCart(cart!);

            TempData.Add("message", $"Your Product , {productToBeRemoved.ProductName} was Deleted successfully.");

            return RedirectToAction("List", "Cart");
        }

        public IActionResult Increase(int productId, int unitsInStock, int quantitiy, string productName)
        {
            if (unitsInStock > quantitiy)
            {
                var cart = _cartSessionService.GetCart();

                _cartService.IncreaseProductQuantitiy(cart!, productId);

                _cartSessionService.SetCart(cart!);
            }
            else 
            {
                TempData.Add("messageAlert", $"There are not that many {productName} in stock.");
            }

            return RedirectToAction("List", "Cart");
        }

        //Have Delete Functionalitiy From DecreaseProductQuantitiy
        public IActionResult Decrease(int productId)
        {
            var cart = _cartSessionService.GetCart();

            _cartService.DecreaseProductQuantitiy(cart!, productId);

            _cartSessionService.SetCart(cart!);

            return RedirectToAction("List", "Cart");
        }
    }
}
