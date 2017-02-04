using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HemtentaTdd2017.Bank;

namespace HemtentaTdd2017.webshop
{
    public class Webshop : IWebshop
    {
        public IBasket Basket
        {
            get
            {
                return new Basket();
            }
        }

        public void Checkout(IBilling billing)
        {
            IBasket basket = new Basket();

            if (basket.TotalCost > billing.Balance)
            {
                throw new InsufficientFundsException("Insufficient funds. Remove items from basket");
            }

            decimal cost = basket.TotalCost;
            billing.Pay(cost);            
        }
    }
}
