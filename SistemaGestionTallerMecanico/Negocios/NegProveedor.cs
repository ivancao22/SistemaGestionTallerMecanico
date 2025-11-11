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
    public class NegProveedor
    {
        private DaProveedor daProveedores = new DaProveedor();

        // Obtener el siguiente código disponible
        public string ObtenerSiguienteCodigo()
        {
            return daProveedores.ObtenerSiguienteCodigo();
        }

        // Cargar todos los proveedores
        public DataTable CargarProveedores()
        {
            return daProveedores.CargarProveedores();
        }

        // Obtener un proveedor por código
        public Proveedor ObtenerProveedorPorCodigo(string codProveedor)
        {
            if (string.IsNullOrWhiteSpace(codProveedor))
                return null;

            return daProveedores.ObtenerProveedorPorCodigo(codProveedor);
        }

        // Agregar un nuevo proveedor
        public bool AgregarProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
                return false;

            if (string.IsNullOrWhiteSpace(proveedor.CodProveedor))
                return false;

            // VALIDACIÓN ESPECIAL: Al menos uno de los 3 debe tener valor
            bool tieneNombre = !string.IsNullOrWhiteSpace(proveedor.Nombre);
            bool tieneApellido = !string.IsNullOrWhiteSpace(proveedor.Apellido);
            bool tieneRazonSocial = !string.IsNullOrWhiteSpace(proveedor.RazonSocial);

            if (!tieneNombre && !tieneApellido && !tieneRazonSocial)
                return false;

            return daProveedores.InsertarProveedor(proveedor);
        }

        // Actualizar un proveedor existente
        public bool ActualizarProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
                return false;

            if (string.IsNullOrWhiteSpace(proveedor.CodProveedor))
                return false;

            // VALIDACIÓN ESPECIAL: Al menos uno de los 3 debe tener valor
            bool tieneNombre = !string.IsNullOrWhiteSpace(proveedor.Nombre);
            bool tieneApellido = !string.IsNullOrWhiteSpace(proveedor.Apellido);
            bool tieneRazonSocial = !string.IsNullOrWhiteSpace(proveedor.RazonSocial);

            if (!tieneNombre && !tieneApellido && !tieneRazonSocial)
                return false;

            return daProveedores.ActualizarProveedor(proveedor);
        }

        // Borrar un proveedor
        public bool BorrarProveedor(string codProveedor)
        {
            if (string.IsNullOrWhiteSpace(codProveedor))
                return false;

            return daProveedores.BorrarProveedor(codProveedor);
        }
    }
}
