using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROYECTOFINAL
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bregistrarse_Click(object sender, EventArgs e)
        {
            Clsusuario.cedula = tcedula.Text;
            Clsusuario.nombre = tnombre.Text;
            Clsusuario.apellido = tapellido.Text;
            Clsusuario.correo = temail.Text;
            Clsusuario.usuario = tusuario.Text;
            Clsusuario.clave = tclave.Text;

            int resultado = Clsusuario.RegistroPersona();

            if (resultado > 0) {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Notification : Usuario ha sido registrado');", true);
                Response.Redirect("Login.aspx");

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Notification : No se ha podido registrar el usuario');", true);
                Response.Redirect("RegisterPage.aspx");
            }
            return;

        }
    }
}