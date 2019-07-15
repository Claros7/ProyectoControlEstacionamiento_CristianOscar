using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoControlEstacionamiento_CristianOscar
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClaseEstacionamiento estacionamiento = new ClaseEstacionamiento();
        SqlConnection cn = new SqlConnection("Data Source=OSCKAR_BENITES\\SQLEXPRESS;Initial Catalog=Estacionamiento;Integrated Security=True");
        private DataTable tabla;
        public MainWindow()
        {
            InitializeComponent();

            tabla = new DataTable();
            try
            {
                cn.Open();
                string query = "SELECT * FROM Parqueo.TipoVehiculo";
                SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
                using (adapter)
                {
                    adapter.Fill(tabla);
                    cmbTipoVehiculo.DisplayMemberPath = "nombreTipo";
                    cmbTipoVehiculo.SelectedValuePath = "idTipoVehiculo";
                    cmbTipoVehiculo.ItemsSource = tabla.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            txtPlaca.Focus();
        }



        private void Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Aceptar(object sender, RoutedEventArgs e)
        {
            ClaseEstacionamiento parqueo = new ClaseEstacionamiento();
            if (txtPlaca.Text.Equals("") == false && cmbTipoVehiculo.SelectedIndex != -1)
            {

                parqueo.PlacaVehiculo = txtPlaca.Text;
                parqueo.IdTipoVehiculo = Convert.ToInt32(cmbTipoVehiculo.SelectedValue);
                parqueo.InsertarVehiculo();
                txtPlaca.Clear();
                cmbTipoVehiculo.SelectedIndex = -1;
                txtPlaca.Focus();
                this.lbReporteVehiculo.ItemsSource = parqueo.MostrarEntrada();
            }
            else
            {
                MessageBox.Show("Ingrese todos los datos para continuar");
                txtPlaca.Clear();
                cmbTipoVehiculo.SelectedIndex = -1;
                txtPlaca.Focus();
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            txtPlaca.Clear();
            cmbTipoVehiculo.SelectedIndex = -1;
            txtPlaca.Focus();
        }









        //Mostar los datos en el listbox
        private void MostrarReporteVehiculo()
        {
            ClaseEstacionamiento estacionamiento = new ClaseEstacionamiento();
            lbReporteVehiculo.ItemsSource = estacionamiento.MostrarEntrada();
        }



      
    }
}



    