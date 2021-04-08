# ObtenerDatosSunat
Software que permite obtener los datos de sunat, incluye un ejemplo en Windows Form desarrollado en Visual Studio 2019.

Mediante la DLL datosSunat que contiene los siguientes metodos:

ObtenerDatosSunat(): Permite conectarce a la web de la sunat y sacar el codigo en html en texto plano y eliminar los saltos de linea y dejar unicamente un espacio.

  obtenerRAZONSOCIAL(): Permite extraer RAZONSOCIAL como string.
  obtenerDOMICILIOFISCAL(): Permite extraer DOMICILIOFISCA como string.
  obtenerNOMBRECOMERCIAL(): Permite extraer NOMBRECOMERCIAL como string.
  obtenerTIPOCONTRIBUYENTE(): Permite extraer la razon social como string.
  obtenerESTADO(): Permite extraer la ESTADO como string.
  obtenerCONDICION(): Permite extraer la CONDICION como string.

La DLL se puede editar para extraer mas datos.

En ejemplo se encuentra un Formulario donde al hacer click en capturar puede obtener los datos.

Nota: Este proyecto esta hecho en con .Net Core 5.0 DLL y el Ejemplo (Windows Form) para que funcione previamente el exe tienen que tener el .Net Core 5.

https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.202-windows-x64-installer

