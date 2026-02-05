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

🎯 Tareas Pendientes
1. Mejoras en la Página de Bienvenida (Bienvenida.xaml)
🔹 Añadir imágenes RPG
Buscar o crear imágenes con temática RPG (aventureros, sponsors, etc.)

🔹 Añadir las imágenes a los proyectos nativos
Android: colocar en la carpeta Resources/drawable

iOS: colocar en la carpeta Resources

🔹 Usar las imágenes en la página de bienvenida
Imagen de aventurero para la sección de usuario

Imagen de patrocinador para la sección de sponsor

🔹 Añadir botones que redirijan a los diferentes logins
En la página de bienvenida ya existen dos botones: "Soy Usuario" y "Soy Admin" (que ahora será "Soy Sponsor")

Modificar el texto del botón "Soy Admin" a "Soy Sponsor" si es necesario

Asegurar que los botones redirijan correctamente:

"Soy Usuario" → LoginUsuario.xaml

"Soy Sponsor" → LoginSponsor.xaml

2. Realizar el Login de Sponsor
🔹 Crear la página LoginSponsor.xaml
Diseñar una interfaz similar al login de usuario

Añadir campo adicional si es necesario (por ejemplo, código de sponsor)

🔹 Conectar con Firebase Authentication
Usar autenticación por correo y contraseña

🔹 Añadir a la base de datos el sponsor
En Firebase, crear una colección llamada "sponsors"

Al registrar un sponsor, guardar su información en Firestore o Realtime Database

🔹 Validación del login
Verificar que el sponsor exista en la base de datos

Validar credenciales

Redirigir a la página principal de sponsor después del login

3. Configuración de Firebase
🔹 Authentication
Asegurarse de que el método de autenticación por correo y contraseña esté habilitado para sponsors
