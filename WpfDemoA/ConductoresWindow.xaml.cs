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
    public partial class ConductoresWindow : Window
    {
        public ConductoresWindow()
        {
            InitializeComponent();
            txtNombreConductor.Focus();
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
            {
                try
                {
                    string nombre = txtNombreConductor.Text.Trim();
                    string licencia = txtLicencia.Text.Trim();
                    string transporte = txtTransporte.Text.Trim();
                    string tipoDocumento = (cmbTipoDocumento.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "DNI";
                    string numeroDocumento = txtNumeroDocumento.Text.Trim();
                    string telefono = txtTelefono.Text.Trim();

                    // Verificar si ya existe un conductor con la misma licencia
                    if (DataManager.Conductores.Any(c => c.Licencia.Equals(licencia, StringComparison.OrdinalIgnoreCase)))
                    {
                        MostrarMensaje("Ya existe un conductor con esa licencia.", Brushes.Red);
                        txtLicencia.Focus();
                        txtLicencia.SelectAll();
                        return;
                    }

                    // Agregar conductor con todos los campos
                    DataManager.AgregarConductor(
                        nombre: nombre,
                        licencia: licencia,
                        transporte: transporte,
                        tipoDocumento: tipoDocumento,
                        numeroDocumento: numeroDocumento,
                        telefono: telefono
                    );

                    MostrarMensaje("¡Conductor registrado exitosamente!", Brushes.Green);

                    // Limpiar formulario después de 2 segundos
                    System.Threading.Tasks.Task.Delay(2000).ContinueWith(t =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            LimpiarFormulario();
                        });
                    });
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al registrar conductor: {ex.Message}", Brushes.Red);
                }
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombreConductor.Text))
            {
                MostrarMensaje("Por favor ingrese el nombre del conductor.", Brushes.Red);
                txtNombreConductor.Focus();
                return false;
            }

            if (txtNombreConductor.Text.Trim().Length < 3)
            {
                MostrarMensaje("El nombre debe tener al menos 3 caracteres.", Brushes.Red);
                txtNombreConductor.Focus();
                txtNombreConductor.SelectAll();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLicencia.Text))
            {
                MostrarMensaje("Por favor ingrese el número de licencia.", Brushes.Red);
                txtLicencia.Focus();
                return false;
            }

            if (txtLicencia.Text.Trim().Length < 5)
            {
                MostrarMensaje("La licencia debe tener al menos 5 caracteres.", Brushes.Red);
                txtLicencia.Focus();
                txtLicencia.SelectAll();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTransporte.Text))
            {
                MostrarMensaje("Por favor ingrese el nombre del transporte.", Brushes.Red);
                txtTransporte.Focus();
                return false;
            }

            if (txtTransporte.Text.Trim().Length < 3)
            {
                MostrarMensaje("El nombre del transporte debe tener al menos 3 caracteres.", Brushes.Red);
                txtTransporte.Focus();
                txtTransporte.SelectAll();
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            txtNombreConductor.Clear();
            txtLicencia.Clear();
            txtTransporte.Clear();
            lblMensaje.Visibility = Visibility.Collapsed;

            // Enfocar el primer campo
            txtNombreConductor.Focus();
        }

        private void MostrarMensaje(string mensaje, SolidColorBrush color)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Foreground = color;
            lblMensaje.Visibility = Visibility.Visible;

            // Ocultar mensaje después de 5 segundos
            System.Threading.Tasks.Task.Delay(5000).ContinueWith(t =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (lblMensaje.Text == mensaje) // Solo ocultar si es el mismo mensaje
                    {
                        lblMensaje.Visibility = Visibility.Collapsed;
                    }
                });
            });
        }
    }
}
