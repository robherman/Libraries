using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using Southapps.Framework.Backoffice.SDK.Model;

namespace Southapps.Framework.Backoffice
{
    public abstract class GridPage : BasePage
    {
        protected string module;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.module = PermissionManager.GetBackendModuleFromUrl(Path.GetFileName(Request.PhysicalPath));
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            FixGridViewTable();
            CheckPageControlPermissions();
        }

        private void FixGridViewTable()
        {
            var grid = Utils.FindControlRecursive(Page, "grdView") as GridView;

            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                grid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GrdView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var gridView = Utils.FindControlRecursive(Page, "grdView") as GridView;

            gridView.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindGridData();
        }

        protected void GrdView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            var gridView = Utils.FindControlRecursive(Page, "grdView") as GridView;
            //Reset the edit index.
            gridView.EditIndex = -1;
            //Bind data to the GridView control.
            BindGridData();
        }

        protected void GrdView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BindGridData();
        }

        protected void CheckPageControlPermissions()
        {
            var addButton = Utils.FindControlRecursive(Page, "btnAdd");

            if (addButton != null)
            {
                addButton.Visible = PermissionManager.UserHasPermission(this.module, Permissions.Add);
            }
        }

        protected void CheckGridControlPermissions(GridViewRow row)
        {
            var modifyButton = row.FindControl("imgBtnEdit");

            if (modifyButton != null)
            {
                modifyButton.Visible = PermissionManager.UserHasPermission(this.module, Permissions.Modify);
            }

            var deleteButton = row.FindControl("imgBtnDelete");

            if (deleteButton != null)
            {
                deleteButton.Visible = PermissionManager.UserHasPermission(this.module, Permissions.Remove);
            }
        }

        protected abstract void BindGridData();
    }
}