using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROYECTOFINAL
{
    public partial class InicioPF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Bregistrarp_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrosPF.aspx");
        }

        protected void Breporte_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportesPF.aspx");
        }
    }
}