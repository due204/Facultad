# Kanban CLI

![kanban_cli](https://raw.githubusercontent.com/due204/Facultad/refs/heads/main/kanban_cli/kanban_cli.png)

Una herramienta de línea de comandos desarrollada en **C#** para
gestionar tableros Kanban de manera simple, rápida y eficiente desde la
terminal.

Este proyecto no fue creado para ninguna materia de la facultad. Lo
desarrollé como una herramienta personal para mantener mis tareas
organizadas, mejorar mi productividad al programar y aprender un poco de C#.

## ✨ Características

-   Crear tableros con columnas personalizadas.
-   Listar tableros existentes.
-   Usar un tablero y trabajar dentro de él.
-   Agregar tareas.
-   Editar tareas.
-   Mover tareas entre columnas.
-   Eliminar tareas.
-   Eliminar tableros completos.
-   Vista mejorada con colores ANSI para facilitar la lectura.
-   Comandos simples inspirados en herramientas Unix.

## 📦 Requisitos

-   .NET SDK 6.0 o superior\
-   Sistema operativo compatible con colores ANSI (Linux recomendado)

## 🚀 Instalación y ejecución

Clonar el repositorio:

``` bash
git clone <FALTA_ESTA_PARTE>
cd KanbanCLI
```

Compilar:

``` bash
dotnet build
```

Ejecutar:

``` bash
dotnet run
```

## 📚 Uso general (Menú Principal)

Comandos disponibles:

  Comando   Acción
  --------- ----------------------------------------
  `help`    Muestra ayuda general
  `new`     Crear un nuevo tablero
  `ls`      Listar tableros existentes
  `use`     Usar un tablero existente
  `rm`      Eliminar un tablero
  `exit`    Salir del programa
  `clean`   Limpia la pantalla

## 📌 Uso dentro de un tablero

Cuando entrás a un tablero con `use`, la CLI cambia a este formato:

    NOMBRE_TABLERO $

Comandos disponibles:

  Comando                               Acción
  ------------------------------------  --------------------------------------------
  `ls`                                  Mostrar columnas y tareas con colores ANSI
  `ad <tarea>`                          Agregar una tarea a la primera columna
  `ed <col> <tarea> <nuevo texto>`      Editar una tarea existente
  `mv <colOrigen> <tarea> <colDestino>` Mover una tarea entre columnas
  `rm <col> <id>`                       Eliminar una tarea
  `cl`                                  Limpiar la pantalla
  `q`                                   Salir del tablero

## 🖼 Ejemplo de uso

### Crear un tablero

    > new
    Ingrese el nombre del tablero: Due
    Ingrese las columnas separadas por coma: Pensado, Iniciado, En proceso, Terminado
    Tablero creado.

### Listar tableros

    > ls
    1. Due
    2. TABLERO2

### Entrar al tablero

    > use Due
    Usando el tablero: Due

### Trabajar dentro del tablero

    Due $ ad Estudiar C#
    Due $ ad Comprar pan
    Due $ mv 1 2 4
    Due $ ed 1 1 Estudiar C# (repaso)
    Due $ rm 4 1
    Due $ ls

## 🎨 Vista con colores ANSI

El comando `ls` muestra:

-   **Amarillo** → nombre del tablero\
-   **Cyan**     → número de columna\
-   **Magenta**  → nombre de columna\
-   **Blanco**   → tareas

Esto mejora la lectura y hace más agradable el uso de la CLI.

## 🗂 Formato interno de almacenamiento

Los tableros se guardan en un archivo de texto plano (por ahora):

    TABLERO[
    Columna1{tarea1;tarea2;tarea3}
    Columna2{}
    Columna3{tarea4}
    ]

Es simple, portable y editable manualmente si es necesario.

## 🛠 Tecnologías utilizadas

-   C#
-   .NET 6
-   Manejo de archivos
-   Expresiones regulares
-   Colores ANSI
-   Diseño estilo CLI

## 🧭 Roadmap (futuras mejoras)

-   Exportar tableros a JSON/Markdown
-   Buscar tareas (`search`)
-   Ordenar tareas por prioridad
-   Filtrar tareas por palabra clave
-   Atajos tipo vim
-   Autocompletado de comandos
-   Temas ANSI (oscuro/claro)

## 👤 Autor

Desarrollado por **Due204**.

## 📜 Licencia

Este proyecto está bajo la licencia GPLv3.
Sientete libre de usar y modificar este código a tus necesidades.
