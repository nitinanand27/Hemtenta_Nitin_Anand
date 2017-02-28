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
    Whereas I added LOWFUNDSEXCEPTION incase user's
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
    Below mention 4 tests needs to run individually to see them passing. Some unknown error.

    RemoveProduct_ValidInputs_Test()
    AddProduct_ValidProductQuantity_Test()
    Checkout_NullBasket_Exception_Test()
    RemoveProduct_Partly_Test()
         */

    [TestFixture]
    public class UnitTest3
    {
        Product product;
        IBasket basket;        
        int quantity = 5;
        IWebshop webshop;
        Mock<IBilling> mbill;

        public UnitTest3()
        {
            basket = new Basket();
            webshop = new Webshop(basket);
            mbill = new Mock<IBilling>();
            product = new Product() { Price = 100 };
        }

        //Testing Basket Class

        [TestCase]
        public void AddProduct_ValidProductQuantity_Test()
        {
            //valid quantity & valid product as declared above
            basket.AddProduct(product, quantity);

            decimal actual = basket.TotalCost;
            decimal expected = 500;

            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void AddProduct_InvalidProduct_ILLegalInputException_Test()
        {
            //adding invalid product
            Assert.Throws<IllegalInputException>(() => basket.AddProduct
            (null, quantity));
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
            basket.RemoveProduct(product, quantity -2);

            decimal expected = 200;
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
        public void Remove_InvalidProduct_Test()
        {
           Assert.Throws<IllegalInputException>(() => basket.RemoveProduct(null, quantity));
        }
        
        // Webshop tests

        [TestCase]
        public void Checkout_LowFundsException_Test()
        {
            basket.AddProduct(product, 10);
            //setting less balance than totalcost
            mbill.Setup(x => x.Balance).Returns(basket.TotalCost -1);

            Assert.Throws<LowFundsException>(() => webshop.Checkout(mbill.Object)); 
        }

        [TestCase]
        public void Checkout_Pay_Success_Test()
        {
            basket.AddProduct(product, 10);
            mbill.SetupGet(x => x.Balance).Returns(basket.TotalCost);
            webshop.Checkout(mbill.Object);

            mbill.Verify(x => x.Pay(basket.TotalCost), Times.Once);
        }

        [TestCase]
        public void Checkout_NullBasket_Exception_Test()
        {
            //making the basket empty before checkout
            webshop.Basket.AddProduct(product, 1);
            webshop.Basket.RemoveProduct(product, 1);

            Assert.Throws<NullBasketException>(() => webshop.Checkout(mbill.Object));
        }

    }
}
