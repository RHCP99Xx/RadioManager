using RadioManager.Models.DAO;
using RadioManager.Models.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using ObjWord = Microsoft.Office.Interop.Word;

namespace RadioManager.GUI
{
    /// <summary>
    /// Interaction logic for ReporteCancionesSinUsar.xaml
    /// </summary>
    public partial class ReporteCancionesSinUsar : Window
    {
        List<LineaPatronReporte> lineasPatronReporte;
        public ReporteCancionesSinUsar()
        {
            InitializeComponent();

            LineaPatronDAO lineaPatronDAO = new LineaPatronDAO();
            
            lineasPatronReporte = lineaPatronDAO.getLineasPatronSinUsar();
            lvLineas.ItemsSource = lineasPatronReporte;

            tbNumCanciones.Text = lineasPatronReporte.Count.ToString();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            ObjWord.Application aplicacion = new ObjWord.Application();
            ObjWord.Document document = new ObjWord.Document();

            ObjWord.Paragraph titulo = document.Content.Paragraphs.Add(Type.Missing);
            titulo.Range.Text = "Reporte De Canciones Sin Usar";
            titulo.Range.Font.Size = 20;
            titulo.Range.InsertParagraphAfter();

            ObjWord.Paragraph fecha = document.Content.Paragraphs.Add(Type.Missing);
            fecha.Range.Text = "Fecha: " + date;
            fecha.Range.Font.Size = 12;
            fecha.Range.InsertParagraphAfter();

            ObjWord.Paragraph numCanciones = document.Content.Paragraphs.Add(Type.Missing);
            numCanciones.Range.Text = "Número de canciones: " + lineasPatronReporte.Count;
            numCanciones.Range.Font.Size = 12;
            numCanciones.Range.InsertParagraphAfter();

            Object oMissing = System.Reflection.Missing.Value;
            ObjWord.Table tablaCanciones = document.Content.Tables.Add(document.Range(0, 0), (lineasPatronReporte.Count + 1), 3, ref oMissing, ref oMissing);
            tablaCanciones.Cell(1, 1).Range.Text = "Categoría";
            tablaCanciones.Cell(1, 1).Range.Font.Size = 18;
            tablaCanciones.Cell(1, 2).Range.Text = "Género";
            tablaCanciones.Cell(1, 2).Range.Font.Size = 18;
            tablaCanciones.Cell(1, 3).Range.Text = "Número";
            tablaCanciones.Cell(1, 3).Range.Font.Size = 18;

            int posicion = 2;
            foreach (LineaPatronReporte linea in lineasPatronReporte)
            {
                tablaCanciones.Cell(posicion, 1).Range.Text =  linea.Categoria.NombreCategoria;
                tablaCanciones.Cell(posicion, 1).Range.Font.Size = 12;
                tablaCanciones.Cell(posicion, 2).Range.Text = linea.Genero.NombreGenero;
                tablaCanciones.Cell(posicion, 2).Range.Font.Size = 12;
                tablaCanciones.Cell(posicion, 3).Range.Text = linea.NumCanciones.ToString();
                tablaCanciones.Cell(posicion, 3).Range.Font.Size = 12;
                posicion++;
            }
            tablaCanciones.Range.InsertParagraphAfter();

            date = date.Replace(":", "-");
            document.SaveAs2(ruta + "\\CancionesSinUsar " + date + ".docx");
            document.Close();
            aplicacion.Quit();

            MessageBox.Show("Reporte guardado");
        }

    }
}
