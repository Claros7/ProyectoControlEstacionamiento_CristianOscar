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

//Bibliotecas para SQL
using System.Data;
using System.Data.SqlClient;

namespace ProyectoControlEstacionamiento_CristianOscar
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection cn = new SqlConnection("Data Source=OSCKAR_BENITES\\SQLEXPRESS;Initial Catalog=Estacionamiento;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Dtgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
