using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROYECTOFINAL
{
    public partial class RegistrosPF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        public void llenarGrid()
        {

            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            DataTable dt = new DataTable();

            try
            {
                using (Conn = PROYECTOFINAL.DBconn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("LISTARUSUARIOS", Conn))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            retorno = cmd.ExecuteNonQuery();
                            cmd.Connection = Conn;
                            sda.SelectCommand = cmd;

                            using (dt = new DataTable())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                sda.Fill(dt);
                                this.GridView1.DataSource = dt;
                                this.GridView1.DataBind();
                            }
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }
        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-IMC8MH0\\SQLEXPRESS;Initial Catalog=PROYECTOFINAL;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("AGREGARUSUARIOS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Tipo", Dtipor.Text);
            cmd.Parameters.AddWithValue("Nombre", tusuarior.Text);
            cmd.Parameters.AddWithValue("Correo", tcorreor.Text);
            cmd.Parameters.AddWithValue("Descripcion", tdescripcionr.Text);
            cmd.Parameters.AddWithValue("Fecha", tfechar.Text);
            cmd.Parameters.AddWithValue("Monto", tmontor.Text);
            cmd.Parameters.AddWithValue("Clave", tclaver.Text);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Exception adding account. " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            llenarGrid();
        }

        protected void Beliminar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-IMC8MH0\\SQLEXPRESS;Initial Catalog=PROYECTOFINAL;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("ELIMINARUSUARIOS", conn);
            cmd.CommandType = CommandType.StoredProcedure; ;
            cmd.Parameters.AddWithValue("Nombre", tusuarior.Text);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Exception adding account. " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            llenarGrid();
        }

        protected void Bactualizar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-IMC8MH0\\SQLEXPRESS;Initial Catalog=PROYECTOFINAL;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("ACTUALIZARUSUARIOS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Tipo", Dtipor.Text);
            cmd.Parameters.AddWithValue("Nombre", tusuarior.Text);
            cmd.Parameters.AddWithValue("Clave", tclaver.Text);
            cmd.Parameters.AddWithValue("Correo", tcorreor.Text);
            cmd.Parameters.AddWithValue("Descripcion", tdescripcionr.Text);
            cmd.Parameters.AddWithValue("Fecha", tfechar.Text);
            cmd.Parameters.AddWithValue("Monto", tmontor.Text);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Exception adding account. " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            llenarGrid();
        }

        protected void Bvolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioPF.aspx");
        }
    }
}