using System;
using NUnit.Framework;
using HemtentaTdd2017.webshop;
using HemtentaTdd2017;
using Moq;
using static HemtentaTdd2017.Bank;
using NUnit.Framework.Constraints;

namespace UnitTestProject
{
    /*Vilka metoder och properties behöver testas?
    1. BASKET CLASS 
    AddProduct(), RemoveProduct(), TotalCost needs to be tested
    2. WEBSHOP ClASS
    Checkout() needs to be tested

    Ska några exceptions kastas?
    Yes, since i have control product and quantity is one statement
    I chose to throw ILLEGALINPUT exception incase any of the input
    entered is invalid.
    Whereas I chose to import INSUFFICIENTFUNDS exception incase user's
    shopping cost exceeds balance amount in billing.


    Vilka är domänerna för IWebshop och IBasket?
    IWEBSHOP
    In IWEBSHOP's Checkout function billing has domain of type IBilling or null

    IBASKET
    In IBASKET's AddProduct(Product p, int amount) & RemoveProduct(Product p, int amount)
    amount has domain integer between -2147483648 och +2147483647 &
    p has a domain of type Product or null
    TotalCost property has a domain of a positive number. */

    [TestFixture]
    public class UnitTest3
    {
        [TestCase]
        public void AddProduct_ValidProduct_Test()
        {
            IBasket basket = new Basket();
            Product product = new Product { Price = 100}; //valid product
            int quantity = 2; //valid quantity
            basket.AddProduct(product, quantity);

            decimal actual = basket.TotalCost;
            decimal expected = 200;

            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void AddProduct_InvalidInput_Exception_Test()
        {
            IBasket basket = new Basket();
            Product p = new Product() { Price = 100 };
            int quantity = -5; //setting invalid quantity

            Assert.Throws<IllegalInputException>(() => basket.AddProduct(p, quantity));
        }

        [TestCase]
        public void RemoveProduct_Partly_Test()
        {
            IBasket basket = new Basket();
            Product p = new Product() { Price = 100 };
            int quantity = 5;

            basket.AddProduct(p, quantity);
            basket.RemoveProduct(p, quantity - 1);

            decimal expected = 100;
            decimal actual = basket.TotalCost;

            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void RemoveProduct_ValidInputs_Test()
        {
            IBasket basket = new Basket();
            Product p = new Product() {Price = 100 };
            //int quantity = 5;

            decimal actual = basket.TotalCost;
            decimal expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void Checkout_InSufficientFunds_Test()
        {
            IWebshop webshop = new Webshop();
            IBasket basket = new Basket();
            Product p = new Product() { Price = 100 };
            int qty = 5;

            basket.AddProduct(p, qty);
            Mock<IBilling> mb = new Mock<IBilling>();
            mb.Setup(x => x.Balance).Returns(100); //setting less balance than totalcost

        }

    }
}
