using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace datosSunat
{
    public class getSunat
    {
        public string obtenerRUC(string datosSunat)
        {
            string formatearDatos = Regex.Replace(datosSunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
            int first = formatearDatos.IndexOf("RUC: </td> <td class=\"bg\" colspan=3>") + "RUC: </td> <td class=\"bg\" colspan=3>".Length;
            return formatearDatos.Substring(first, 11);
        }
        public string obtenerRAZONSOCIAL(string datosSunat)
        {
            string formatearDatos = Regex.Replace(datosSunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
            int first = formatearDatos.IndexOf("RUC: </td> <td class=\"bg\" colspan=3>") + "RUC: </td> <td class=\"bg\" colspan=3>".Length + 14;
            int last = formatearDatos.IndexOf("</td> </tr> <tr> <td class=\"bgn\" colspan=1>Tipo Contribuyente:") - first;
            return formatearDatos.Substring(first, last);
        }

        public string obtenerDOMICILIOFISCAL(string datosSunat)
        {
            try
            {
                string formatearDatos = Regex.Replace(datosSunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
                int first = formatearDatos.IndexOf("Domicilio Fiscal:</td> <td class=\"bg\" colspan=3>") + "Domicilio Fiscal:</td> <td class=\"bg\" colspan=3>".Length;
                int last = formatearDatos.IndexOf("</td> </tr> <!-- PAS20134EA20000249<tr>-->") - first;
                return formatearDatos.Substring(first, last);
            }
            catch
            {
                return "";
            }

        }
        public string obtenerNOMBRECOMERCIAL(string datosSunat)
        {
            try
            {
                string formatearDatos = Regex.Replace(datosSunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
                int first = formatearDatos.IndexOf("Nombre Comercial: </td> <td class=\"bg\" colspan=1>") + "Nombre Comercial: </td> <td class=\"bg\" colspan=1>".Length;
                int last = formatearDatos.IndexOf("</td> <td class=\"bgn\" colspan=1 >Afecto al Nuevo RUS: </td> <td class=\"bg\" colspan=1>");
                if(last <= 0)
                {
                    last = formatearDatos.IndexOf("</td> </tr> <tr> <td class=\"bgn\" colspan=1>Fecha de Inscripci&oacute;n:") - first;
                }
                else
                {
                    last = formatearDatos.IndexOf("</td> <td class=\"bgn\" colspan=1 >Afecto al Nuevo RUS: </td> <td class=\"bg\" colspan=1>") - first;
                }
                return formatearDatos.Substring(first, last);
            }
            catch
            {
                return "";
            }

        }
        public string obtenerTIPOCONTRIBUYENTE(string datosSunat)
        {
            try
            {
                string formatearDatos = Regex.Replace(datosSunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
                int first = formatearDatos.IndexOf("Tipo Contribuyente: </td> <td class=\"bg\" colspan=3>") + "Tipo Contribuyente: </td> <td class=\"bg\" colspan=3>".Length;


                int last = formatearDatos.IndexOf("</td> </tr> <tr> <td class=\"bgn\" colspan=1>Tipo de Documento: </td> <td class=\"bg\" colspan=3>DNI");

                if (last <= 0) {
                    last = formatearDatos.IndexOf("</td> </tr> <tr> <td class=\"bgn\" colspan=1 >Nombre Comercial:") - first;
                }
                else
                {
                    last = formatearDatos.IndexOf("</td> </tr> <tr> <td class=\"bgn\" colspan=1>Tipo de Documento: </td> <td class=\"bg\" colspan=3>DNI") - first;
                }

                
                return formatearDatos.Substring(first, last);
            }
            catch
            {
                return "";
            }

        }
        public string obtenerESTADO(string datosSunat)
        {
            try
            {
                string formatearDatos = Regex.Replace(datosSunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
                int first = formatearDatos.IndexOf("Estado: </td> <td class=\"bg\" colspan=1>") + "Estado: </td> <td class=\"bg\" colspan=1>".Length;
                int last = formatearDatos.IndexOf("</td> <td class=\"bgn\" colspan=1> </td> </tr> <tr> <td class=\"bgn\"colspan=1>Condici&oacute;n:</td>") - first;

                if (last <= 0)
                {
                    first = formatearDatos.IndexOf("Estado: </td> <td class=\"bg\" colspan=1>") + "Estado: </td> <td class=\"bg\" colspan=1>".Length;
                    last = formatearDatos.IndexOf("</td> <td class=\"bgn\" colspan=1> Fecha de Baja: ") - first;
                }

                
                return formatearDatos.Substring(first, last);
            }
            catch
            {
                return "";
            }

        }
        public string obtenerCONDICION(string datosSunat)
        {
            try
            {
                string formatearDatos = Regex.Replace(datosSunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
                int first = formatearDatos.IndexOf("Condici&oacute;n:</td> <td class=\"bg\" colspan=3> ") + "Condici&oacute;n:</td> <td class=\"bg\" colspan=3> ".Length;
                int last = formatearDatos.IndexOf(" </td> </tr> <tr> <td class=\"bgn\" colspan=1>Domicilio Fiscal:") - first;
                return formatearDatos.Substring(first, last);
            }
            catch
            {
                return "";
            }

        }
        public async Task<string> ObtenerDatosSunat(string numeroRUC)
        {
            using var client = new HttpClient();
            int codigoRandom = 0;
            codigoRandom = int.Parse(await client.GetStringAsync("https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=random"));
            return await client.GetStringAsync("https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + numeroRUC + "&numRnd=" + codigoRandom);
        }
    }
}
