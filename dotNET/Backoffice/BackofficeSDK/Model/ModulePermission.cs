using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Southapps.Framework.Backoffice.SDK.Model
{
    public class ModulePermission
    {
        private Permissions permission;
        private string module;
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        
        public Permissions Permission
        {
            get { return permission; }
            set { permission = value; }
        }

        public string Module
        {
            get { return module; }
            set { module = value; }
        }
    }
}
