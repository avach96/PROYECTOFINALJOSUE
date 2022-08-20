using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PROYECTOFINAL
{
    public class Clsusuario
    {
 
        public static string correo { get; set; }
        public static string clave { get; set; }

        public static string cedula { get; set; }
        public static string nombre { get; set; }
        public static string apellido { get; set; }
        public static string usuario { get; set; }


        //meetodos 

        public static int RegistroPersona()
        {
            int retorno = 0;
            int tipo = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = PROYECTOFINAL.DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("Registro", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@cedula", cedula));
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@apellido", apellido));
                    cmd.Parameters.Add(new SqlParameter("@email", correo));
                    cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
                    cmd.Parameters.Add(new SqlParameter("@clave", clave));
                    //Console.WriteLine(cmd);
                    using (SqlDataReader registro = cmd.ExecuteReader())
                    {
                        if (registro.Read())
                        {
                            retorno = 1;
                        }
                    }

                }

            }
            catch
            {

            }
            return retorno;



        }

        public static string Getcorreo()
        {
            return correo;
        }

        public static void  Setcorreo(string email)
        {
            correo = email; ;
        }

        public static string Getclave()
        {
            return clave;
        }

        public static void  Setclave(string password)
        {
            clave = password; 
        }


    }

}