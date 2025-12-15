**Languages:**  
[🇬🇹 Español](README.es.md) | [🌎 English](README.md)

## Plantilla Empresarial para aplicaciones en Razor Pages y .NET
## <sub>Impulsada por Selenium, Principios CA, TDD, Docker & GitHub Actions</sub>


## Introducción
Este proyecto proporciona una base sólida para construir aplicaciones web con Razor Pages. Su objetivo es ofrecer un punto de partida claro y mantenible que pueda crecer sin complicaciones. Está pensado para equipos o desarrolladores individuales que desean comenzar con una estructura bien definida sin tener que reinventar configuraciones esenciales.

## Estructura

**Business**: Incluye las reglas y comportamientos principales del negocio.  

**Application**: Define los servicios y coordina la comunicación entre la presentación y la lógica de negocio.  

**Infrastructure**: Implementa los detalles técnicos como acceso a datos y servicios externos.  

**Presentation**: Contiene las Razor Pages y la lógica que gestiona la interacción con el usuario.  

<img width="578" height="404" alt="projects-dependency-diagram" src="https://github.com/user-attachments/assets/0764a3f9-4737-4d57-aae4-deaf11605441" />

## Enfoque de desarrollo
La solución sigue un flujo de trabajo orientado a TDD, incorporando pruebas unitarias y de integración para validar los componentes clave y garantizar la calidad del código desde las primeras etapas del desarrollo. Además, se implementan pruebas end-to-end utilizando Selenium para verificar flujos críticos de usuario en un entorno de navegador real.

## Integración continua
El repositorio incluye un pipeline de CI configurado con GitHub Actions. Este flujo automatiza la ejecución de pruebas y verifica que cada cambio cumpla con los estándares definidos.

## Entorno de ejecución
Para simplificar la configuración y asegurar consistencia entre entornos, el proyecto utiliza Docker Compose. La configuración incluye soporte para SQL Server, lo que permite replicar fácilmente un entorno de base de datos realista tanto para desarrollo como para pruebas.

## Video en YouTube
Para una breve demostración, grabé el siguiente video:
[Ver en YouTube](https://youtu.be/0nfXpb7OsPA?si=28_t2m6mDIMfSiVw)

## Proyecto creado y mantenido por [Luis López](https://github.com/luislopez-dev)
