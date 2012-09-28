using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Southapps.Framework.Backoffice
{
    public abstract class AddModifyPage : BasePage
    {
        private int? entityKey;
        private bool isView;

        protected bool IsView
        {
            get
            {
                return this.isView;
            }
        }
        protected int? EntityKey
        {
            get
            {
                if (Request["id"] != null)
                {
                    entityKey = int.Parse(Request["id"]);
                }

                return entityKey;
            }
        }

        protected void Initialize()
        {
            Control ctlTitle = Utils.FindControlRecursive(Page, "lblAddModifyTitle");
            Label lblAddModifyTitle = ctlTitle!=null? (Label)ctlTitle : null;

            Form.Attributes.Add("class", "da-form");

            if (Request["view"] != null)
            {
                this.isView = true;
            }

            if (Request["id"] != null)
            {
                entityKey = int.Parse(Request["id"]);
            }

            this.SetAddModifyTitle(lblAddModifyTitle);
            this.SetBottomControls();

            LoadFields();

            if (entityKey.HasValue)
            {
                LoadFieldsFromEntity(entityKey.Value);
                if (this.isView)
                {
                    SetReadOnlyFields();
                }
            }
        }

        private void SetAddModifyTitle(Label addModifyTitle)
        {
            string addModifyString = null;
    
            if (entityKey.HasValue)
            {
                if (addModifyTitle != null)
                {
                    addModifyString = string.Format(addModifyTitle.Text, "Modificar");
                }

                if (Request["view"] != null)
                {
                    if (addModifyTitle != null)
                    {
                        addModifyString = string.Format(addModifyTitle.Text, "Ver");
                    }
                }
            }
            else
            {
                if (addModifyTitle != null)
                {
                    addModifyString = string.Format(addModifyTitle.Text, "Crear");
                }
            }

            if(!string.IsNullOrEmpty(addModifyString))
                addModifyTitle.Text = addModifyString;
        }

        private void SetBottomControls()
        {
            Button btnSave = (Button)Utils.FindControlRecursive(Page, "btnSave");
            Button btnSaveAndContinue = (Button)Utils.FindControlRecursive(Page, "btnSaveAndContinue");
            Button btnCancel = (Button)Utils.FindControlRecursive(Page, "btnCancel");

            if (entityKey.HasValue)
            {
                if (Request["view"] != null)
                {
                    btnSave.Visible = false;
                    btnCancel.CssClass = "da-button gray";
                    btnCancel.Text = "Volver";
                }
            }
            else
            {
                btnSave.Text = "Crear";

                if (btnSaveAndContinue != null)
                {
                    btnSaveAndContinue.Visible = true;
                }
            }
        }

        protected abstract void LoadFieldsFromEntity(int entityKey);
        protected abstract void LoadFields();
        protected abstract void LoadEntityFromFields();
        protected abstract void SetReadOnlyFields();
    }
}