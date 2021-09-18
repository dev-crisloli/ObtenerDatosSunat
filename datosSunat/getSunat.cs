using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

/*
Autor: CRISTOPHER AVELINO LOLI ANTEQUERA
Fecha Actualizacion: 18-09-2021
 */

namespace datosSunat
{
    public class getSunat
    {
        List<string> arrDatosSunat = new List<string>();
        string datosSunat;
        string numeroRUC;
        string tipoDeContribuyente;
        string domicilioFiscal;
        string nombreComercial;
        string estadoDeContribuyente;
        string condicionDeContribuyente;
        string razonSocial;

        public string NumeroRUC { get => numeroRUC; set => numeroRUC = value; }
        public string DatosSunat { get => datosSunat; set => datosSunat = value; }
        public string TipoDeContribuyente { get => tipoDeContribuyente; set => tipoDeContribuyente = value; }
        public string DomicilioFiscal { get => domicilioFiscal; set => domicilioFiscal = value; }
        public string NombreComercial { get => nombreComercial; set => nombreComercial = value; }
        public string EstadoDeContribuyente { get => estadoDeContribuyente; set => estadoDeContribuyente = value; }
        public string CondicionDeContribuyente { get => condicionDeContribuyente; set => condicionDeContribuyente = value; }
        public string RazonSocial { get => razonSocial; set => razonSocial = value; }

        public void buscarDatosSunat()
        { 
            // Eliminamos demasiados espacios y solo dejamos un unico espacio por palabra
            string formatearDatos = Regex.Replace(DatosSunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), " {2,}", " ");
            //Buscamos el contenido que esten dentro de los siguientes elementos
            string elementSearchInicio = "<h4 class=\"list-group-item-heading\">";
            string elementSearchFinal = "</h4>";
            int antIndiceElementSearchInicio = 0;
            int indiceElementSearchInicio;
            int indiceElementSearchFinal;
            int longitudCaracteresExtraidos;
            do
            {
                indiceElementSearchInicio = formatearDatos.IndexOf(elementSearchInicio, antIndiceElementSearchInicio);
                indiceElementSearchFinal = formatearDatos.IndexOf(elementSearchFinal, antIndiceElementSearchInicio);
                antIndiceElementSearchInicio = indiceElementSearchFinal + elementSearchFinal.Length;
                longitudCaracteresExtraidos = indiceElementSearchFinal - indiceElementSearchInicio - elementSearchInicio.Length;
                if (indiceElementSearchInicio != -1)
                {
                    //Mediante HTMLDecode convertimos a texto los caracteres especiales de HTML
                    //ltrim END o START eliminamos los espacios al final o inicio del texto
                    //Reemplazamos los espacios o saltos de linea /t /n u otros
                    arrDatosSunat.Add(HttpUtility.HtmlDecode(Regex.Replace(formatearDatos.Substring(indiceElementSearchInicio + elementSearchInicio.Length, longitudCaracteresExtraidos), @"\t|\n|\r", "")).TrimStart().TrimEnd());
                }
            } while (longitudCaracteresExtraidos >= 0);

            //Buscamos el contenido que esten dentro de los siguientes elementos
            elementSearchInicio = "<p class=\"list-group-item-text\">";
            elementSearchFinal = "</p>";
            antIndiceElementSearchInicio = 0;

            do
            {
                indiceElementSearchInicio = formatearDatos.IndexOf(elementSearchInicio, antIndiceElementSearchInicio);
                indiceElementSearchFinal = formatearDatos.IndexOf(elementSearchFinal, antIndiceElementSearchInicio);
                antIndiceElementSearchInicio = indiceElementSearchFinal + elementSearchFinal.Length;
                longitudCaracteresExtraidos = indiceElementSearchFinal - indiceElementSearchInicio - elementSearchInicio.Length;
                if (indiceElementSearchInicio != -1)
                {
                    arrDatosSunat.Add(HttpUtility.HtmlDecode(Regex.Replace(formatearDatos.Substring(indiceElementSearchInicio + elementSearchInicio.Length, longitudCaracteresExtraidos), @"\t|\n|\r", "")).TrimStart().TrimEnd());
                }
            } while (longitudCaracteresExtraidos >= 0);
        }

