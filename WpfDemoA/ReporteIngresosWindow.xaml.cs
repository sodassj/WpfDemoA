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
    /// <summary>
    /// Lógica de interacción para ReporteIngresosWindow.xaml
    /// </summary>
    public partial class ReporteIngresosWindow : Window
    {
        public ReporteIngresosWindow(List<Ingreso> ingresos)
        {
            InitializeComponent();
            dataGridIngresos.ItemsSource = ingresos;
        }
    }
}