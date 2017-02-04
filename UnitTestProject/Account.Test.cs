using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HemtentaTdd2017.bank;
using Moq;
using static HemtentaTdd2017.Bank;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void Deposit_ValidAmount_Test()
        {
            Account account = new Account();
            account.Deposit(100);
            Assert.AreEqual(100, account.Amount);
        }

        [TestMethod]
        public void Withdraw_Test()
        {
            Account acc = new Account();
            acc.Deposit(200);
            acc.Withdraw(200);
            double expected = 0;
            double actual = acc.Amount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void Withdraw_InsuffiecientFunds_Test()
        {
            Account acc = new Account();
            acc.Deposit(100);
            acc.Withdraw(200);
            double expected = 0;
            double actual = acc.Amount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TransferFunds_Test()
        {
            IAccount account = new Account();
            IAccount destinationAccount = new Account();

            destinationAccount.Deposit(500);            
            destinationAccount.TransferFunds(account, 100);
            double expected = 400;
            double actual = destinationAccount.Amount;
            
            Assert.AreEqual(expected, actual);
        }
    }
}
