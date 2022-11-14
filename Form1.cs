using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pictureBox
{
    public partial class Form1 : Form
    {
        private List<Discos> listaDiscos;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DiscosNegocio negocio = new DiscosNegocio();
            listaDiscos = negocio.listar();

            dgvBaseDatos.DataSource = listaDiscos;
            cargarImagen(listaDiscos[0].UrlImagen);                 
        }

        private void dgvBaseDatos_SelectionChanged(object sender, EventArgs e)
        {
            // Tomo el objeto de la fila que esta seleccionada...
            // Hago un casteo explicito para reciba Discos...
            Discos seleccionado = (Discos)dgvBaseDatos.CurrentRow.DataBoundItem;
            
            // Llamo al metodo de cargarImagen, y le paso del objeto 'seleccionado', su Url (atributo del objeto Discos)
            cargarImagen(seleccionado.UrlImagen);

            lblCanciones.Text = (seleccionado.CantidadCanciones - 2).ToString();
        }

        
        // Creo un metodo para cargar imagenes, y poder con la excepcion cargar una imagen vacia si no tiene url en la DB.
        private void cargarImagen(string imagen)
        {
            try
            {
                //  Cargo imagen en el Picture Box
                pbxImagen.Load(imagen);

            }
            catch (Exception ex)
            {

                pbxImagen.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
            }
        }
    } 
}
