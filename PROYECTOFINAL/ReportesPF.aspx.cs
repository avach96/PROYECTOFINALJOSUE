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
    public partial class ReportesPF : System.Web.UI.Page
    {
        public float gastos = 0;
        public float ingresos = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            llenarGrid_filtro();

            gastosTotales();
            ingresosTotales();
            rpresupuesto.Text = Convert.ToString(Convert.ToInt32(ingresos) - Convert.ToInt32(gastos));

            
         }

        protected void gastosTotales()
        {
            
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = PROYECTOFINAL.DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("SUMGASTO", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    //Console.WriteLine(cmd);
                    using (SqlDataReader registro = cmd.ExecuteReader())
                    {
                        if (registro.Read())
                        {
                            tgasto.Text = registro[0].ToString();
                            gastos = Convert.ToInt32(registro[0]);
                            Conn.Close();
                        }
                    }
                }
            }
            catch 
            {
                tgasto.Text = "Datos no validos";
            }
        }
        

        protected void ingresosTotales()
        {
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = PROYECTOFINAL.DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("SUMINGRESO", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    //Console.WriteLine(cmd);
                    using (SqlDataReader registro = cmd.ExecuteReader())
                    {
                        if (registro.Read())
                        {
                            tingreso.Text = registro[0].ToString();
                            ingresos = Convert.ToInt32(registro[0]);
                            Conn.Close();
                        }
                    }
                }
            }
            catch
            {
                tingreso.Text = "Datos no validos";
            }
        }



        public void llenarGrid_filtro()
        {

            DataTable dt = new DataTable();
            SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-IMC8MH0\\SQLEXPRESS;Initial Catalog=PROYECTOFINAL;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("TRANSACCION_FILTRO", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("TIPO", Dtipo2.Text);
            Conn.Open();
            try
            {
                using (Conn = PROYECTOFINAL.DBconn.obtenerConexion())
                {


                    cmd.ExecuteNonQuery();
                    using (dt = new DataTable())
                    {

                        SqlDataAdapter sda = new SqlDataAdapter();
                        cmd.CommandType = CommandType.StoredProcedure;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                        this.GridView1.DataSource = dt;
                        this.GridView1.DataBind();

                    }

                }


            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception("Exception adding account. " + ex.Message);
            }
            finally
            {
                Conn.Close();
            }
        }

        protected void bsalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioPF.aspx");
        }
    }
}