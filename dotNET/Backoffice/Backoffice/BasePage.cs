using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using SouthApps.STS.SDK.DAL.Model;
using System.Web.UI.WebControls;

namespace Southapps.Framework.Backoffice
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
            this.Load += new EventHandler(this.Page_Load);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckUrlAccess();
            this.CheckSession();
            this.ShowPendingMessages();
        }

        private void CheckUrlAccess()
        {
            if (!PermissionManager.UserHasUrlAccess(Request.Path.Substring(Request.Path.LastIndexOf("/") + 1)))
            {
                Response.Redirect("~/Error.aspx?code=403");
            }
        }

        private void CheckSession()
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx?RedirectUrl=" + HttpContext.Current.Request.Url.AbsolutePath);
            }
        }

        private void ShowPendingMessages()
        {
            Control divSuccess = Utils.FindControlRecursive(Page, "divSuccess");

            if (divSuccess != null)
            {
                if (Session["showSuccess"] != null)
                {
                    divSuccess.Visible = true;
                    Session["showSuccess"] = null;
                }
                else
                {
                    divSuccess.Visible = false;
                }
            }

            Control divError = Utils.FindControlRecursive(Page, "divError");

            if (divError != null)
            {
                if (Session["showError"] != null)
                {
                    divError.Visible = true;
                    
                    Session["showError"] = null;

                    if (Session["errorMessage"] != null)
                    {
                        Label lblErrorDescription = (Label)Utils.FindControlRecursive(Page, "lblErrorDescription");
                        lblErrorDescription.Text = Session["errorMessage"].ToString();
                    }
                }
                else
                {
                    divError.Visible = false;
                }

                return;
            }
        }

        protected void SetSuccess()
        {
            Session["showSuccess"] = true;

        }

        protected void SetSuccessOnPostback()
        {
            Control divSuccess = Utils.FindControlRecursive(Page, "divSuccess");
            divSuccess.Visible = true;
        }

        protected void SetError()
        {
            Session["showError"] = true;
        }

        protected void SetError(string customDescription)
        {
            Session["showError"] = true;
            Session["errorMessage"] = customDescription;
        }

        protected void SetErrorOnPostback(string customDescription)
        {
            Control divError = Utils.FindControlRecursive(Page, "divError");
            divError.Visible = true;

            if (!string.IsNullOrEmpty(customDescription))
            {
                Label errorDescription = (Label)Utils.FindControlRecursive(Page, "lblErrorDescription");
                errorDescription.Text = customDescription;
            }
        }

        protected void SetErrorOnPostback()
        {
            this.SetErrorOnPostback(null);
        }
    }
}