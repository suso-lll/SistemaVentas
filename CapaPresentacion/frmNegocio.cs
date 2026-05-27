using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmNegocio : Form
    {
        public frmNegocio()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtruc_Click(object sender, EventArgs e)
        {

        }
        public Image ByteToImage(byte[]imageBytes)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imageBytes,0,imageBytes.Length);
            Image image = new Bitmap(ms);

            return image;
        }

        private void frmNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimage = new CN_Negocio().ObtenerLogo(out obtenido); 
            
            if(obtenido && byteimage.Length > 0)
                picLogo.Image = ByteToImage(byteimage);
            else
            picLogo.Image = null;

            Negocio datos = new CN_Negocio().ObtenerDatos();
            txtnombre.Text = datos.Nombre;
            txtruc.Text = datos.RUC;
            txtdireccion.Text = datos.Direccion;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            OpenFileDialog oPenFileDialog = new OpenFileDialog();
            oPenFileDialog.FileName = "Files | *.jpg;*.jpeg;*.png";

            if(oPenFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] byteimage = File.ReadAllBytes(oPenFileDialog.FileName);
                bool respuesta = new CN_Negocio().ActualizarLogo(byteimage, out mensaje);
                
                if(respuesta)
                    picLogo.Image = ByteToImage(byteimage);
                else
                MessageBox.Show(mensaje,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Negocio obj = new Negocio()
            {
                Nombre = txtnombre.Text,
                RUC = txtruc.Text,
                Direccion = txtdireccion.Text
            };

            bool respuesta = new CN_Negocio().GuardarDatos(obj, out mensaje);
            if (respuesta)
            {
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void picLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
