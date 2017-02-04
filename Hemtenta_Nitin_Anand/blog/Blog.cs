using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.blog
{
    public class Blog : IBlog
    {
        //private IAuthenticator authenticator;
        //public Blog(IAuthenticator authenticator)
        //{
        //    this.authenticator = authenticator;
        //}
        public void LoginUser(User u)
        {
            if (String.IsNullOrEmpty(u.Name) || String.IsNullOrEmpty(u.Password))
            {
                throw new ArgumentException("Username can't be empty");
            }
            UserIsLoggedIn = true;
        }

        public bool UserIsLoggedIn { get; set; }

        public void LogoutUser(User u)
        {
            if (String.IsNullOrEmpty(u.Name))
            {
                throw new ArgumentException("Invalid User");
            }
            else if (u != null)
            {
                UserIsLoggedIn = true;
            }
            UserIsLoggedIn = false;

        }

        public bool PublishPage(Page p)
        {
            if (String.IsNullOrEmpty(p.Title) || String.IsNullOrEmpty(p.Content))
            {
                throw new ArgumentException("Page is not valid");
            }
            else if (p != null && UserIsLoggedIn)
            {
                return true;
            }
            return false;
        }

        // För att skicka e-post måste användaren vara
        // inloggad och alla parametrar ha giltiga värden.
        // Returnerar 1 om det gick att skicka mailet,
        // 0 annars.
        public int SendEmail(string address, string caption, string body)
        {
            if (UserIsLoggedIn && 
                !String.IsNullOrEmpty(address) 
                && !String.IsNullOrEmpty(caption)
                && !String.IsNullOrEmpty(body))
            {
                return 1;
            }
            return 0;
        }
    }
}
