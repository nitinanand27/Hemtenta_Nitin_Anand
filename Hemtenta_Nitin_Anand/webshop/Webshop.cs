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
        IBasket ibasket;

        public Webshop(IBasket basket)
        {
            ibasket = basket;
        }
        public IBasket Basket
        {
            get
            {
                return ibasket;
            }
        }
        public void Checkout(IBilling billing)
        {
            if (ibasket.TotalCost > billing.Balance)
            {
                throw new LowFundsException("Insufficient funds. Remove items from basket");
            }

            if(Basket.TotalCost == 0)
            {
                throw new NullBasketException("Oops, Basket is empty");
            }

            decimal cost = ibasket.TotalCost;
            billing.Pay(cost);
        }

        

      
    }
}
