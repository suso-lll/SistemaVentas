using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;   

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        private static int _intentosFallidos = 0;
        private static DateTime _tiempoBloqueo = DateTime.MinValue;
        private const int MAX_INTENTOS = 3;
        private const int TIEMPO_BLOQUEO_MINUTOS = 5;
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

            if (_intentosFallidos >= MAX_INTENTOS && DateTime.Now < _tiempoBloqueo)
            {
                TimeSpan tiempoRestante = _tiempoBloqueo - DateTime.Now;
                MessageBox.Show($"Demasiados intentos fallidos. Intente nuevamente en {tiempoRestante.Minutes} minutos y {tiempoRestante.Seconds} segundos.",
                                "Cuenta bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (_intentosFallidos >= MAX_INTENTOS && DateTime.Now >= _tiempoBloqueo)
            {
                // Reiniciar si ya pasó el tiempo
                _intentosFallidos = 0;
                _tiempoBloqueo = DateTime.MinValue;
            }

            // 2. Validación original (NO MODIFICAS NADA DE LA LÓGICA EXISTENTE)
            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == txtdocumento.Text && u.Clave == txtclave.Text).FirstOrDefault();

            if (ousuario != null)
            {
                // Login exitoso: reiniciar contadores
                _intentosFallidos = 0;
                _tiempoBloqueo = DateTime.MinValue;

                Inicio form = new Inicio(ousuario);
                form.Show();
                this.Hide();
                form.FormClosed += frm_closing;
            }
            else
            {
                // Incrementar fallos
                _intentosFallidos++;
                int intentosRestantes = MAX_INTENTOS - _intentosFallidos;

                if (intentosRestantes > 0)
                {
                    MessageBox.Show($"Usuario o contraseña incorrectos. Le quedan {intentosRestantes} intento(s).",
                                    "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    _tiempoBloqueo = DateTime.Now.AddMinutes(TIEMPO_BLOQUEO_MINUTOS);
                    MessageBox.Show($"Ha superado el número máximo de intentos. La cuenta quedará bloqueada por {TIEMPO_BLOQUEO_MINUTOS} minutos.",
                                    "Cuenta bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Limpiar contraseña para nuevo intento
                txtclave.Text = "";
                txtclave.Focus();
            }

        }

        private void frm_closing(object sender, FormClosedEventArgs e)
        {
            txtdocumento.Text = "";
            txtclave.Text = "";
            this.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
