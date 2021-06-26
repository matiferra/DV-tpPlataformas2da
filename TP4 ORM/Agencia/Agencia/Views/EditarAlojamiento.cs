﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agencia.Views
{
    public partial class EditarAlojamiento : Form
    {
        Bussines.AgenciaManager Ag = new Bussines.AgenciaManager();
        public EditarAlojamiento(AdmAlojamientos alojaPadre)
        {
            InitializeComponent();
            combo_ciudad.DisplayMember = "nombre";
            combo_ciudad.ValueMember = "id";
            combo_ciudad.DataSource = Ag.getCiudades();
        }

        public delegate void UpdateDelegate(object serder, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventArgsHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void agregar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventArgsHandler.Invoke(this, args);
        }
        private void Agrego_boton_Click(object sender, EventArgs e)
        {
            // RESERVADO ATRIBUTO VER
            if (Ag.modificarAlojamiento(combo_ciudad.SelectedValue.ToString(), "NOMBREALOJAMIENTO",barrioText.Text, estrellasText.Text, personasText.Text, check_tv.Checked,
                                    precioxdiaText.Text, habitacionesText.Text, baniosText.Text, campo_precioxpersona.Text, false))
            {
                MessageBox.Show("Agregado con éxito");
                agregar();
                this.Close();
                this.Dispose();

            }
            else
            {
                MessageBox.Show("No se pudo Modificar el alojamiento");
            }

        }


        private void EditarAlojamiento_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
