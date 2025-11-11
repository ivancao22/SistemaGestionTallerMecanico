using System.Data;
using Datos;
using Entidades;

namespace Negocio
{
    public class NegClientes
    {
        private DaCliente daClientes = new DaCliente();

        // Obtener el siguiente código disponible
        public string ObtenerSiguienteCodigo()
        {
            string codigo = daClientes.ObtenerSiguienteCodigo();

            // Si devuelve null, significa que hubo un error de conexión o BD
            if (codigo == null)
                return null;  // Propagar el error

            return codigo;
        }

        // Cargar todos los clientes
        public DataTable CargarClientes()
        {
            return daClientes.CargarClientes();
        }

        // Obtener un cliente por código (para editar)
        public Cliente ObtenerClientePorCodigo(string codCliente)
        {
            // Validación de negocio: código no puede estar vacío
            if (string.IsNullOrWhiteSpace(codCliente))
                return null;

            return daClientes.ObtenerClientePorCodigo(codCliente);
        }

        // Agregar un nuevo cliente
        public bool AgregarCliente(Cliente cliente)
        {
            // Validaciones de REGLAS DE NEGOCIO
            // (Las validaciones de formato se hacen en el formulario)

            if (cliente == null)
                return false;

            if (string.IsNullOrWhiteSpace(cliente.CodCliente))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
                return false;

            return daClientes.InsertarCliente(cliente);
        }

        // Actualizar un cliente existente
        public bool ActualizarCliente(Cliente cliente)
        {
            // Validaciones de REGLAS DE NEGOCIO

            if (cliente == null)
                return false;

            if (string.IsNullOrWhiteSpace(cliente.CodCliente))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                return false;

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
                return false;

            return daClientes.ActualizarCliente(cliente);
        }

        // Borrar un cliente
        public bool BorrarCliente(string codCliente)
        {
            // Validación de negocio: código no puede estar vacío
            if (string.IsNullOrWhiteSpace(codCliente))
                return false;

            return daClientes.BorrarCliente(codCliente);
        }
    }
}