using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Southapps.Framework.Backoffice
{
    public class ModuleMenuItem
    {
        private string module;
        private HtmlGenericControl addControl;
        private HtmlGenericControl viewControl;

        public string Module
        {
            get { return module; }
            set { module = value; }
        }
        
        public HtmlGenericControl ViewControl
        {
            get { return viewControl; }
            set { viewControl = value; }
        }
        
        public HtmlGenericControl AddControl
        {
            get { return addControl; }
            set { addControl = value; }
        }
    }
}
