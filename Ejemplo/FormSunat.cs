using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejemplo
{
    public partial class FormSunat : Form
    {
        public FormSunat()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            datosSunat.getSunat getSunat = new datosSunat.getSunat();
            getSunat.NumeroRUC = txt_buscarRUC.Text;
            string errorMensaje = getSunat.ObtenerDatosSunat();
            if (errorMensaje == "ok") { ;
                string Sunat = getSunat.DatosSunat;
                txt_visor.Text = Regex.Replace(Sunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
                txt_RUC.Text = getSunat.NumeroRUC;
                txt_razonSocial.Text = "";
                txt_domicilioFiscal.Text = getSunat.DomicilioFiscal;
                txt_nombreComercial.Text = getSunat.NombreComercial;
                txt_tipoDeContribuyente.Text = getSunat.TipoDeContribuyente;
                txt_estado.Text = getSunat.EstadoDeContribuyente;
                txt_condicion.Text = getSunat.CondicionDeContribuyente;
                txt_razonSocial.Text = getSunat.RazonSocial;
            }
            else
            {
                MessageBox.Show(errorMensaje);
            }
        }
    }
}
