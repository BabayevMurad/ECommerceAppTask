using ECommerce.Entities.Concrete;
using ECommerce.Entities.Models;
using ECommerceApp.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Business.Concrete
{
    public class CartService : ICartService
    {
        public void AddToCart(Cart cart, Product product)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == product.ProductId);
            if (cartLine != null)
            {
                cartLine.Quantity++;
            }
            else
            {
                cart.CartLines.Add(new CartLine { Product = product, Quantity = 1 });
            }
        }

        public List<CartLine>? List(Cart cart)
        {
           return cart.CartLines;
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            var cartLine = cart.CartLines.FirstOrDefault(c => c.Product!.ProductId == productId);

            if (cartLine != null)
            {
                cart.CartLines.Remove(cartLine);
            }

        }

        public void IncreaseProductQuantitiy(Cart cart, int productId)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product!.ProductId == productId)!;

            if (cartLine != null)
            {
                cartLine.Quantity++;
            }
        }

        //Have Delete Fuctionality
        public void DecreaseProductQuantitiy(Cart cart, int productId)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product!.ProductId == productId)!;

            if (cartLine != null)
            {
                if (cartLine.Quantity > 1)
                {
                    cartLine.Quantity--;
                }
                else 
                {
                    cart.CartLines.Remove(cartLine);
                }
            }
        }
    }
}
