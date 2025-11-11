using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utiles
{
    public class LogHelper
    {
        private static readonly object lockObj = new object();

        // Obtener la carpeta raíz del proyecto (donde está el ejecutable)
        private static string ObtenerCarpetaRaiz()
        {
            string carpetaEjecutable = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return carpetaEjecutable;
        }

        // =====================================================
        // GUARDAR ERROR (versión original)
        // =====================================================

        public static void GuardarError(Exception ex, string origen, string metodo = "")
        {
            try
            {
                string carpetaRaiz = ObtenerCarpetaRaiz();
                string carpetaErrores = Path.Combine(carpetaRaiz, "errores");
                string archivoErrores = Path.Combine(carpetaErrores, "errores.txt");

                if (!Directory.Exists(carpetaErrores))
                    Directory.CreateDirectory(carpetaErrores);

                string mensaje = FormatearMensajeError(ex, origen, metodo, null);

                lock (lockObj)
                {
                    File.AppendAllText(archivoErrores, mensaje + Environment.NewLine);
                }
            }
            catch
            {
                // Si falla el guardado del log, no hacer nada
            }
        }

        // =====================================================
        // GUARDAR ERROR (con datos adicionales) ← SOBRECARGA NUEVA
        // =====================================================

        public static void GuardarError(Exception ex, string origen, string metodo, string datosAdicionales)
        {
            try
            {
                string carpetaRaiz = ObtenerCarpetaRaiz();
                string carpetaErrores = Path.Combine(carpetaRaiz, "errores");
                string archivoErrores = Path.Combine(carpetaErrores, "errores.txt");

                if (!Directory.Exists(carpetaErrores))
                    Directory.CreateDirectory(carpetaErrores);

                string mensaje = FormatearMensajeError(ex, origen, metodo, datosAdicionales);

                lock (lockObj)
                {
                    File.AppendAllText(archivoErrores, mensaje + Environment.NewLine);
                }
            }
            catch
            {
                // Si falla el guardado del log, no hacer nada
            }
        }

        // =====================================================
        // GUARDAR INFO (versión original)
        // =====================================================

        public static void GuardarInfo(string mensaje, string origen)
        {
            try
            {
                string carpetaRaiz = ObtenerCarpetaRaiz();
                string carpetaErrores = Path.Combine(carpetaRaiz, "errores");
                string archivoInfo = Path.Combine(carpetaErrores, "info.txt");

                if (!Directory.Exists(carpetaErrores))
                    Directory.CreateDirectory(carpetaErrores);

                string mensajeFormateado = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {origen} | {mensaje}";

                lock (lockObj)
                {
                    File.AppendAllText(archivoInfo, mensajeFormateado + Environment.NewLine);
                }
            }
            catch
            {
                // Si falla el guardado del log, no hacer nada
            }
        }

        // =====================================================
        // GUARDAR INFO (con origen y método) ← SOBRECARGA NUEVA
        // =====================================================

        public static void GuardarInfo(string mensaje, string origen, string metodo)
        {
            try
            {
                string carpetaRaiz = ObtenerCarpetaRaiz();
                string carpetaErrores = Path.Combine(carpetaRaiz, "errores");
                string archivoInfo = Path.Combine(carpetaErrores, "info.txt");

                if (!Directory.Exists(carpetaErrores))
                    Directory.CreateDirectory(carpetaErrores);

                string mensajeFormateado = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {origen}.{metodo} | {mensaje}";

                lock (lockObj)
                {
                    File.AppendAllText(archivoInfo, mensajeFormateado + Environment.NewLine);
                }
            }
            catch
            {
                // Si falla el guardado del log, no hacer nada
            }
        }

        // =====================================================
        // FORMATEAR MENSAJE DE ERROR (modificado para incluir datos adicionales)
        // =====================================================

        private static string FormatearMensajeError(Exception ex, string origen, string metodo, string datosAdicionales)
        {
            string separador = new string('-', 80);

            string mensaje = $@"
{separador}
FECHA/HORA: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
USUARIO: {Environment.UserName}
ORIGEN: {origen}
MÉTODO: {metodo}";

            // ✅ Agregar datos adicionales si existen
            if (!string.IsNullOrEmpty(datosAdicionales))
            {
                mensaje += $@"
DATOS ADICIONALES: {datosAdicionales}";
            }

            mensaje += $@"
TIPO EXCEPCIÓN: {ex.GetType().Name}
MENSAJE: {ex.Message}
STACK TRACE:
{ex.StackTrace}";

            // Agregar inner exception si existe
            if (ex.InnerException != null)
            {
                mensaje += $@"
INNER EXCEPTION: {ex.InnerException.Message}";
            }

            mensaje += $@"
{separador}";

            return mensaje;
        }

        // =====================================================
        // LIMPIAR LOGS ANTIGUOS (opcional - útil para no acumular archivos enormes)
        // =====================================================

        public static void LimpiarLogsAntiguos(int diasAMantener = 30)
        {
            try
            {
                string carpetaRaiz = ObtenerCarpetaRaiz();
                string carpetaErrores = Path.Combine(carpetaRaiz, "errores");

                if (!Directory.Exists(carpetaErrores))
                    return;

                // Obtener todos los archivos .txt de la carpeta
                string[] archivos = Directory.GetFiles(carpetaErrores, "*.txt");

                foreach (string archivo in archivos)
                {
                    FileInfo fileInfo = new FileInfo(archivo);

                    // Si el archivo tiene más de X días, eliminarlo
                    if ((DateTime.Now - fileInfo.LastWriteTime).TotalDays > diasAMantener)
                    {
                        File.Delete(archivo);
                    }
                }
            }
            catch
            {
                // Si falla la limpieza, no hacer nada
            }
        }
    }
}

