# ARS Avalanche ❄️

## Descripción

ARS Avalanche es una plataforma integral de gestión de seguros de salud desarrollada en C#. El sistema está diseñado para facilitar la administración de afiliados, políticas de cobertura, autorizaciones médicas y notificaciones en tiempo real para aseguradoras.

## 🎯 Características principales

- **Gestión de Afiliados**: Registro y administración de afiliados al sistema
- **Planes de Cobertura**: Administración de planes con diferentes coberturas y costos
- **Autorización de Servicios**: Sistema completo de autorización de servicios médicos
- **Gestión de Hospitales**: Registro y validación de instituciones médicas
- **Notificaciones en Tiempo Real**: Sistema de notificaciones usando SignalR
- **Gestión de Usuarios**: Control de acceso con roles diferenciados (SuperAdmin, Administrator, Analyst, Guest)
- **Envío de Emails**: Servicio de comunicación vía correo electrónico
- **API RESTful**: Interfaz completa para integración con clientes

## 🏗️ Arquitectura

El proyecto sigue una arquitectura de capas limpia (Clean Architecture):

```
BackEnd/Avalanche/
├── Avalanche.Core.Domain/          # Capa de dominio
│   ├── Entities/                   # Entidades principales (Plan, Policy, etc.)
│   ├── Common/                     # Clases base y comunes
│   └── Settings/                   # Configuraciones (MailSettings)
├── Avalanche.Core.Application/     # Capa de aplicación
│   ├── Dtos/                       # Data Transfer Objects
│   ├── Features/                   # CQRS (Queries y Commands)
│   ├── Mappings/                   # AutoMapper profiles
│   ├── Interfaces/                 # Contratos
│   ├── Helpers/                    # Utilidades (ImageUpload)
│   ├── Constants/                  # Constantes
│   └── Enums/                      # Enumerables
├── Avalanche.Infrastructure.Shared/ # Servicios compartidos
│   └── Services/                   # EmailService, etc.
└── Avalanche.Interface.BusinessApi/ # Capa de presentación
    ├── Controllers/                # Endpoints API
    ├── Hubs/                       # SignalR Hubs
    └── Services/                   # NotificationSender
```

## 🛠️ Tecnologías utilizadas

- **Framework**: .NET (C#)
- **Patrón de diseño**: Clean Architecture, CQRS
- **ORM**: Entity Framework Core
- **Mapping**: AutoMapper
- **Comunicación en tiempo real**: SignalR
- **Email**: MailKit
- **API Documentation**: Swagger/OpenAPI

## 📦 Entidades principales

### Plan
- **Name**: Nombre del plan
- **Description**: Descripción (opcional)
- **MonthlyCost**: Costo mensual
- **Policies**: Políticas asociadas
- **PlanCoverages**: Coberturas incluidas

### Roles del sistema
- `SuperAdmin`: Acceso total al sistema
- `Administrator`: Administración general
- `Analyst`: Análisis de autorizaciones
- `Guest`: Solo lectura

### Autorización
Sistema completo para autorizar servicios médicos:
- Tipo de autorización
- Hospital asociado
- Política/Afiliado
- Estado de la autorización
- Fecha de solicitud

## 🚀 Primeros pasos

### Requisitos previos
- .NET SDK (versión específica a definir)
- SQL Server (para base de datos)
- Visual Studio o VS Code

### Instalación

1. **Clonar el repositorio**
```bash
git clone https://github.com/Servicios-WEBI/ARS-Avalanche.git
cd ARS-Avalanche
```

2. **Restaurar dependencias**
```bash
dotnet restore
```

3. **Configurar la base de datos**
- Actualizar la cadena de conexión en `appsettings.json`
- Ejecutar migraciones:
```bash
dotnet ef database update
```

4. **Compilar el proyecto**
```bash
dotnet build
```

5. **Ejecutar la aplicación**
```bash
dotnet run --project BackEnd/Avalanche/Avalanche.Interface.BusinessApi
```

La API estará disponible en `http://localhost:5000` (o el puerto configurado)

## 📚 Documentación de API

Una vez ejecutada la aplicación, accede a la documentación interactiva de Swagger:
```
http://localhost:5000/swagger
```

## 🔐 Autenticación

El sistema incluye:
- Autenticación de usuarios
- Registro de administradores y analistas
- Control de acceso basado en roles (RBAC)
- Manejo seguro de contraseñas

## 📧 Configuración de Email

Configura los siguientes parámetros en `appsettings.json`:

```json
"MailSettings": {
  "EmailFrom": "tu-email@ejemplo.com",
  "SmtpHost": "smtp.gmail.com",
  "SmtpPort": 587,
  "SmtpUser": "tu-usuario",
  "SmtpPass": "tu-contraseña",
  "DisplayName": "ARS Avalanche"
}
```

## 🔔 Notificaciones en Tiempo Real

El sistema usa SignalR para notificaciones en tiempo real:
- Notificaciones de autorizaciones asignadas
- Actualización de estados
- Alertas importantes

## 🤝 Contribución

Las contribuciones son bienvenidas. Para cambios importantes:

1. Fork el repositorio
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📝 Licencia

Este proyecto es desarrollado por **Servicios-WEBI**.

## 👥 Autores

- **Servicios-WEBI Organization**

## 📞 Soporte

Para reportar problemas o sugerencias, abre un issue en el repositorio.

## 🗺️ Roadmap

- [ ] Frontend en React/Angular
- [ ] Módulo de reportes avanzados
- [ ] Integración con sistemas externos
- [ ] Módulo de facturación
- [ ] Aplicación móvil