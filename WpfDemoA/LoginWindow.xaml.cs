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
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            // Permitir login con Enter
            this.KeyDown += LoginWindow_KeyDown;
        }

        private void LoginWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string password = txtPassword.Password;

            // Validación de credenciales
            if (usuario == "david" && password == "123456")
            {
                // Login exitoso
                MostrarMensaje("¡Login exitoso! Bienvenido al sistema.", Brushes.Green);

                // Esperar un poco y abrir menú principal
                System.Threading.Tasks.Task.Delay(1000).ContinueWith(t =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    });
                });
            }
            else
            {

                // Login fallido
                MostrarMensaje("Usuario o contraseña incorrectos. Por favor, verifique sus credenciales.", Brushes.Red);
                txtPassword.Clear();
                txtUsuario.Focus();
            }
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
                    lblMensaje.Visibility = Visibility.Collapsed;
                });
            });
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
