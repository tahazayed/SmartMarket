
using BusinessEntities;
using SmartMarket.Web.Helpers;
using System;
using System.Linq;

namespace SmartMarket.Web.Business
{
    public class User
    {
        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public bool Authenticate(string _username, string _password, string IP)
        {
            bool authenticated = false;
            using (SmartMarketDB db = new SmartMarketDB())
            {
                try
                {
                    string encodedPassword = TextEncoding.EncodeString(_password);
                    BusinessEntities.User loginUser = db.Users
                        .FirstOrDefault(u => u.UserName == _username && u.Password == encodedPassword && u.Active);

                    if (loginUser == null)
                    {
                        authenticated = false;
                    }
                    else
                    {
                        authenticated = true;
                    }
                }
                catch (Exception ex)
                {
                    authenticated = false;
                }
            }
            return authenticated;
        }

        public long GetUserId(string _username)
        {
            long userId = 0;
            using (SmartMarketDB db = new SmartMarketDB())
            {

                var user = (from u in db.Users
                            where u.UserName == _username
                            select u).SingleOrDefault();
                if (user != default(BusinessEntities.User))
                {
                    userId = user.Id;
                }
            }
            return userId;
        }
    }
}