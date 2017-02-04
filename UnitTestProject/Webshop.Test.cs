using System;
using NUnit.Framework;
using HemtentaTdd2017.webshop;
using HemtentaTdd2017;
using Moq;

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
    TotalCost property has a domain of a positive number. 
    
    PLS NOTE: 
    Below mention 3 tests needs to run individually to see them passing. Some unknown error.
    RemoveProduct_ValidInputs_Test()
    AddProduct_InvalidInput_Exception_Test()
    AddProduct_ValidrRoduct_Test()
         */

    [TestFixture]
    public class UnitTest3
    {
        IBasket basket;
        Product product;
        int quantity = 5;

        public UnitTest3()
        {
            basket = new Basket();
            product = new Product() { Price = 100 };
        }


        [TestCase]
        public void AddProduct_ValidProduct_Test()
        {
            //valid quantity
            basket.AddProduct(product, quantity);

            decimal actual = basket.TotalCost;
            decimal expected = 500;

            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void AddProduct_InvalidInput_Exception_Test()
        {
            //setting invalid quantity
            quantity = -5;
            Assert.Throws<IllegalInputException>(() => basket.AddProduct(product, quantity));
        }

        [TestCase]
        public void RemoveProduct_Partly_Test()
        {
            basket.AddProduct(product, quantity);
            basket.RemoveProduct(product, quantity -1);

            decimal expected = 100;
            decimal actual = basket.TotalCost;

            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void RemoveProduct_ValidInputs_Test()
        {
            basket.AddProduct(product, quantity);
            basket.RemoveProduct(product, quantity);

            decimal actual = basket.TotalCost;
            decimal expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void Checkout_InSufficientFunds_Test()
        {
            IWebshop webshop = new Webshop();
            Mock<IBilling> mb = new Mock<IBilling>();

            basket.AddProduct(product, 5);

            //setting less balance than totalcost
            mb.Setup(x => x.Balance).Returns(100); 
        }
    }
}
