﻿## [2025-04-29]

- 🚀 Fix: solución del error de inicio por ícono faltante + ajustes finales - SeguridadHelper fue movido a su propio archivo .cs en vez de estar embebido en AdminForm.cs - Comentarios XML añadidos - Changelog actualizado - Añadir soporte para cierre de sesión con retorno al LoginForm - Cambios de eficiencia de código en todos los archivos e interfaz gráfica rediseñada. Lista completa de cambios en el CHANGELOG.md adjunto. - Proyecto funcional sin documentación ni pruebas unitarias - Merge branch 'master' of https://github.com/Fedebarriosd/ChinoPelado - Commit del proyecto completamente funcional. Faltan los unit-tests y la documentación formal, - Consideraciones importantse 1 README.md - Create LICENSE - Update README.md - README.md - Add project files. - Add .gitattributes, .gitignore, and README.md.

---
## 2025-04-16

### LoginForm.cs
- Unificación de placeholders:
  - Eliminados los métodos `InicializarPlaceholders` y los handlers individuales de `Enter`/`Leave`.
  - Creado método `ConfigurarPlaceholder(TextBox, string, bool)`.
- Asignación de eventos:
  - Suscripción a `btnIniciarSesion.Click` tras `InitializeComponent()`.
- Flujo de navegación:
  - Uso de `ShowDialog()` para abrir formularios Admin/User y `this.Close()` al terminar.
- Cierre de sesión:
  - Login ahora detecta `DialogResult.Retry` para mostrar nuevamente el login tras cerrar sesión desde el AdminForm o UserForm.

### LoginForm.Designer.cs
- Layout responsivo:
  - Reemplazo de posiciones fijas por un `TableLayoutPanel` de 2 columnas y 4 filas.
  - Eliminación de todas las propiedades `Location` y `Size` fijas.

### AdminForm.cs
- Centralización de acceso a datos:
  - Añadido `EjecutarQuery(string, Action<SQLiteCommand>)` para unificar `ExecuteNonQuery()`.
- Seguridad:
  - Movido `CalcularHash` a la clase estática `SeguridadHelper`.
- Refactor de operaciones:
  - `btnAgregar` y `btnDesactivar` envueltos en `try-catch` usando `EjecutarQuery()`.
  - `btnEditar` muestra `EditarPerfilForm` y pasa parámetros a nuevo método `EditarPerfil(...)`.
- Nuevo método `EditarPerfil(...)`:
  - Dos variantes de `UPDATE`: con o sin nueva contraseña.
  - Mensaje único de éxito: `MessageBox.Show("Perfil editado correctamente.")`.
- Nuevo botón `Cerrar sesión`:
  - Agregado botón en la cuarta fila del `TableLayoutPanel`.
  - Devuelve `DialogResult.Retry` al LoginForm para reiniciar sesión sin cerrar la aplicación.

### AdminForm.Designer.cs
- Layout responsivo:
  - Uso de `TableLayoutPanel` de 1 columna y 4 filas con `Dock = Fill`.
  - Botones con `Dock = Fill`, sin `Location`/`Size` fijos.
  - Añadido botón `Cerrar sesión` en fila 3.

### DbConfig.cs
- Centralización de la cadena de conexión:
  - Clase estática `DbConfig` con `const string ConnectionString`.
  - Eliminación de duplicados en otros formularios.

### BuscarPerfilForm.cs
- Helper de lectura:
  - Añadido `EjecutarQueryReader(string, Action<SQLiteCommand>, Action<SQLiteDataReader>)`.
- Carga de datos:
  - Implementado `ObtenerUsuariosActivos()` que devuelve `List<string>`.
  - Usado `lstUsuarios.DataSource = usuarios` en lugar de `Items.Add(...)`.
- Mejora de UI:
  - Suscripción a `lstUsuarios.SelectedIndexChanged` para habilitar/deshabilitar `btnSeleccionar`.

### BuscarPerfilForm.Designer.cs
- Corrección de error de diseñador:
  - Extracción de `DbConfig` a su propio archivo para que el form sea la primera clase.

### EditarPerfilForm.cs
- Carga real de perfil:
  - En `Load`, uso de `EjecutarQueryReader` para poblar `txtUsuario` y `chkEsAdministrador`.
- Tooltip de contraseña:
  - Campo `_tooltipContrasena` con `ShowAlways = true`, `InitialDelay`, `ReshowDelay`, `AutoPopDelay`.
  - Asociado a `txtContrasena` para que aparezca en cualquier parte del control.
- Contraseña opcional:
  - Propiedad `Contrasena` queda `null` si el TextBox está vacío (no se modifica).
- Validación de unicidad:
  - Añadido helper `UsuarioExiste(string)` con `SELECT COUNT(1)`.
- Feedback UI:
  - Suscripción a `TextChanged` y `CheckedChanged` para habilitar `btnAceptar` solo tras cambios.

### EditarPerfilForm.Designer.cs
- Layout responsivo:
  - Uso de `TableLayoutPanel` con 2 columnas (35%/65%) y 4 filas (labels, campos, checkbox y botones).
  - Eliminación de `Location`/`Size` fijos; controles anclados y centrados.

### AgregarPerfilForm.cs
- Teclas de acceso:
  - Configurados `AcceptButton = btnAceptar` y `CancelButton = btnCancelar`.
- Validación dinámica:
  - `btnAceptar` deshabilitado hasta que ambos `TextBox` tengan contenido (`TextChanged`).
- Trim de entradas:
  - Aplicado `.Trim()` a `Usuario` y `Contrasena` al asignar propiedades.
- Tooltip de contraseña:
  - Campo `_tooltipContrasena` con `ShowAlways = true`, delays configurados.
- Validación de unicidad:
  - Inclusión de `UsuarioExiste(string)` para evitar duplicados antes de aceptar.

### Archivos ICO y CHANGELOG añadidos
- El logo del comercio fue convertido a ICO y añadido como imagen del proyecto.
- El changelog (este mismo archivo que está leyendo) fue añadido al proyecto.

---
