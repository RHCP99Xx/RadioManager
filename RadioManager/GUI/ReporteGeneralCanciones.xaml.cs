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
    /// Interaction logic for ReporteGeneralCanciones.xaml
    /// </summary>
    public partial class ReporteGeneralCanciones : Window
    {
        List<Genero> generos;
        List<Categoria> categorias;
        List<Artista> artistas;
        List<Cancion> canciones = new List<Cancion>();

        public ReporteGeneralCanciones()
        {
            InitializeComponent();
            cargarGeneros();
            cargarCategorias();
            cargarCantantes();
        }

        private void cargarGeneros()
        {
            GeneroDAO generoDAO = new GeneroDAO();
            generos = generoDAO.getGeneros();
            generos.RemoveAt(0);
            cbGenero.DisplayMemberPath = "NombreGenero";
            cbGenero.ItemsSource = generos;
        }

        private void cargarCategorias()
        {
            CategoriaDAO CategoriaDAO = new CategoriaDAO();
            categorias = CategoriaDAO.getCategorias();
            categorias.RemoveAt(0);
            cbCategoria.DisplayMemberPath = "NombreCategoria";
            cbCategoria.ItemsSource = categorias;
        }

        private void cargarCantantes()
        {
            ArtistaDAO artistaDAO = new ArtistaDAO();
            artistas = artistaDAO.getArtistasSinImagen();
            artistas.RemoveAt(0);
            cbArtista.DisplayMemberPath = "NombreArtistico";
            cbArtista.ItemsSource = artistas;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cbArtista.SelectedItem != null && cbCategoria.SelectedItem != null && cbGenero.SelectedItem != null)
            {
                CancionDAO cancionDAO = new CancionDAO();
                canciones = cancionDAO.getCancionesReporte((Artista)cbArtista.SelectedItem, (Categoria)cbCategoria.SelectedItem, (Genero)cbGenero.SelectedItem);
                lvCanciones.ItemsSource = canciones;
                if (canciones.Count > 0)
                {
                    crearReporte();
                    MessageBox.Show("Reporte guardado");
                }
                else
                {
                    MessageBox.Show("No hay canciones");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cantante, una categoría y un género");
            }
        }


        private void crearReporte()
        {
            string date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            ObjWord.Application aplicacion = new ObjWord.Application();
            ObjWord.Document document = new ObjWord.Document();

            ObjWord.Paragraph titulo = document.Content.Paragraphs.Add(Type.Missing);
            titulo.Range.Text = "Reporte De Canciones";
            titulo.Range.Font.Size = 20;
            titulo.Range.InsertParagraphAfter();

            ObjWord.Paragraph fecha = document.Content.Paragraphs.Add(Type.Missing);
            fecha.Range.Text = "Fecha: " + date;
            fecha.Range.Font.Size = 12;
            fecha.Range.InsertParagraphAfter();

            ObjWord.Paragraph datos = document.Content.Paragraphs.Add(Type.Missing);
            String datosFiltro = "Cantante: " + ((Artista)cbArtista.SelectedItem).NombreArtistico +
                                 "\nCategoría: " + ((Categoria)cbCategoria.SelectedItem).NombreCategoria +
                                 "\nGénero: " + ((Genero)cbGenero.SelectedItem).NombreGenero;
            datos.Range.Text = datosFiltro;
            datos.Range.Font.Size = 12;
            datos.Range.InsertParagraphAfter();


            Object oMissing = System.Reflection.Missing.Value;
            ObjWord.Table tablaCanciones = document.Content.Tables.Add(document.Range(0, 0), (canciones.Count + 1), 3, ref oMissing, ref oMissing);
            tablaCanciones.Cell(1, 1).Range.Text = "Clave";
            tablaCanciones.Cell(1, 1).Range.Font.Size = 18;
            tablaCanciones.Cell(1, 2).Range.Text = "Título";
            tablaCanciones.Cell(1, 2).Range.Font.Size = 18;
            tablaCanciones.Cell(1, 3).Range.Text = "Días";
            tablaCanciones.Cell(1, 3).Range.Font.Size = 18;

            int posicion = 2;
            foreach (Cancion cancionActual in canciones)
            {
                tablaCanciones.Cell(posicion, 1).Range.Text = cancionActual.Clave;
                tablaCanciones.Cell(posicion, 1).Range.Font.Size = 12;
                tablaCanciones.Cell(posicion, 2).Range.Text = cancionActual.Titulo;
                tablaCanciones.Cell(posicion, 2).Range.Font.Size = 12;
                tablaCanciones.Cell(posicion, 3).Range.Text = cancionActual.Dias;
                tablaCanciones.Cell(posicion, 3).Range.Font.Size = 12;
                posicion++;
            }
            tablaCanciones.Range.InsertParagraphAfter();

            date = date.Replace(":", "-");
            document.SaveAs2(ruta + "\\Reporte Canciones " + date + ".docx");
            document.Close();
            aplicacion.Quit();

        }
    }
}
