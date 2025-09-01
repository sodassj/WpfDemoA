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
    public partial class IngresosWindow : Window
    {
        // PROPIEDAD que MainWindow usará para recuperar el nuevo ingreso
        public Ingreso NuevoIngreso { get; private set; }

        public IngresosWindow()
        {
            InitializeComponent();

            // Establecer fecha actual
            dtpFecha.SelectedDate = DateTime.Now;

            // Seleccionar primer item por defecto
            if (cmbTipoDocumento.Items.Count > 0)
                cmbTipoDocumento.SelectedIndex = 0;

            if (cmbTurno.Items.Count > 0)
                cmbTurno.SelectedIndex = 0;
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
            {
                try
                {
                    string tipoDoc = ((System.Windows.Controls.ComboBoxItem)cmbTipoDocumento.SelectedItem).Content.ToString();
                    string numeroDoc = txtNumeroDocumento.Text.Trim();
                    string placa = txtPlaca.Text.Trim().ToUpper();
                    string turno = ((System.Windows.Controls.ComboBoxItem)cmbTurno.SelectedItem).Content.ToString();
                    string nombreConductor = txtNombreConductor.Text.Trim();
                    string nombreCliente = txtNombreCliente.Text.Trim();
                    DateTime fechaHora = dtpFecha.SelectedDate ?? DateTime.Now;

                    if (!decimal.TryParse(txtPesoIngreso.Text, out decimal peso))
                    {
                        MostrarMensaje("El peso debe ser un número válido.", Brushes.Red);
                        return;
                    }

                    // Creamos el objeto y lo guardamos en la propiedad
                    NuevoIngreso = new Ingreso
                    {
                        TipoDocumento = tipoDoc,
                        NumeroDocumento = numeroDoc,
                        Placa = placa,
                        Turno = turno,
                        NombreConductor = nombreConductor,
                        NombreCliente = nombreCliente,
                        FechaHora = fechaHora,
                        PesoIngreso = peso
                    };

                    // También lo agregamos a la lista global si quieres mantener DataManager sincronizado
                    DataManager.Ingresos.Add(NuevoIngreso);

                    this.DialogResult = true; // Para que MainWindow sepa que se guardó algo
                    this.Close();
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al registrar ingreso: {ex.Message}", Brushes.Red);
                }
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }

        private bool ValidarFormulario()
        {
            if (cmbTipoDocumento.SelectedItem == null)
            {
                MostrarMensaje("Por favor seleccione el tipo de documento.", Brushes.Red);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroDocumento.Text))
            {
                MostrarMensaje("Por favor ingrese el número de documento.", Brushes.Red);
                txtNumeroDocumento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPlaca.Text))
            {
                MostrarMensaje("Por favor ingrese la placa.", Brushes.Red);
                txtPlaca.Focus();
                return false;
            }

            if (cmbTurno.SelectedItem == null)
            {
                MostrarMensaje("Por favor seleccione el turno.", Brushes.Red);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreConductor.Text))
            {
                MostrarMensaje("Por favor ingrese el nombre del conductor.", Brushes.Red);
                txtNombreConductor.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MostrarMensaje("Por favor ingrese el nombre del cliente.", Brushes.Red);
                txtNombreCliente.Focus();
                return false;
            }

            if (dtpFecha.SelectedDate == null)
            {
                MostrarMensaje("Por favor seleccione la fecha.", Brushes.Red);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPesoIngreso.Text))
            {
                MostrarMensaje("Por favor ingrese el peso de ingreso.", Brushes.Red);
                txtPesoIngreso.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPesoIngreso.Text, out decimal peso) || peso <= 0)
            {
                MostrarMensaje("El peso debe ser un número positivo válido.", Brushes.Red);
                txtPesoIngreso.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            cmbTipoDocumento.SelectedIndex = 0;
            txtNumeroDocumento.Clear();
            txtPlaca.Clear();
            cmbTurno.SelectedIndex = 0;
            txtNombreConductor.Clear();
            txtNombreCliente.Clear();
            dtpFecha.SelectedDate = DateTime.Now;
            txtPesoIngreso.Clear();
            lblMensaje.Visibility = Visibility.Collapsed;
            txtNumeroDocumento.Focus();
        }

        private void MostrarMensaje(string mensaje, SolidColorBrush color)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Foreground = color;
            lblMensaje.Visibility = Visibility.Visible;

            System.Threading.Tasks.Task.Delay(5000).ContinueWith(t =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (lblMensaje.Text == mensaje)
                        lblMensaje.Visibility = Visibility.Collapsed;
                });
            });
        }
    }
}
