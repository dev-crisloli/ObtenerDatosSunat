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
            string Sunat = await getSunat.ObtenerDatosSunat(txt_buscarRUC.Text);
            txt_visor.Text = Regex.Replace(Sunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
            txt_RUC.Text = getSunat.obtenerRUC(Sunat);
            txt_razonSocial.Text = getSunat.obtenerRAZONSOCIAL(Sunat);
            txt_domicilioFiscal.Text = getSunat.obtenerDOMICILIOFISCAL(Sunat);
            txt_nombreComercial.Text = getSunat.obtenerNOMBRECOMERCIAL(Sunat);
            txt_tipoDeContribuyente.Text = getSunat.obtenerTIPOCONTRIBUYENTE(Sunat);
            txt_estado.Text = getSunat.obtenerESTADO(Sunat);
            txt_condicion.Text = getSunat.obtenerCONDICION(Sunat);
        }
    }
}
