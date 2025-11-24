using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocios
{
    public class NegLibroDiario
    {
        private readonly DaLibroDiario daLibroDiario;
        private readonly DaCajaDiaria daCajaDiaria;

        public NegLibroDiario()
        {
            this.daLibroDiario = new DaLibroDiario();
            this.daCajaDiaria = new DaCajaDiaria();
        }

        // =====================================================
        // LISTAR MOVIMIENTOS POR CAJA
        // =====================================================

        public async Task<List<LibroDiario>> ListarPorCajaAsync(int idCajaDiaria, string filtroNombre = null)
        {
            return await daLibroDiario.ListarPorCajaAsync(idCajaDiaria, filtroNombre);
        }

        // =====================================================
        // OBTENER MOVIMIENTO POR CÓDIGO
        // =====================================================

        public async Task<LibroDiario> ObtenerPorCodigoAsync(string codMovimiento)
        {
            return await daLibroDiario.ObtenerPorCodigoAsync(codMovimiento);
        }

        // =====================================================
        // AGREGAR MOVIMIENTO (con validaciones y actualización de saldo)
        // =====================================================

        public async Task<string> AgregarMovimientoAsync(LibroDiario movimiento)
        {
            // ✅ VALIDACIONES

            if (movimiento.IdCajaDiaria <= 0)
            {
                throw new Exception("La caja diaria no es válida");
            }

            if (string.IsNullOrWhiteSpace(movimiento.TipoMovimiento))
            {
                throw new Exception("Debe seleccionar el tipo de movimiento");
            }

            if (movimiento.TipoMovimiento != "INGRESO" && movimiento.TipoMovimiento != "EGRESO")
            {
                throw new Exception("El tipo de movimiento debe ser INGRESO o EGRESO");
            }

            if (string.IsNullOrWhiteSpace(movimiento.MetodoPago))
            {
                throw new Exception("Debe seleccionar el método de pago");
            }

            if (string.IsNullOrWhiteSpace(movimiento.Concepto))
            {
                throw new Exception("Debe ingresar un concepto");
            }

            if (movimiento.Monto <= 0)
            {
                throw new Exception("El monto debe ser mayor a cero");
            }

            // ✅ EJECUTAR
            string codMovimiento = await daLibroDiario.InsertarAsync(movimiento);

            // ✅ ACTUALIZAR SALDO SI ES EFECTIVO
            if (movimiento.MetodoPago == "EFECTIVO")
            {
                await daCajaDiaria.ActualizarSaldoFinalAsync(movimiento.IdCajaDiaria);
            }

            return codMovimiento;
        }

        // =====================================================
        // MODIFICAR MOVIMIENTO (con validaciones y actualización de saldo)
        // =====================================================

        public async Task<bool> ModificarMovimientoAsync(LibroDiario movimiento)
        {
            // ✅ VALIDACIONES

            if (string.IsNullOrWhiteSpace(movimiento.CodMovimiento))
            {
                throw new Exception("El código del movimiento no es válido");
            }

            if (string.IsNullOrWhiteSpace(movimiento.Concepto))
            {
                throw new Exception("Debe ingresar un concepto");
            }

            if (movimiento.Monto <= 0)
            {
                throw new Exception("El monto debe ser mayor a cero");
            }

            // Obtener el método de pago anterior
            string metodoPagoAnterior = await daLibroDiario.ObtenerMetodoPagoAsync(movimiento.CodMovimiento);

            if (string.IsNullOrEmpty(metodoPagoAnterior))
            {
                throw new Exception("No se encontró el movimiento");
            }

            // ✅ EJECUTAR
            bool exito = await daLibroDiario.ActualizarAsync(movimiento);

            // ✅ ACTUALIZAR SALDO SI EL MÉTODO ANTERIOR O ACTUAL ES EFECTIVO
            if (metodoPagoAnterior == "EFECTIVO" || movimiento.MetodoPago == "EFECTIVO")
            {
                await daCajaDiaria.ActualizarSaldoFinalAsync(movimiento.IdCajaDiaria);
            }

            return exito;
        }

        // =====================================================
        // ELIMINAR MOVIMIENTO (con actualización de saldo)
        // =====================================================

        public async Task<bool> EliminarMovimientoAsync(string codMovimiento, string codOrdenTrabajo)
        {
            // ✅ VALIDACIONES

            if (string.IsNullOrWhiteSpace(codMovimiento))
            {
                throw new Exception("El código del movimiento no es válido");
            }

            // Obtener el movimiento antes de eliminarlo
            var movimiento = await daLibroDiario.ObtenerPorCodigoAsync(codMovimiento);

            if (movimiento == null)
            {
                throw new Exception("No se encontró el movimiento");
            }

            if (!movimiento.Activo)
            {
                throw new Exception("El movimiento ya fue eliminado");
            }

            // ✅ EJECUTAR
            bool exito = await daLibroDiario.EliminarAsync(codMovimiento, codOrdenTrabajo);

            // ✅ ACTUALIZAR SALDO SI ERA EFECTIVO
            if (movimiento.MetodoPago == "EFECTIVO")
            {
                await daCajaDiaria.ActualizarSaldoFinalAsync(movimiento.IdCajaDiaria);
            }

            return exito;
        }
    }
}
