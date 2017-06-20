using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace SmartMarket.Web.Business
{
    public class Roles : RoleProvider
    {

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> _Roles = new List<string>();
            using (SmartMarketDB db = new SmartMarketDB())
            {
                if (!string.IsNullOrEmpty(username))
                {
                    try
                    {
                        _Roles = db.UserRoles
                            .Where(obj => obj.User.UserName == username && obj.User.Active)
                            .Select(obj => obj.Role.Roles).ToList();
                    }
                    catch (Exception)
                    {
                        throw new Exception("GetRolesForUser " + username);
                    }
                }
            }
            return _Roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool isUserInRole = false;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(roleName))
            {
                using (SmartMarketDB db = new SmartMarketDB())
                {
                    try
                    {
                        isUserInRole = db.UserRoles
                            .Any(obj => obj.User.UserName == username && obj.User.Active && obj.Role.Roles == roleName);
                    }
                    catch (Exception)
                    {
                        throw new Exception("IsUserInRole User: " + username + " Role: " + roleName);
                    }
                }

            }
            return isUserInRole;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}