        public void llenarInformaicionAClase()
        {
            //Asignamos TipoContribuyente
            if (arrDatosSunat[21] == "-")
            {
                TipoDeContribuyente = arrDatosSunat[20];
            }
            else
            {
                TipoDeContribuyente = arrDatosSunat[21];
            }

            //Llenoamos Informacion
            if (TipoDeContribuyente == "PERSONA NATURAL SIN NEGOCIO")
            {
                DomicilioFiscal = "-";
                nombreComercial = arrDatosSunat[23];
                estadoDeContribuyente = arrDatosSunat[26];
                condicionDeContribuyente = arrDatosSunat[27];
                RazonSocial = arrDatosSunat[1].Substring(14);
            }
            else
            {
                DomicilioFiscal = arrDatosSunat[26];
                nombreComercial = arrDatosSunat[21];
                estadoDeContribuyente = arrDatosSunat[24];
                condicionDeContribuyente = arrDatosSunat[25];
                RazonSocial = arrDatosSunat[1].Substring(14);
            }
        }



        //Obtenemos el codigo random para realizar la consulta por numero de ruc del HTML
        public int obtenerCodigoRandom(string htmlRandomSunat)
        {
            string formatearDatos = Regex.Replace(htmlRandomSunat.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\t", ""), " {2,}", " ");
            int first = formatearDatos.IndexOf("<input type=\"hidden\" name=\"numRnd\" value=\"") + "<input type=\"hidden\" name=\"numRnd\" value=\"".Length;
            int last = formatearDatos.IndexOf("\"><input type=\"hidden\" name=\"modo\" value=\"1\">") - first;
           

            if (first > 41) {
                return int.Parse(formatearDatos.Substring(first, last));
            }
            else
            {
                return -1;
            }
           
        }

        //Obtenemos los datos de sunat en codigo HTML
        public string ObtenerDatosSunat()
        {
            try
            {
                using var client = new HttpClient();
                int codigoRandom = 0;

                string codificacionEnvioRand = "application/x-www-form-urlencoded"; //Formulario
                StringContent parametrosEnvioRand = new StringContent("{\"accion\":\"consPorRazonSoc\",\"razSoc\":\"BVA FOODS\",\"modo\":\"1\"}", Encoding.UTF8, codificacionEnvioRand);

                var dictFormConsPorRanzonSoc = new Dictionary<string, string>();
                dictFormConsPorRanzonSoc.Add("accion", "consPorRazonSoc");
                dictFormConsPorRanzonSoc.Add("razSoc", "BVA FOODS");
                dictFormConsPorRanzonSoc.Add("modo", "1");

                FormUrlEncodedContent Content = new FormUrlEncodedContent(dictFormConsPorRanzonSoc);
                HttpResponseMessage codigoRandomPostHTML;
                string obtieneCodigoRandomHTML;

                do
                {
                    codigoRandomPostHTML = client.PostAsync("https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias", Content).Result;
                    obtieneCodigoRandomHTML = codigoRandomPostHTML.Content.ReadAsStringAsync().Result;
                    codigoRandom = obtenerCodigoRandom(obtieneCodigoRandomHTML);
                } while (codigoRandom == -1);

                var dictFormConsPorRuc = new Dictionary<string, string>();
                dictFormConsPorRuc.Add("accion", "consPorRuc");
                dictFormConsPorRuc.Add("nroRuc", NumeroRUC);
                dictFormConsPorRuc.Add("numRnd", codigoRandom.ToString());
                dictFormConsPorRuc.Add("actReturn", "1");
                dictFormConsPorRuc.Add("modo", "1");

                FormUrlEncodedContent contentFormConsPorRuc = new FormUrlEncodedContent(dictFormConsPorRuc);
                HttpResponseMessage datosRucPostHTML;
                string obtieneDatosRucHTML;

                do
                {
                    datosRucPostHTML = client.PostAsync("https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias", contentFormConsPorRuc).Result;
                    obtieneDatosRucHTML = datosRucPostHTML.Content.ReadAsStringAsync().Result;
                } while ((int)datosRucPostHTML.StatusCode != 200);

                DatosSunat = obtieneDatosRucHTML;
                buscarDatosSunat();
                llenarInformaicionAClase();
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
                //throw;
            }
        }
    }
}
