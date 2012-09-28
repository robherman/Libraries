using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Southapps.Framework.Backoffice.SDK.DataAccess;
using System.Web.UI.HtmlControls;
using Southapps.Framework.Backoffice.SDK.Model;

namespace Southapps.Framework.Backoffice
{
    public abstract class BaseMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadLoginData();
                this.CheckPermissions();
            }
        }

        private void LoadLoginData()
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            User user = (User)Session["User"];

            this.OnLoadLoginData(user.name, user.bo_role.name);
        }

        protected abstract void OnLoadLoginData(string username, string role);
        protected abstract void CheckPermissions();

        private void CheckModulePermissions(string module, HtmlGenericControl menu, HtmlGenericControl createOption, HtmlGenericControl listOption)
        {
            if (PermissionManager.UserCanViewMenu(module))
            {
                menu.Visible = true;

                if (PermissionManager.UserHasPermission(module, Permissions.View))
                {
                    listOption.Visible = true;
                }

                if (PermissionManager.UserHasPermission(module, Permissions.Add))
                {
                    createOption.Visible = true;
                }
            }
        }
    }
}
