# ObtenerDatosSunat
Software que permite obtener los datos de sunat, incluye un ejemplo en Windows Form desarrollado en Visual Studio 2022.

Mediante la DLL y sus propiedades se puede obtener:

numeroRUC;
tipoDeContribuyente;
domicilioFiscal;
nombreComercial;
estadoDeContribuyente;
condicionDeContribuyente;
razonSocial;

La DLL se puede editar para extraer mas datos.

En ejemplo se encuentra un Formulario donde al hacer click en capturar puede obtener los datos.

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

Nota: Este proyecto esta hecho en con .Net Core 5.0 DLL y el Ejemplo (Windows Form) para que funcione previamente el exe tienen que tener el .Net Core 5.

https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.202-windows-x64-installer

