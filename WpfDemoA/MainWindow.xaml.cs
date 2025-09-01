using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDemoA
{
    public partial class MainWindow : Window
    {
        // Lista de ingresos para compartir entre ventanas
        private List<Ingreso> ingresos = new List<Ingreso>();

        public MainWindow()
        {
            InitializeComponent();

            // Inicializamos datos de ejemplo
            DataManager.InicializarDatosEjemplo();
            ingresos = new List<Ingreso>(DataManager.Ingresos);
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cerrar sesión?",
                               "Confirmar",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        // ================== OPERACIONES ==================
        private void BtnIngresos_Click(object sender, RoutedEventArgs e)
        {
            IngresosWindow ingresosWindow = new IngresosWindow();

            // Si el usuario guardó un nuevo ingreso, lo agregamos a la lista
            if (ingresosWindow.ShowDialog() == true && ingresosWindow.NuevoIngreso != null)
            {
                ingresos.Add(ingresosWindow.NuevoIngreso);
                MessageBox.Show("Ingreso agregado correctamente.", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnSalidas_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad de Salidas en desarrollo...",
                            "Información",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        // ================== MANTENIMIENTOS ==================
        private void BtnConductores_Click(object sender, RoutedEventArgs e)
        {
            ConductoresWindow conductoresWindow = new ConductoresWindow();
            conductoresWindow.ShowDialog();
        }

        private void BtnListaConductores_Click(object sender, RoutedEventArgs e)
        {
            ListaConductoresWindow listaConductoresWindow = new ListaConductoresWindow();
            listaConductoresWindow.ShowDialog();
        }

        private void BtnTransportistas_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad de Transportistas en desarrollo...",
                            "Información",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void BtnCamiones_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad de Camiones en desarrollo...",
                            "Información",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad de Productos en desarrollo...",
                            "Información",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        // ================== REPORTES ==================
        private void BtnReporteCargas_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad de Reporte de Cargas en desarrollo...",
                            "Información",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void BtnReporteIngresos_Click(object sender, RoutedEventArgs e)
        {
            // Pasamos la lista de ingresos al constructor del reporte
            ReporteIngresosWindow reporteIngresosWindow = new ReporteIngresosWindow(ingresos);
            reporteIngresosWindow.ShowDialog();
        }

        private void BtnReporteSalidas_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad de Reporte de Salidas en desarrollo...",
                            "Información",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
    }
}