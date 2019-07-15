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



