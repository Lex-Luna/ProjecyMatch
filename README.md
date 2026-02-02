# ProyecMatch

Aplicación móvil Xamarin.Forms para iOS y Android con autenticación Firebase.

## 🔧 Configuración Firebase

1. En [Firebase Console](https://console.firebase.google.com/), ve a **Authentication** → **Sign-in method**
2. Habilita los proveedores necesarios (Email/Password, Google, etc.)
3. Descarga los archivos de configuración:
   - **Android:** `google-services.json` → `ProyecMatch.Android/Assets/`
   - **iOS:** `GoogleService-Info.plist` → `ProyecMatch.iOS/Resources/`

## 📦 Paquetes NuGet Requeridos

⚠️ Estas versiones son **mutuamente compatibles** - no las actualices:

```powershell
Install-Package FirebaseAuthentication -Version 3.7.2
Install-Package FirebaseDatabase.net -Version 4.2.0
```

Instálalas en todos los proyectos de la solución.

## 🚀 Compilar y Ejecutar

```bash
dotnet restore
dotnet build -c Debug

# Android
dotnet build -f net6.0-android -c Debug

# iOS
dotnet build -f net6.0-ios -c Debug
```

## 📁 Estructura

- `Models/` - Modelos de datos
- `ViewModels/` - Lógica de presentación
- `Views/` - Interfaces XAML
- `Datos/` - Acceso a datos Firebase
- `Conexiones/` - Configuración

## 📝 Notas

- Firebase Authentication debe habilitarse en Firebase Console
- `Musuario.cs` - Modelo de usuario
- `DcrearCuenta.cs` - Registro de cuentas
- `Dusuario.cs` - Gestión de usuarios
- Configuración en `Conexiones/Constantes.cs`
