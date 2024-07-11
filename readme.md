# ShortNest - Acortador de URLs

![Logo de ShortNest](images/shortnestlogo2.png "Logo de ShortNest")

ShortNest es una aplicación web diseñada para acortar URLs, facilitando el compartir enlaces largos de manera más sencilla y estética. Este proyecto está construido utilizando ASP.NET Core para el backend, Vue.js para el frontend, y PostgreSQL para la gestión de la base de datos.

## Características

- **Acortamiento de URLs**: Permite a los usuarios ingresar una URL larga y obtener una versión más corta y manejable.
- **Redirección eficiente**: Los usuarios pueden acceder a la URL original a través de la versión acortada generada.
- **Interfaz amigable**: Ofrece una interfaz de usuario clara y sencilla para acortar y gestionar URLs.

## Tecnologías Utilizadas

- Backend: ASP.NET Core
- Frontend: Vue.js con TypeScript
- Gestión de estado: Vue Refs
- Base de datos: PostgreSQL
- Gestión de paquetes: npm

## Estructura del Proyecto

- `Controller/`: Contenedor principal del backend de la aplicación desarrollado en ASP.NET Core.
  - `appsettings.json` y `appsettings.Production.json`: Archivos de configuración para los entornos de desarrollo y producción, respectivamente.
  
- `Services/`: Encargado principal de los servicios y utiles de la aplicación.
  
- `Context/`: Contexto de nuestra base de datos con todos los elementos respectivos como **Entidades**,**DTOs** y **Mapeadores**.
  
- `ShortNestFront/`: Directorio del frontend de la aplicación, construido con Vue.js.
  - `src/components/`: Componentes Vue utilizados en la aplicación.
  - `src/composables/`: Composables Vue para la lógica reutilizable.
  - `src/models/`: Modelos de datos TypeScript.
  - `.env.prod`: Archivo de variables de entorno para producción.


## Configuración y Despliegue

### Requisitos Previos

- .NET SDK
- Node.js y npm
- Un servidor SQL

### Configuración Local

1. Clonar el repositorio.
2. Instalar las dependencias del frontend con `npm install` dentro de `ShortNestFront/`.
3. Configurar la cadena de conexión a la base de datos en `Controller/appsettings.json`.
4. Ejecutar el backend desde `Controller/` con `dotnet run`.
5. Ejecutar el frontend desde `ShortNestFront/` con `npm run serve`.

### Despliegue en Producción

1. Configurar `Controller/appsettings.Production.json` con los detalles de producción.
2. Publicar el backend con `dotnet publish`.
3. Construir el frontend con `npm run build`.
4. Desplegar los archivos publicados y construidos en el servidor de producción.
5. Configurar un servidor web como Nginx para servir el frontend y actuar como proxy inverso para el backend.

## Contribuir

Para contribuir al proyecto, por favor crea un fork del repositorio, realiza tus cambios y envía un pull request con una descripción detallada de tus modificaciones.

## Licencia

Este proyecto fue desarrollador por Jorge Alonso Cruz Vera.