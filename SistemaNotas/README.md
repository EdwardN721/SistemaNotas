# 🚀 SistemaNotas: Sistema de Anclaje Progresivo

## 🎯 Objetivo del Proyecto
Una plataforma personal diseñada para mejorar las habilidades de comunicación técnica, presentaciones y reporte de estatus. El sistema resuelve el problema del nerviosismo y las "muletillas" al hablar en público mediante una restricción de diseño: **prohíbe el copiar y pegar**. 

Al obligar al usuario a sintetizar la información en "Anclas" (ideas cortas de máximo 100 caracteres) y categorizarlas visualmente, el sistema reduce la carga cognitiva, permitiendo usar la aplicación móvil como un apoyo discreto ("salvavidas") en lugar de un guion de lectura.

## 💻 Stack Tecnológico
*   **Backend:** C# con .NET 10.
*   **Arquitectura:** Clean Architecture (Domain, Application, Infrastructure, API).
*   **Base de Datos:** SQL Server (gestionada vía Entity Framework Core 10).
*   **Frontend (Cliente Oficial):** App Móvil en Kotlin Nativo (Android), optimizada para lecturas rápidas "de reojo" durante las exposiciones.
*   **Entorno Local:** Linux Ubuntu, JetBrains Rider, Android Studio.

## 🏗️ Modelo de Dominio (Core Entities)
El sistema se basa en una jerarquía de anclaje progresivo:

1.  **`Presentacion`**: Cabecera del tema a exponer (Ej. "Implementar llaveMX").
2.  **`Seccion`**: Bloques lógicos o de tiempo que estructuran la exposición.
3.  **`CategoriaAncla`**: Catálogo de intenciones visuales (Ej. *Rojo = Error crítico, Amarillo = Cambio Provisional, Verde = Recomendación*).
4.  **`Ancla`**: El núcleo restrictivo. Notas extremadamente cortas (Max. 100 chars) enlazadas a una categoría para detonar el recuerdo sin necesidad de leer.
5.  **`Retrospectiva`**: Tracker de progreso post-presentación para medir el nivel de nerviosismo (1-10) y registrar muletillas a erradicar.

## ⚙️ Decisiones Arquitectónicas y Mejores Prácticas Aplicadas

*   **Entidad Base Robusta:** Uso de `Guid` para llaves primarias preparadas para sistemas distribuidos, control de auditoría (`CreatedAt`, `UpdatedAt` con `DateTimeOffset`) y soporte para Soft Delete (`IsDeleted`).
*   **Intercepción de Datos:** Implementación de un `SaveChangesInterceptor` en EF Core para automatizar el llenado de fechas de auditoría y transformar los borrados físicos en borrados lógicos.
*   **Fluent API Exclusivo:** El dominio es 100% agnóstico. Todas las reglas de base de datos, relaciones, protección de borrados en cascada (`DeleteBehavior.Restrict`) y conversiones de JSON (para el `List<string>`) viven estrictamente en la capa de Infraestructura.
*   **Patrones Empresariales:** Implementación de `IRepositoryGeneric<T>` y `IUnitOfWork`.
*   **Resiliencia y Concurrencia:** Soporte total de `CancellationToken` asíncrono desde el controlador hasta la base de datos, incluyendo manejo explícito de transacciones (`Begin`, `Commit`, `Rollback`).