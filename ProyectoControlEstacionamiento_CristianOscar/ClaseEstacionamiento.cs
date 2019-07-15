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
        private int idTipoVehiculo;
        private decimal costo;
        private int tiempoTotal;

        SqlConnection cn = new SqlConnection("Data Source=OSCKAR_BENITES\\SQLEXPRESS;Initial Catalog=Estacionamiento;Integrated Security=True");
        public ClaseEstacionamiento()
        {
            placaVehiculo = "PorDefecto";
            idTipoVehiculo = 0;
        }


        public string PlacaVehiculo
        {
            get { return placaVehiculo; }
            set { placaVehiculo = value; }
        }

        public int IdTipoVehiculo
        {
            get { return idTipoVehiculo; }
            set { idTipoVehiculo = value; }
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
            string query = "SELECT COUNT(*) FROM Parqueo.Vehiculo WHERE placaVehiculo=@placaVehiculo AND idTipoVehiculo=@idTipoVehiculo";
            SqlCommand comando = new SqlCommand(query, cn);
            comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
            comando.Parameters.AddWithValue("@idTipoVehiculo", idTipoVehiculo);
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
            string query = "SELECT COUNT(*) FROM Parqueo.Vehiculo WHERE placaVehiculo=@placaVehiculo AND idTipoVehiculo=@idTipoVehiculo";
            SqlCommand comando = new SqlCommand(query, cn);
            comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
            comando.Parameters.AddWithValue("@idTipoVehiculo", idTipoVehiculo);
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
                    string query = "INSERT INTO Parqueo.Vehiculo VALUES (@placaVehiculo,@idTipovehiculo)";
                    SqlCommand comando = new SqlCommand(query, cn);
                    comando.Parameters.AddWithValue("@placaVehiculo", placaVehiculo);
                    comando.Parameters.AddWithValue("@idTipoVehiculo", idTipoVehiculo);
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
        

        // Listas a mostrar
        public List<ClaseEstacionamiento> MostrarEntrada()
        {
            cn.Open();
            String query = @"SELECT placaVehiculo, horaEntrada FROM Parqueo.Detalle";
            SqlCommand comando = new SqlCommand(query, cn);
            List<ClaseEstacionamiento> Lista = new List<ClaseEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                ClaseEstacionamiento dato = new ClaseEstacionamiento();
                dato.placaVehiculo = reder.GetString(0);
                dato.HoraEntrada = reder.GetDateTime(1);

                Lista.Add(dato);
            }
            reder.Close();
            cn.Close();
            return Lista;
        }
        
    }
}

        


