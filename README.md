# ChinoPelado
Es el nombre de la pizzería que nos encomendó un módulo de gestión de inventario.

## Quiénes somos
- Federico Barrios
- Gustavo Barrios
- Mauricio Cáceres
- Leandro L'Heureux

## Requerimientos
- [Visual Studio](https://visualstudio.microsoft.com/vs/community/) como IDE.
- [SQLite](https://sqlite.org/download.html) como sistema de gestión de bases de datos relacional.
  - System.Data.SQLite.dll: Este es el archivo principal que permite interactuar con la base de datos desde C#. Se instala a través del paquete NuGet System.Data.SQLite.
  - SQLite.Interop.dll: Este archivo es necesario para las funciones nativas de SQLite y también se incluye automáticamente cuando instalas el paquete NuGet.
  - sqlite3.dll: Este archivo de la biblioteca nativa es necesario para ejecutar las consultas de SQLite. En caso de que no se incluya en el paquete de NuGet, se consigue desde la página de SQLite.
- [DB Browser](https://sqlitebrowser.org/dl/), aunque no es estrictamente necesario, ayuda a visualizar y operar con SQLite. Es el que usamos nosotros.
- .NET Framework. Se instala desde el setup manager de Visual Studio. Nosotros usamos la versión [9.0](https://dotnet.microsoft.com/es-es/download/dotnet/9.0)

## Permisos de uso
Nosotros creemos firmemente en el código libre y en sus principios éticos frente en contraposición al software propietario; es por ello que nuestro proyecto entero está bajo la licencia GNU General Public License (GPL) 3.0. Para más información sobre qué es sofware libre, consulte la [página de GNU](https://www.gnu.org/philosophy/free-sw.html).
Es importante recordar los 4 principios _fundamentales_ del software libre, los cuales son:
- <span style="color:yellow;">Libertad 0</span> La libertad de ejecutar el programa como se desee, con cualquier propósito.
- <span style="color:yellow;">Libertad 1</span> La libertad de estudiar cómo funciona el programa, y cambiarlo para que haga lo que se desee. El acceso al código fuente es una condición necesaria para ello.
- <span style="color:yellow;">Libertad 2</span> La libertad de redistribuir copiar para ayudar a otros.
- <span style="color:yellow;">Libertad 3</span> La libertad de distribuir copiar de sus versiones modificadas a terceros. Esto le permite ofrecer a toda la comunidad la oportunidad de beneficiarse de las modificaciones. El acceso al código fuente es una condición necesaria para ello.
