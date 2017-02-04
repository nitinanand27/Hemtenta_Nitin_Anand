using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HemtentaTdd2017.Bank;

namespace HemtentaTdd2017.webshop
{
    public class Basket : IBasket
    {
        //List<Product> productsInBasket;

        /// <summary>
        /// Cart item is the general model in a webshop representing
        /// an item in a list of selected products in a shopping cart.
        /// I have created this class with necessary properties.
        /// </summary>
        
        List<CartItem> cartItems;

        public Basket()
        {
            cartItems = new List<CartItem>();
        }
        public decimal TotalCost
        {
            get
            {
                return cartItems.Sum(x => x.Product.Price * x.Quantity);
            }
        }

        public void AddProduct(Product p, int amount)
        {
            if (p != null && amount > 0)
            {
                cartItems.Add(new CartItem
                {
                    Product = p,
                    Quantity = amount
                });
            }
            else if (p == null || amount < 0)        
            {
                throw new IllegalInputException("Invalid input");
            }
        }

        public void RemoveProduct(Product p, int amount)
        {
            if (p != null && amount > 0)
            {
                //extract cart item to remove
                var itemToRemove = cartItems.Where(x => x.Product.Price == p.Price).First();

                //checks if extracted item's count in cart is more than desired to remove
                if (itemToRemove.Quantity > amount)
                {
                    itemToRemove.Quantity -= amount;
                }
                else
                {
                    cartItems.Remove(itemToRemove);
                }
            }

            else
            {
                throw new IllegalInputException("Invalid input");
            }

            
        }
    }
}
