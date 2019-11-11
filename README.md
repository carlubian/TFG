# Código fuente TFG
[![Build Status](https://carlubian.visualstudio.com/TFG/_apis/build/status/Build%20TFG?branchName=master)](https://carlubian.visualstudio.com/TFG/_build/latest?definitionId=23&branchName=master)

Aquí aparece todo el código fuente para el TFG. Iincluye los siguientes proyectos:

* <strong>TFG.UWP</strong>: Interfaz cliente en forma de Aplicación Universal de Windows.
* <strong>TFG.Core</strong>: Clases del modelo, validación de datos, cliente Kaomi y métodos de extensión.
* <strong>Kaomi.Legacy</strong>: Versión de Kaomi compatible con .NET Framework 4.6.1 para poder usar las librerías de sensores.
* <strong>Kaomi.WebAPI.Legacy</strong>: Servidor WebAPI de Kaomi compatible con .NET Framework 4.6.1 para poder usar las librerías de sensores.
* <strong>Kaomi.Processes</strong>: Procesos de sensores para Kaomi, incluyendo un proceso de prueba.

## Notas
Las versiones Legacy de Kaomi solo son necesarias para usar procesos que incluyan dependencias compiladas sobre net461. El proceso de prueba está compilado sobre netcoreapp3.0, y por lo tanto puede usar las versiones normales de Kaomi, que se pueden encontrar en su propio repositorio.

Además, el proceso real necesita librerías que no se incluyen necesariamente aquí.

## Otras dependencias
Esta aplicación también depende de la librería ConfigAdapter para gestionar los archivos de configuración. Esta librería, junto al resto de dependencias de terceros, se restaura automáticamente desde NuGet al compilar la aplicación.

# Requisitos
Para ejecutar la interfaz cliente <strong>TFG.UWP</strong> es necesario:
1.	Windows 10 versión 1803 o superior.

Para ejecutar el servidor Kaomi es necesario:
1. .NET Framework 4.6.1

Nota: Es posible hacer que la aplicación se conecte a un servidor Kaomi que apunte a .NET Core 3.0. Dicho servidor se puede encontrar en el repositorio principal de Kaomi.

# Ejecutar la aplicación
Es posible ejecutar la aplicación en modo depuración desde Visual Studio, o instalarla de forma local mediante el paquete de despliegue. Para ello, se recomienda seguir los siguientes pasos:
* Navegar al directorio \_Despliegue\_/TFG.UWP_1.0.2.0
* Ejecutar el script Add-AppDevPackage.ps1 con Powershell.

Ese script se encargará de instalar las dependencias y registrar el certificado de pruebas necesario para desplegar y ejecutar la aplicación.

## Despliegue de procesos remotos
La otra cara de la moneda son los procesos remotos de sensores. Para desplegar uno de estos procesos, se deben seguir los siguientes pasos:
* Colocar un servidor Kaomi en la máquina remota (aunque para hacer pruebas se puede colocar en localhost).
* Copiar al directorio local los ensamblados necesarios para instanciar el proceso deseado.
* Arrancar el servidor Kaomi e indicarle que cargue el ensamblado e instancie el proceso.
* Conectarse desde la aplicación local al servidor Kaomi remoto.

### Directorio de ensamblados
El directorio en el que se deben colocar los ensamblados y sus dependencias cambia en función de la versión de Kaomi que se esté utilizando:

* Kaomi.Legacy para net461: En este caso deben colocarse en el mismo directorio que el ejecutable Kaomi.WebAPI.exe
* Kaomi para netcoreapp3.0: En este caso deben colocarse en %LocalAppData%/Kaomi Assemblies

Para obtener información adicional sobre cómo crear o gestionar tanto el servidor como los procesos remotos, se recomienda ir al repositorio principal de Kaomi.
