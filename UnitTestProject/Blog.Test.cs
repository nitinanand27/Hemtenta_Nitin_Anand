using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HemtentaTdd2017.blog;
using HemtentaTdd2017;
using Moq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        IBlog iblog;
        Mock<IAuthenticator> mauth;
        User user = new User("username");
        Page page = new Page() { Title = "Title", Content = "Content" };

        public UnitTest1()
        {
            mauth = new Mock<IAuthenticator>();
            mauth.Setup(x => x.GetUserFromDatabase("username")).Returns(new User("username"));
            iblog = new Blog(mauth.Object);
        }

        [TestMethod]
        public void LoginUser_Test()
        {
            iblog.LoginUser(user);
            mauth.Verify(u => u.GetUserFromDatabase("username"), Times.Exactly(1));
            Assert.AreEqual(true, iblog.UserIsLoggedIn, "Exception");
        }

        [TestMethod]
        public void Logout_Test()
        {
            iblog.LogoutUser(user);
            Assert.AreEqual(false, iblog.UserIsLoggedIn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogoutUser_ThrowException_InvalidUser_Test()
        {
            user = new User(""); //set an invalid user
            iblog.LogoutUser(user);

            bool expected = true;
            bool actual = iblog.UserIsLoggedIn;

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void PublishPage_ValidPage_Test()
        {
            //we have declared a valid page in this class, lets test it
            iblog.LoginUser(user);
            mauth.Verify(u => u.GetUserFromDatabase("username"), Times.Exactly(1));

            bool expected = true;
            bool actual = iblog.PublishPage(page);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PublishPage_UserLoginStatus_Test()
        {
            //here we set user not logged in
            iblog.LogoutUser(user);

            bool expected = false; //since user is not logged in
            bool actual = iblog.PublishPage(page);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PublishPage_InvalidPage_GivesException_Test()
        {
            //setting an invalid page with no title
            Page page = new Page() { Title = "", Content = "content" };
            iblog.LoginUser(user);
            mauth.Verify(u => u.GetUserFromDatabase("username"), Times.Exactly(1));

            bool expected = false;
            bool actual = iblog.PublishPage(page);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SendEmail_InvalidParameter_Test()
        {
            string address = ""; //invalid
            string caption = "n";
            string body = "n";
            iblog.LoginUser(user);
            mauth.Verify(u => u.GetUserFromDatabase("username"), Times.Exactly(1));

            int expected = 0; //does not send mail because address is empty
            int actual = iblog.SendEmail(address, caption, body);

            Assert.AreEqual(expected, actual, "Address is empty");
        }

        [TestMethod]
        public void SendMail_UserLoggedOut_Test()
        {
            iblog.LogoutUser(user);
            string address = "n";
            string caption = "n";
            string body = "n";

            int expected = 0;
            int actual = iblog.SendEmail(address, caption, body);

            Assert.AreEqual(expected, actual, "User not logged in");
        }        
    }
}
