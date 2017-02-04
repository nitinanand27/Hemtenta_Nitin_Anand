using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HemtentaTdd2017.Bank;

namespace HemtentaTdd2017.bank
{
    public class Account : IAccount
    {
        double balance;

        public double Amount
        {
            get
            {
                return balance;
            }
        }

        public void Deposit(double amount)
        {
            if (Double.IsNaN(amount) || amount <= 0 || Double.IsInfinity(amount))
            {
                throw new IllegalAmountException("Invalid Amount");
            }
            balance += amount;            
        }

        public void TransferFunds(IAccount destination, double amount)
        {
            if (destination == null || Double.IsNaN(amount) || Double.IsInfinity(amount) || amount < 0)
            {
                throw new OperationNotPermittedException("Not able to Transfer funds");
            }
            Withdraw(amount);
            destination.Deposit(amount);
            
        }

        public void Withdraw(double amount)
        {
            if (Double.IsNaN(amount) || amount <= 0 || Double.IsInfinity(amount))
            {
                throw new IllegalAmountException("Invalid amount");
            }
            else if (amount > balance)
            {
                throw new InsufficientFundsException("Insufficient amount in a/c");
            }
            balance -= amount;
        }
    }
}
