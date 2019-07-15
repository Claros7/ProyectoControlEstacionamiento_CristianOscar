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

        public MainWindow()
        {
            InitializeComponent();
            
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
                parqueo.IdTipoVehiculo = cmbTipoVehiculo.Text;
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



// Boton Pagar
private void BtnPagar_Click(object sender, RoutedEventArgs e)
{
    if (lbReporteVehiculo2.SelectedItem == null)
        MessageBox.Show("Debes seleccionar un Vehiculo");
    else
    {
        ClaseEstacionamiento estacionamiento = new ClaseEstacionamiento();
        estacionamiento.PlacaVehiculo = txtBuscarPlaca.Text;
        estacionamiento.SalidaVehiculo();
        MessageBox.Show("Gracias por su visita :)");
    }
    //CalcularPago();
    this.lbReporteVehiculo.ItemsSource = estacionamiento.MostrarEntrada();
    txtBuscarPlaca.Text = String.Empty;
    lbReporteVehiculo.ItemsSource = "";
    txtBuscarPlaca.Focus();
}


//boton cancelar
private void BtnCancelarBuscar_Click(object sender, RoutedEventArgs e)
{
    txtBuscarPlaca.Text = String.Empty;
    lbReporteVehiculo.ItemsSource = "";
    txtBuscarPlaca.Focus();
}

private void Buscar(object sender, RoutedEventArgs e)
{
    if (txtBuscarPlaca.Text == string.Empty)
    {
        MessageBox.Show("Debe ingresar una placa.");
        txtBuscarPlaca.Focus();
    }
    ClaseEstacionamiento estacionamiento = new ClaseEstacionamiento();

    estacionamiento.PlacaVehiculo = txtBuscarPlaca.Text;
    this.lbReporteVehiculo2.ItemsSource = estacionamiento.BuscarEntrada();
    this.lbReporteVehiculo2.SelectedValuePath = estacionamiento.PlacaVehiculo;
}
        
    }
}


