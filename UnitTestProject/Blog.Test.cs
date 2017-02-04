using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HemtentaTdd2017.blog;
using HemtentaTdd2017;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        Blog blog;
        User user;

        public UnitTest1()
        {
            blog = new Blog();
            user = new User("some user");
        }

        [TestMethod]
        public void LoginUser_Test()
        {
            blog.LoginUser(user);
            Assert.AreEqual(true, blog.UserIsLoggedIn, "Exception");
        }

        [TestMethod]
        public void Logout_Test()
        {
            blog.LogoutUser(user);
            Assert.AreEqual(false, blog.UserIsLoggedIn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogoutUser_ThrowException_InvalidUser_Test()
        {
            user = new User(""); //set an invalid user
            blog.LogoutUser(user);

            bool expected = true;
            bool actual = blog.UserIsLoggedIn;

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void PublishPage_ValidPage_Test()
        {
            //here we set valid values to page properties making the page valid
            Page page = new Page() { Title = "Title", Content = "Content" };
            blog.UserIsLoggedIn = true;

            bool expected = true;
            bool actual = blog.PublishPage(page);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PublishPage_UserLoginStatus_Test()
        {
            Page page = new Page() { Title = "title", Content = "content" };
            //here we set user not logged in
            blog.UserIsLoggedIn = false;

            bool expected = false; //since user is not logged in
            bool actual = blog.PublishPage(page);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PublishPage_InvalidPage_GivesException_Test()
        {
            Page page = new Page() { Title = "", Content = "content" };
            blog.UserIsLoggedIn = true;

            bool expected = false;
            bool actual = blog.PublishPage(page);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SendEmail_InvalidParameter_Test()
        {
            string address = ""; //invalid
            string caption = "n";
            string body = "n";
            blog.UserIsLoggedIn = true;

            int expected = 0; //does not send mail because address is empty
            int actual = blog.SendEmail(address, caption, body);

            Assert.AreEqual(expected, actual, "Address is empty");
        }

        [TestMethod]
        public void SendMail_UserLoggedOut_Test()
        {
            blog.UserIsLoggedIn = true;
            string address = "n";
            string caption = "n";
            string body = "n";

            int expected = 1;
            int actual = blog.SendEmail(address, caption, body);

            Assert.AreEqual(expected, actual, "User not logged in");
        }        
    }
}
