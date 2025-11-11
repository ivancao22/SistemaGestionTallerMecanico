using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Entidades;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Utiles
{
    public class PdfHelper
    {
        public static string GenerarPdfOrdenTrabajo(OrdenTrabajo orden, string rutaSalida)
        {
            if (orden == null) throw new ArgumentNullException(nameof(orden));
            if (string.IsNullOrWhiteSpace(rutaSalida)) throw new ArgumentNullException(nameof(rutaSalida));

            var carpeta = Path.GetDirectoryName(rutaSalida);
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            if (File.Exists(rutaSalida))
                File.Delete(rutaSalida);

            using (var fs = new FileStream(rutaSalida, FileMode.Create, FileAccess.Write))
            {
                var doc = new Document(PageSize.A4, 36, 36, 36, 36);
                var writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                var bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                var fontHeader = new Font(bf, 9, Font.NORMAL);
                var fontBold = new Font(bf, 9, Font.BOLD);

                // Cabecera
                var tblCab = new PdfPTable(new float[] { 1f, 2f }) { WidthPercentage = 100 };
                var celLogo = new PdfPCell() { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_TOP };
                var pLogo = new Paragraph();
                pLogo.Add(new Chunk("C  D  M\n", new Font(bf, 20, Font.BOLD)));
                pLogo.Add(new Chunk("Carlos De Marte\nServicio Integral del Automóvil\nIng. White 5398 esq. G Méndez\nCarapachay\nTel: 11-5-691-6968\n", fontHeader));
                celLogo.AddElement(pLogo);
                tblCab.AddCell(celLogo);

                var celInfo = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_TOP };
                var pInfo = new Paragraph();
                celInfo.AddElement(pInfo);
                tblCab.AddCell(celInfo);

                doc.Add(tblCab);
                doc.Add(new Paragraph("\n"));

                // ============================================
                // TABLA: CLIENTE / FECHA / VEHÍCULO / PAT / KM
                // ============================================
                // ============================================
                // TABLA: CLIENTE / FECHA / VEHÍCULO / PAT / KM
                // ============================================
                var tblDatos = new PdfPTable(new float[] { 6f, 2.5f, 1.5f }) { WidthPercentage = 100, SpacingBefore = 4, SpacingAfter = 6 };

                // FILA 1: CLIENTE (ocupa 2 columnas) | FECHA (1 columna)
                var phraseCliente = new Phrase();
                phraseCliente.Add(new Chunk("CLIENTE: ", fontBold)); // ✅ Negrita
                phraseCliente.Add(new Chunk(orden.NombreCliente ?? "", fontHeader)); // ✅ Normal

                tblDatos.AddCell(new PdfPCell(phraseCliente)
                {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 4,
                    Border = Rectangle.BOX
                });

                var phraseFecha = new Phrase();
                phraseFecha.Add(new Chunk("FECHA: ", fontBold)); // ✅ Negrita
                phraseFecha.Add(new Chunk($"{orden.Fecha:dd/MM/yyyy}", fontHeader)); // ✅ Normal

                tblDatos.AddCell(new PdfPCell(phraseFecha)
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 4,
                    Border = Rectangle.BOX
                });

                // FILA 2: VEHÍCULO | PAT | KM
                var phraseVehiculo = new Phrase();
                phraseVehiculo.Add(new Chunk("VEHICULO: ", fontBold)); // ✅ Negrita
                phraseVehiculo.Add(new Chunk(orden.ModeloVehiculo ?? "", fontHeader)); // ✅ Normal

                tblDatos.AddCell(new PdfPCell(phraseVehiculo)
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 4,
                    Border = Rectangle.BOX
                });

                var phrasePat = new Phrase();
                phrasePat.Add(new Chunk("PAT: ", fontBold)); // ✅ Negrita
                phrasePat.Add(new Chunk(orden.PatenteVehiculo ?? "", fontHeader)); // ✅ Normal

                tblDatos.AddCell(new PdfPCell(phrasePat)
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 4,
                    Border = Rectangle.BOX
                });

                var phraseKm = new Phrase();
                phraseKm.Add(new Chunk("KM: ", fontBold)); // ✅ Negrita
                phraseKm.Add(new Chunk(orden.Kilometraje?.ToString("N0") ?? "", fontHeader)); // ✅ Normal

                tblDatos.AddCell(new PdfPCell(phraseKm)
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 4,
                    Border = Rectangle.BOX
                });

                doc.Add(tblDatos);
                // ============================================
                // DETALLE DEL TRABAJO (con recuadro)
                // ============================================
                if (!string.IsNullOrWhiteSpace(orden.DescripcionTrabajo))
                {
                    var tblDetalle = new PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 6, SpacingAfter = 6 };

                    // Celda de título
                    tblDetalle.AddCell(new PdfPCell(new Phrase("DETALLE DEL TRABAJO:", fontBold))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Padding = 4,
                        Border = Rectangle.BOX,
                        BackgroundColor = new BaseColor(240, 240, 240) // Fondo gris claro opcional
                    });

                    // Celda de contenido
                    tblDetalle.AddCell(new PdfPCell(new Phrase(orden.DescripcionTrabajo, fontHeader))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Padding = 6,
                        Border = Rectangle.BOX,
                        MinimumHeight = 40f // Altura mínima para que se vea bien
                    });

                    doc.Add(tblDetalle);
                }

                // Items
                var tblItems = new PdfPTable(new float[] { 8f, 2f }) { WidthPercentage = 100, SpacingBefore = 6, HeaderRows = 1 };
                tblItems.AddCell(new PdfPCell(new Phrase("DETALLE", fontBold)) { HorizontalAlignment = Element.ALIGN_LEFT, Padding = 6 });
                tblItems.AddCell(new PdfPCell(new Phrase("PRECIO", fontBold)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 6 }); 

                int maxFilas = Math.Max(orden.Detalles?.Count ?? 0, 12);
                for (int i = 0; i < maxFilas; i++)
                {
                    if (orden.Detalles != null && i < orden.Detalles.Count)
                    {
                        var d = orden.Detalles[i];
                        tblItems.AddCell(new PdfPCell(new Phrase(d.Descripcion ?? "-", fontHeader)) { Padding = 6 });
                        tblItems.AddCell(new PdfPCell(new Phrase("$" + d.Precio.ToString("N2"), fontHeader)) { Padding = 6, HorizontalAlignment = Element.ALIGN_RIGHT }); // CAMBIO: agregado "$"
                    }
                    else
                    {
                        tblItems.AddCell(new PdfPCell(new Phrase(" ", fontHeader)) { Padding = 6 });
                        tblItems.AddCell(new PdfPCell(new Phrase(" ", fontHeader)) { Padding = 6 });
                    }
                }
                doc.Add(tblItems);

                // Totales
                doc.Add(new Paragraph("\n"));
                var tblTot = new PdfPTable(new float[] { 6f, 2f }) { WidthPercentage = 100 };
                tblTot.AddCell(new PdfPCell(new Phrase("TOTAL REPUESTOS:", fontBold)) { Border = Rectangle.NO_BORDER, Padding = 6, HorizontalAlignment = Element.ALIGN_LEFT });
                tblTot.AddCell(new PdfPCell(new Phrase("$" + orden.TotalRepuestos.ToString("N2"), fontBold)) { Border = Rectangle.NO_BORDER, Padding = 6, HorizontalAlignment = Element.ALIGN_RIGHT }); // CAMBIO: agregado "$"

                tblTot.AddCell(new PdfPCell(new Phrase("TOTAL MANO DE OBRA:", fontBold)) { Border = Rectangle.NO_BORDER, Padding = 6, HorizontalAlignment = Element.ALIGN_LEFT });
                tblTot.AddCell(new PdfPCell(new Phrase("$" + orden.TotalManoObra.ToString("N2"), fontBold)) { Border = Rectangle.NO_BORDER, Padding = 6, HorizontalAlignment = Element.ALIGN_RIGHT }); // CAMBIO: agregado "$"

                tblTot.AddCell(new PdfPCell(new Phrase(" ", fontBold)) { Border = Rectangle.NO_BORDER, Padding = 4 });
                tblTot.AddCell(new PdfPCell(new Phrase(" ", fontBold)) { Border = Rectangle.NO_BORDER, Padding = 4 });

                tblTot.AddCell(new PdfPCell(new Phrase("TOTAL GENERAL:", fontBold)) { Border = Rectangle.NO_BORDER, Padding = 6, HorizontalAlignment = Element.ALIGN_LEFT });
                tblTot.AddCell(new PdfPCell(new Phrase("$" + orden.TotalGeneral.ToString("N2"), fontBold)) { Border = Rectangle.NO_BORDER, Padding = 6, HorizontalAlignment = Element.ALIGN_RIGHT }); // CAMBIO: agregado "$"

                doc.Add(tblTot);

                doc.Close();
                writer.Close();
                fs.Close();
            }

            return rutaSalida;
        }

        public static void AbrirPdf(string ruta)
        {
            if (File.Exists(ruta))
            {
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            else
            {
                throw new FileNotFoundException("No se encontró el archivo PDF.", ruta);
            }
        }
    }
}
