using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace ProyectoControlEstacionamiento_CristianOscar
{
    class ClaseEstacionamiento
    {
        private DateTime horaEntrada;
        private DateTime horaSalida;
        private string placaVehiculo;
        private string tipoVehiculo;
        private decimal costo;
        private int tiempoTotal;

        SqlConnection cn = new SqlConnection("Data Source=OSCKAR_BENITES\\SQLEXPRESS;Initial Catalog=Estacionamiento;Integrated Security=True");
        public ClaseEstacionamiento()
        {
            placaVehiculo = "PorDefecto";
            tipoVehiculo = "PorDefecto";
        }


        public string PlacaVehiculo
        {
            get { return placaVehiculo; }
            set { placaVehiculo = value; }
        }

        public string TipoVehiculo
        {
            get { return tipoVehiculo; }
            set { tipoVehiculo = value; }
        }

        public DateTime HoraEntrada
        {
            get { return horaEntrada; }
            set { horaEntrada = value; }
        }

        public DateTime HoraSalida
        {
            get { return horaSalida; }
            set { horaSalida = value; }
        }

        public decimal Costo
        {
            get { return costo; }
            set { costo = value; }
        }

        public int TiempoTotal
        {
            get { return tiempoTotal; }
            set { tiempoTotal = value; }
        }


        //Validación si la placa existe 
        public Boolean Validar()
        {
            cn.Open();
            string query = "SELECT COUNT(*) FROM Parqueo.Vehiculo WHERE placaVehiculo=@placaVehiculo";
            SqlCommand comando = new SqlCommand(query, cn);
            comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
            comando.Parameters.AddWithValue("@tipoVehiculo", tipoVehiculo);
            int cant = Convert.ToInt32(comando.ExecuteScalar());
            cn.Close();
            if (cant == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //Valida si el vehículo coincide con la placa
        public Boolean ValidarVehiculo()
        {
            cn.Open();
            string query = "SELECT COUNT(*) FROM Parqueo.Vehiculo WHERE placaVehiculo=@placaVehiculo AND tipoVehiculo=@tipoVehiculo";
            SqlCommand comando = new SqlCommand(query, cn);
            comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
            comando.Parameters.AddWithValue("@tipoVehiculo", tipoVehiculo);
            int cant = Convert.ToInt32(comando.ExecuteScalar());
            cn.Close();
            if (cant == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }


        //Insertar vehiculo a la base de datos
        public void InsertarVehiculo()
        {
            //Si la placa no existe guardarse en la base de datos
            if (Validar() == false)
            {
                try
                {
                    cn.Open();
                    string query = "INSERT INTO Parqueo.Vehiculo VALUES (@placaVehiculo,@tipovehiculo)";
                    SqlCommand comando = new SqlCommand(query, cn);
                    comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
                    comando.Parameters.AddWithValue("@tipoVehiculo", tipoVehiculo);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("El vehiculo se ha agregado correctamente");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    cn.Close();
                }
            }

            //Guardado de cada ingreso de un nuevo vehiculo
            if (ValidarVehiculo() == true)
            {
                try
                {
                    cn.Open();
                    string query = "INSERT INTO Parqueo.Detalle VALUES (@placa,GETDATE(),GETDATE(),0,0)";
                    SqlCommand comando = new SqlCommand(query, cn);
                    comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Bienvenido :)");
                    cn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("El vehiculo aun se encuentra esta en el estacionamiento");
                }
            }
            else
            {
                MessageBox.Show("El vehiculo no coincide con la placa registrada");
            }

        }

        public void SalidaVehiculo()
        {
            try
            {
                cn.Open();
                string query = "UPDATE Parqueo.Detalle SET horaSalida=GETDATE() WHERE placaVehiculo = @placaVehiculo";
                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
                comando.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error con la salida el vehiculo");
            }
            finally
            {
                try
                {
                    cn.Open();
                    string query = "DELETE FROM Parqueo.Detalle where placaVehiculo = @placaVehiculo";
                    SqlCommand comando = new SqlCommand(query, cn);
                    comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
                    comando.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Placa no válida");

                }
            }
        }

        // Listas a mostrar
        public List<ClaseEstacionamiento> MostrarEntrada()
        {
            cn.Open();
            String query = @"SELECT placaVehiculo,tipoVehiculo,horaEntrada FROM Parqueo.Vehiculo  INNER JOIN Parqueo.Detalle he
                                        ON placaVehiculo = he.placaVehiculo WHERE placaVehiculo = placaVehiculo";
            SqlCommand comando = new SqlCommand(query, cn);
            List<ClaseEstacionamiento> Lista = new List<ClaseEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                ClaseEstacionamiento dato = new ClaseEstacionamiento();
                dato.placaVehiculo = reder.GetString(0);
                dato.tipoVehiculo = reder.GetString(1);
                dato.horaEntrada = reder.GetDateTime(2);

                Lista.Add(dato);
            }
            reder.Close();
            cn.Close();
            return Lista;
        }

        public List<ClaseEstacionamiento> BuscarEntrada()
        {

            cn.Open();
            String query = @"SELECT placaVehiculo,tipoVehiculo,horaEntrada FROM Parqueo.Vehiculo  INNER JOIN Parqueo.Detalle he
                                        ON placaVehiculo = he.placaVehiculo WHERE placaVehciulo = @placaVehiculo";
            SqlCommand comando = new SqlCommand(query, cn);

            comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
            List<ClaseEstacionamiento> ListaB = new List<ClaseEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                ClaseEstacionamiento datob = new ClaseEstacionamiento();
                datob.placaVehiculo = reder.GetString(0);
                datob.tipoVehiculo = reder.GetString(1);
                datob.horaEntrada = reder.GetDateTime(2);

                ListaB.Add(datob);
            }
            reder.Close();
            cn.Close();
            return ListaB;
        }


        // lista que muestra el reporte
        public List<ClaseEstacionamiento> MostrarReporte()
        {
            cn.Open();
            string query = "SELECT * FROM Parqueo.Detalle ";
            SqlCommand comando = new SqlCommand(query, cn);
            List<ClaseEstacionamiento> reporte = new List<ClaseEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                ClaseEstacionamiento datoR = new ClaseEstacionamiento();
                datoR.placaVehiculo = reder.GetString(1);
                datoR.tipoVehiculo = reder.GetString(2);
                datoR.horaEntrada = reder.GetDateTime(3);
                datoR.horaSalida = reder.GetDateTime(4);
                datoR.tiempoTotal = reder.GetInt32(5);
                datoR.costo = reder.GetDecimal(6);


                reporte.Add(datoR);
            }
            reder.Close();
            cn.Close();
            return reporte;
        }
    }
}

        


