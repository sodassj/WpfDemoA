using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemoA
{
    // Modelo para Conductor
    public class Conductor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Estado { get; set; } = "Activo";
        public string Licencia { get; set; } = string.Empty;
        public string Transporte { get; set; } = string.Empty;
    }

    // Modelo para Ingreso
    public class Ingreso
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string Turno { get; set; } = string.Empty;
        public string NombreConductor { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public decimal PesoIngreso { get; set; }
        public string Producto { get; set; } = "Producto General";
        public string Transporte { get; set; } = "Transporte General";
    }

    // Clase estática para manejar los datos
    public static class DataManager
    {
        public static ObservableCollection<Conductor> Conductores { get; set; } = new ObservableCollection<Conductor>();
        public static ObservableCollection<Ingreso> Ingresos { get; set; } = new ObservableCollection<Ingreso>();

        private static int nextConductorId = 1;
        private static int nextIngresoId = 1;

        // Métodos para Conductores
        public static void AgregarConductor(string nombre, string licencia, string transporte,
                                   string tipoDocumento = "DNI",
                                   string numeroDocumento = "",
                                   string telefono = "",
                                   string estado = "Activo")
        {
            Conductor conductor = new Conductor
            {
                Id = nextConductorId++,
                Nombre = nombre,
                Licencia = licencia,
                Transporte = transporte,
                TipoDocumento = tipoDocumento,
                NumeroDocumento = numeroDocumento,
                Telefono = telefono,
                Estado = estado
            };
            Conductores.Add(conductor);
        }

        public static void EliminarConductor(Conductor conductor)
        {
            Conductores.Remove(conductor);
        }

        // Métodos para Ingresos
        public static void AgregarIngreso(string tipoDoc, string numeroDoc, string placa, string turno,
                                         string nombreConductor, string nombreCliente, DateTime fechaHora, decimal peso)
        {
            Ingreso ingreso = new Ingreso
            {
                Id = nextIngresoId++,
                TipoDocumento = tipoDoc,
                NumeroDocumento = numeroDoc,
                Placa = placa,
                Turno = turno,
                NombreConductor = nombreConductor,
                NombreCliente = nombreCliente,
                FechaHora = fechaHora,
                PesoIngreso = peso
            };
            Ingresos.Add(ingreso);
        }

        // Filtrar ingresos
        public static List<Ingreso> FiltrarIngresos(DateTime? fechaInicio, DateTime? fechaFin,
                                                   string placa, string conductor, string producto)
        {
            List<Ingreso> ingresosFiltrados = new List<Ingreso>();

            foreach (var ingreso in Ingresos)
            {
                bool cumpleFechaInicio = !fechaInicio.HasValue || ingreso.FechaHora.Date >= fechaInicio.Value.Date;
                bool cumpleFechaFin = !fechaFin.HasValue || ingreso.FechaHora.Date <= fechaFin.Value.Date;
                bool cumplePlaca = string.IsNullOrEmpty(placa) || ingreso.Placa.ToLower().Contains(placa.ToLower());
                bool cumpleConductor = string.IsNullOrEmpty(conductor) || ingreso.NombreConductor.ToLower().Contains(conductor.ToLower());
                bool cumpleProducto = string.IsNullOrEmpty(producto) || ingreso.Producto.ToLower().Contains(producto.ToLower());

                if (cumpleFechaInicio && cumpleFechaFin && cumplePlaca && cumpleConductor && cumpleProducto)
                {
                    ingresosFiltrados.Add(ingreso);
                }
            }

            return ingresosFiltrados;
        }

        // Inicializar datos de ejemplo
        public static void InicializarDatosEjemplo()
        {
            // Agregar conductores de ejemplo
            AgregarConductor("Juan Pérez", "A1234567", "Transportes Lima SAC", "DNI", "12345678", "987654321", "Activo");
            AgregarConductor("María García", "B7654321", "Logística Norte EIRL", "RUC", "20123456789", "987654322", "Activo");
            AgregarConductor("Carlos Rodríguez", "C9876543", "Transporte Rápido SRL", "DNI", "87654321", "987654323", "Activo");
            AgregarConductor("Ana López", "D4567890", "Express Perú", "DNI", "34567890", "987654324", "Inactivo");
            AgregarConductor("Pedro Martínez", "E2345678", "Transportes del Sur", "RUC", "20456789012", "987654325", "Activo");
            AgregarConductor("Lucía Fernández", "F8765432", "Logística Andina", "DNI", "56789012", "987654326", "Inactivo");
            AgregarConductor("José Ramírez", "G3456789", "Transporte Norte SAC", "DNI", "67890123", "987654327", "Activo");
            AgregarConductor("Sofía Torres", "H9876541", "Transportes Global", "RUC", "20567890123", "987654328", "Inactivo");
            AgregarConductor("Miguel Ángel", "I2345679", "Transporte Rápido SRL", "DNI", "78901234", "987654329", "Activo");
            AgregarConductor("Carolina Rojas", "J7654322", "Logística Lima", "RUC", "20678901234", "987654330", "Activo");

            // Agregar ingresos de ejemplo
            AgregarIngreso("DNI", "12345678", "ABC-123", "Mañana", "Juan Pérez", "Cliente A", DateTime.Now.AddDays(-2), 1500.50m);
            AgregarIngreso("RUC", "20123456789", "XYZ-456", "Tarde", "María García", "Cliente B", DateTime.Now.AddDays(-1), 2300.75m);
            AgregarIngreso("DNI", "87654321", "DEF-789", "Noche", "Carlos Rodríguez", "Cliente C", DateTime.Now, 1800.25m);
        }
    }
}
