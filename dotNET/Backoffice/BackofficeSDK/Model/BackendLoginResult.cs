using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Southapps.Framework.Backoffice.SDK.Model;
using Southapps.Framework.Backoffice.SDK.DataAccess;

namespace SouthApps.STS.SDK.DAL.Model
{
    public class BackendLoginResult
    {
        private List<ModulePermission> permissions;
        private bool authenticated;
        private User user;

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public List<ModulePermission> Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }
        
        public bool Authenticated
        {
            get { return authenticated; }
            set { authenticated = value; }
        }
    }
}
