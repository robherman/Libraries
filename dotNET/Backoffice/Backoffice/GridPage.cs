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

        public string GridViewSortExpression
        {
            get
            {
                if (ViewState["sortExpression"] == null)
                    return string.Empty;

                return (string)ViewState["sortExpression"];
            }
            set { ViewState["sortExpression"] = value; }
        }

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected int GetColumnIndex(string SortExpression)
        {
            var grid = Utils.FindControlRecursive(Page, "grdView") as GridView;
            int i = 0;
            foreach (DataControlField c in grid.Columns)
            {
                if (c.SortExpression == SortExpression)
                    break;
                i++;
            }
            return i;
        }

        protected void GrdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var grdView = Utils.FindControlRecursive(Page, "grdView") as GridView;
            grdView.PageIndex = e.NewPageIndex;
            BindGridData(GridViewSortExpression, GridViewSortDirection);
        }

        protected void GrdView_Sorting(object sender, GridViewSortEventArgs e)
        {
            var grdView = Utils.FindControlRecursive(Page, "grdView") as GridView;

            string sortExpression = e.SortExpression;

            GridViewSortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
            }

            this.BindGridData(e.SortExpression, GridViewSortDirection);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            FixGridViewTable();
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

            if (grid.TopPagerRow != null)
            {
                grid.TopPagerRow.TableSection = TableRowSection.TableHeader;
            }

            if (grid.BottomPagerRow != null)
            {
                grid.BottomPagerRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            var grdView = sender as GridView;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell tc in e.Row.Cells)
                {
                    if (tc.HasControls())
                    {
                        // search for the header link
                        LinkButton lnk = (LinkButton)tc.Controls[0];
                        if (lnk != null && GridViewSortExpression == lnk.CommandArgument)
                        {
                            // inizialize a new image
                            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                            // setting the dynamically URL of the image
                            img.ImageUrl = "~/styles/" + (GridViewSortDirection == SortDirection.Ascending ? "asc" : "desc") + ".gif";
                            // adding a space and the image to the header link
                            tc.Controls.Add(new LiteralControl(" "));
                            tc.Controls.Add(img);

                        }
                    }
                }
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

        protected virtual void BindGridData(string sortExpression, SortDirection direction)
        {

        }

        protected virtual void BindGridData()
        {
            this.BindGridData(GridViewSortExpression, GridViewSortDirection);
        }
    }
}