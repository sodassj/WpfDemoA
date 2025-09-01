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
using System.Windows.Shapes;

namespace WpfDemoA
{
    public partial class ListaConductoresWindow : Window
    {
        public ListaConductoresWindow()
        {
            InitializeComponent();

            // Inicializamos datos de ejemplo solo si no hay datos cargados
            if (!DataManager.Conductores.Any())
            {
                DataManager.InicializarDatosEjemplo();
            }

            CargarConductores();
        }

        // Método para cargar la lista en el DataGrid
        private void CargarConductores()
        {
            dgConductores.ItemsSource = null;
            dgConductores.ItemsSource = DataManager.Conductores;

            // Mostrar u ocultar el mensaje "No hay datos"
            lblSinDatos.Visibility = DataManager.Conductores.Count == 0
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            CargarConductores();

            MessageBox.Show("Lista de conductores actualizada.", "Información",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            Close();
        }

        // (Opcional) Si agregas un botón de eliminar
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgConductores.SelectedItem is Conductor conductorSeleccionado)
            {
                var result = MessageBox.Show(
                    $"¿Está seguro que desea eliminar al conductor '{conductorSeleccionado.Nombre}'?",
                    "Confirmación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    DataManager.EliminarConductor(conductorSeleccionado);
                    CargarConductores();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un conductor de la lista.", "Atención",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
