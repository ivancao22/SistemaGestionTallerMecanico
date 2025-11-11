using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocios
{
    public class NegCajaDiaria
    {
        private readonly DaCajaDiaria daCajaDiaria;

        public NegCajaDiaria()
        {
            this.daCajaDiaria = new DaCajaDiaria();
        }

        // =====================================================
        // OBTENER CAJA POR FECHA
        // =====================================================

        public async Task<CajaDiaria> ObtenerPorFechaAsync(DateTime fecha)
        {
            return await daCajaDiaria.ObtenerPorFechaAsync(fecha);
        }

        // =====================================================
        // OBTENER SALDO FINAL DEL DÍA ANTERIOR
        // =====================================================

        public async Task<decimal?> ObtenerSaldoDiaAnteriorAsync(DateTime fecha)
        {
            return await daCajaDiaria.ObtenerSaldoFinalDiaAnteriorAsync(fecha);
        }

        // =====================================================
        // ABRIR CAJA (INSERTAR) - Con validaciones
        // =====================================================

        public async Task<int> AbrirCajaAsync(DateTime fecha, decimal saldoInicial, string usuario = null)
        {
            // ✅ VALIDACIONES (esto sí va en Negocio)

            if (fecha.Date > DateTime.Today)
            {
                throw new Exception("No se puede abrir caja de fecha futura");
            }

            if (saldoInicial < 0)
            {
                throw new Exception("El saldo inicial no puede ser negativo");
            }

            bool existeCaja = await daCajaDiaria.ExisteCajaEnFechaAsync(fecha);
            if (existeCaja)
            {
                throw new Exception($"Ya existe una caja para el día {fecha:dd/MM/yyyy}");
            }

            // ✅ EJECUTAR
            var caja = new CajaDiaria(fecha, saldoInicial, usuario);
            return await daCajaDiaria.InsertarAsync(caja);
        }

        // =====================================================
        // MODIFICAR SALDO INICIAL - Con validaciones
        // =====================================================

        public async Task<bool> ModificarSaldoInicialAsync(int idCajaDiaria, decimal nuevoSaldoInicial)
        {
            // ✅ VALIDACIONES

            if (nuevoSaldoInicial < 0)
            {
                throw new Exception("El saldo inicial no puede ser negativo");
            }

            int cantidadMovimientos = await daCajaDiaria.ContarMovimientosAsync(idCajaDiaria);
            if (cantidadMovimientos > 0)
            {
                throw new Exception($"La caja ya tiene {cantidadMovimientos} movimiento(s) registrado(s). No se puede modificar el saldo inicial.");
            }

            // ✅ EJECUTAR
            return await daCajaDiaria.ModificarSaldoInicialAsync(idCajaDiaria, nuevoSaldoInicial);
        }

        // =====================================================
        // ACTUALIZAR SALDO FINAL (ejecutar SP)
        // =====================================================

        public async Task<decimal> ActualizarSaldoFinalAsync(int idCajaDiaria)
        {
            return await daCajaDiaria.ActualizarSaldoFinalAsync(idCajaDiaria);
        }

        // =====================================================
        // VERIFICAR SI SE PUEDE MODIFICAR SALDO INICIAL
        // =====================================================

        public async Task<bool> PuedeModificarSaldoInicialAsync(int idCajaDiaria)
        {
            int cantidadMovimientos = await daCajaDiaria.ContarMovimientosAsync(idCajaDiaria);
            return cantidadMovimientos == 0;
        }

        // =====================================================
        // CONTAR MOVIMIENTOS
        // =====================================================

        public async Task<int> ContarMovimientosAsync(int idCajaDiaria)
        {
            return await daCajaDiaria.ContarMovimientosAsync(idCajaDiaria);
        }
    }
}
