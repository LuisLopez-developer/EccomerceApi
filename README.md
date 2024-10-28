## Cómo empezar
1. Genera un Fork del proyecto

2. Clona el repositorio en una carpeta local en tu máquina.
```bash
git clone https://github.com/LuisLopez-developer/EccomerceApi.git
```
3. Luego Abre Herramientas -> Administrador de paquetes NuGet -> Consola del Administrador de paquetes

![image](https://github.com/LuisLopez-developer/EccomerceApi/assets/156825396/cc0fd1a8-b982-485a-8647-f2efcf82e0c3)

> [!NOTE]
> Antes de ejecutar los comandos, asegúrate de que el proyecto `Data` esté seleccionado en la consola.
> 
> ![image](https://github.com/user-attachments/assets/f80c49fe-62e2-447d-8ff3-36098501f3e1)


4. Genera las migraciones:
```bash
Add-Migration InitialCreate
```

5. Genera las tablas:
```bash
Update-Database
```   
> [!WARNING]
> Verifica que tu `appsettings.Development.json`, el  `ConnectionStrings`,
>  este con las credenciales de tu servidor SQL Server.

> [!NOTE]
> Valida en tu SSMS, que se hayan generado correctamente las migraciones.

6. Por ultimo:
   
> [!important]
> Ejecuta el proyecto por HTTPS
> 
> ![image](https://github.com/LuisLopez-developer/EccomerceApi/assets/156825396/e67c0399-dc85-4ae0-8e2f-8d097fa115c7)

6. Publicación del Proyecto:
```bash
https://eccomerceapi.fly.dev/
```
