# Código fuente TFG
Aquí aparece todo el código fuente para el TFG. Por el momento incluye los siguientes proyectos:

* <strong>TFG.UWP</strong>: Interfaz cliente en forma de Aplicación Universal de Windows.
* <strong>TFG.Core</strong>: Clases del modelo, validación de datos, cliente Kaomi y métodos de extensión.
* <strong>Kaomi.Legacy</strong>: Versión de Kaomi compatible con .NET Framework 4.6.1 para poder usar las librerías de sensores.
* <strong>Kaomi.WebAPI.Legacy</strong>: Servidor WebAPI de Kaomi compatible con .NET Framework 4.6.1 para poder usar las librerías de sensores.
* <strong>Kaomi.Processes</strong>: Procesos de sensores para Kaomi.

# Requisitos
Para ejecutar la interfaz cliente <strong>TFG.UWP</strong> es necesario:
1.	Windows 10 versión 1803 o superior.
2.	Conexión a internet.
3.	...

# Ejecutar la aplicación
Por ahora, ejecutar desde Visual Studio. Más adelante habrá una forma más elegante de publicar y ejecutar la interfaz.

Asegurarse de copiar todos los archivos DLL de Koami.Processes/Lib a la carpeta raíz de despliegue de Kaomi.WebAPI.Legacy si se va a usar el proceso Gocator.
