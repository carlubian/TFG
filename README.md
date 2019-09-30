# C�digo fuente TFG
Aqu� aparece todo el c�digo fuente para el TFG. Por el momento incluye los siguientes proyectos:

* <strong>TFG.UWP</strong>: Interfaz cliente en forma de Aplicaci�n Universal de Windows.
* <strong>TFG.Core</strong>: Clases del modelo, validaci�n de datos, cliente Kaomi y m�todos de extensi�n.
* <strong>Kaomi.Legacy</strong>: Versi�n de Kaomi compatible con .NET Framework 4.6.1 para poder usar las librer�as de sensores.
* <strong>Kaomi.WebAPI.Legacy</strong>: Servidor WebAPI de Kaomi compatible con .NET Framework 4.6.1 para poder usar las librer�as de sensores.
* <strong>Kaomi.Processes</strong>: Procesos de sensores para Kaomi, incluyendo un proceso de prueba.

## Otras dependencias
Esta aplicaci�n tambi�n depende de la librer�a ConfigAdapter para gestionar los archivos de configuraci�n. Esta librer�a, junto al resto de dependencias de terceros, se restaura autom�ticamente desde NuGet al compilar la aplicaci�n.

# Requisitos
Para ejecutar la interfaz cliente <strong>TFG.UWP</strong> es necesario:
1.	Windows 10 versi�n 1803 o superior.

Para ejecutar el servidor Kaomi es necesario:
1. .NET Framework 4.6.1

Nota: Es posible hacer que la aplicaci�n se conecte a un servidor Kaomi que apunte a .NET Core 3.0. Dicho servidor se puede encontrar en el repositorio principal de Kaomi.

# Ejecutar la aplicaci�n
Es posible ejecutar la aplicaci�n en modo depuraci�n desde Visual Studio, o instalarla de forma local mediante el paquete de despliegue. Para ello, se recomienda seguir los siguientes pasos:
* Navegar al directorio TFG.UWP/AppPackages/TFG.UWP_1.0.1.0_Test
* Ejecutar el script Add-AppDevPackage.ps1 con Powershell.

Ese script se encargar� de instalar las dependencias y registrar el certificado de pruebas necesario para desplegar y ejecutar la aplicaci�n.

## Despliegue de procesos remotos
La otra cara de la moneda son los procesos remotos de sensores. Para desplegar uno de estos procesos, se deben seguir los siguientes pasos:
* Colocar un servidor Kaomi en la m�quina remota.
* Copiar al directorio local los ensamblados necesarios para instanciar el proceso deseado.
* Arrancar el servidor Kaomi e indicarle que cargue el ensamblado e instancie el proceso.
* Conectarse desde la aplicaci�n local al servidor Kaomi remoto.

Para obtener informaci�n adicional sobre c�mo crear o gestionar tanto el servidor como los procesos remotos, se recomienda ir al repositorio principal de Kaomi.
