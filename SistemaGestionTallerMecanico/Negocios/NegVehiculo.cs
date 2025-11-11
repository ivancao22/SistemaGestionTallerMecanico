using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocios
{
    public class NegVehiculo
    {
        private DaVehiculo daVehiculos = new DaVehiculo();

        // Obtener el siguiente código disponible
        public string ObtenerSiguienteCodigo()
        {
            return daVehiculos.ObtenerSiguienteCodigo();
        }

        // Cargar todos los vehículos
        public DataTable CargarVehiculos()
        {
            return daVehiculos.CargarVehiculos();
        }

        // Obtener un vehículo por código
        public Vehiculo ObtenerVehiculoPorCodigo(string codVehiculo)
        {
            if (string.IsNullOrWhiteSpace(codVehiculo))
                return null;

            return daVehiculos.ObtenerVehiculoPorCodigo(codVehiculo);
        }

        // Agregar un nuevo vehículo
        public bool AgregarVehiculo(Vehiculo vehiculo)
        {
            // Validar que el objeto no sea nulo
            if (vehiculo == null)
                return false;

            // Validar código del vehículo
            if (string.IsNullOrWhiteSpace(vehiculo.CodVehiculo))
                return false;

            // Validar código del cliente (FK)
            if (string.IsNullOrWhiteSpace(vehiculo.CodCliente))
                return false;

            // Validar modelo (obligatorio)
            if (string.IsNullOrWhiteSpace(vehiculo.Modelo))
                return false;

            // Validar patente (obligatorio)
            if (string.IsNullOrWhiteSpace(vehiculo.Patente))
                return false;

            // Validar kilometraje (si tiene valor, debe ser >= 0)
            if (vehiculo.Kilometraje.HasValue && vehiculo.Kilometraje.Value < 0)
                return false;

            return daVehiculos.InsertarVehiculo(vehiculo);
        }

        // Actualizar un vehículo existente
        public bool ActualizarVehiculo(Vehiculo vehiculo)
        {
            // Validar que el objeto no sea nulo
            if (vehiculo == null)
                return false;

            // Validar código del vehículo
            if (string.IsNullOrWhiteSpace(vehiculo.CodVehiculo))
                return false;

            // Validar código del cliente (FK)
            if (string.IsNullOrWhiteSpace(vehiculo.CodCliente))
                return false;

            // Validar modelo (obligatorio)
            if (string.IsNullOrWhiteSpace(vehiculo.Modelo))
                return false;

            // Validar patente (obligatorio)
            if (string.IsNullOrWhiteSpace(vehiculo.Patente))
                return false;

            // Validar kilometraje (si tiene valor, debe ser >= 0)
            if (vehiculo.Kilometraje.HasValue && vehiculo.Kilometraje.Value < 0)
                return false;

            return daVehiculos.ActualizarVehiculo(vehiculo);
        }

        // Borrar un vehículo
        public bool BorrarVehiculo(string codVehiculo)
        {
            if (string.IsNullOrWhiteSpace(codVehiculo))
                return false;

            return daVehiculos.BorrarVehiculo(codVehiculo);
        }

        // Cargar clientes activos para ComboBox
        public DataTable CargarClientesActivos()
        {
            return daVehiculos.CargarClientesActivos();
        }
    }
}
