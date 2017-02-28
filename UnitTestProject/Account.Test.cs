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
        Account account;
        double expected;
        double actual;

        public UnitTest2()
        {
            account = new Account();
        }

        [TestMethod]
        public void Deposit_ValidAmount_Test()
        {
            account.Deposit(100);
            Assert.AreEqual(100, account.Amount);
        }

        [TestMethod]
        public void Withdraw_Test()
        {
            account.Deposit(200);
            account.Withdraw(200);
            expected = 0;
            actual = account.Amount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void Withdraw_InsuffiecientFunds_Test()
        {
            account.Deposit(100);
            account.Withdraw(200);
            expected = 0;
            actual = account.Amount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TransferFunds_Test()
        {
            Account destinationAccount = new Account();

            destinationAccount.Deposit(500);            
            destinationAccount.TransferFunds(account, 100);
            expected = 400;
            actual = destinationAccount.Amount;
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalAmountException))]
        public void Deposit_ILLegalAmount_Test()
        {
            //setting illegal amount to check ILLegalAmountException
            account.Deposit(-10); 
            Assert.AreEqual(100, account.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(OperationNotPermittedException))]
        public void Transfer_InvalidAccount_Test()
        {
            //sending legal amount to invalid account to check 
            //OperationNotPermittedException
            account.Deposit(100);
            account.TransferFunds(null, 100);
        }

    }
}
