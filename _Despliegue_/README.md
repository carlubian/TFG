# Despliegue de los servidores de prueba
Para las pruebas hay disponibles diez servidores locales
que utilizan el proceso de prueba TestGocator.

Antes de utilizarlos, es necesario copiar el ensamblado
de prueba al directorio correspondiente.

---
## Aviso importante
Los servidores web que interactúan con sensores reales
utilizan la versión Kaomi.Core.Legacy, y están compilados
para net461 x64.

Estos servidores de prueba, sin embargo, son una copia traída
del repositorio original de Kaomi, compilados para netcoreapp3.0 AnyCPU. 

A la interfaz le da igual esta diferencia, pero es importante
destacar que los sensores reales no pueden usar los servidores de prueba.

---

## Requisitos
* .NET Core Runtime 3.0
* ASP.NET Core Runtime 3.0

## Colocar el ensamblado de prueba
Los servidores de prueba .NET Core 3 no buscan los ensamblados en su carpeta local, como los de legado, sino en el directorio:

<kbd>%LocalAppData%\Kaomi Assemblies</kbd>

En este sentido, las diez instancias del servidor cargarán el
ensamblado desde un solo archivo.

## Importar configuración
También se incluye un archivo Settings.xml que permite importar los diez servidores de prueba a la interfaz de usuario con fines de prueba o demostración.

Se asume que los servidores se estarán ejecutando en la misma máquina que la aplicación.
