# C�digo fuente TFG
Aqu� aparece todo el c�digo fuente para el TFG. Por el momento incluye los siguientes proyectos:

* <strong>TFG.UWP</strong>: Interfaz cliente en forma de Aplicaci�n Universal de Windows.
* <strong>TFG.Core</strong>: Clases del modelo, validaci�n de datos, cliente Kaomi y m�todos de extensi�n.
* <strong>Kaomi.Legacy</strong>: Versi�n de Kaomi compatible con .NET Framework 4.6.1 para poder usar las librer�as de sensores.
* <strong>Kaomi.WebAPI.Legacy</strong>: Servidor WebAPI de Kaomi compatible con .NET Framework 4.6.1 para poder usar las librer�as de sensores.
* <strong>Kaomi.Processes</strong>: Procesos de sensores para Kaomi.

# Requisitos
Para ejecutar la interfaz cliente <strong>TFG.UWP</strong> es necesario:
1.	Windows 10 versi�n 1803 o superior.
2.	Conexi�n a internet.
3.	...

# Ejecutar la aplicaci�n
Por ahora, ejecutar desde Visual Studio. M�s adelante habr� una forma m�s elegante de publicar y ejecutar la interfaz.

Asegurarse de copiar todos los archivos DLL de Koami.Processes/Lib a la carpeta ra�z de despliegue de Kaomi.WebAPI.Legacy si se va a usar el proceso Gocator.
