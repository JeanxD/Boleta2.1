using Microsoft.EntityFrameworkCore;
using FloanyVisionWeb.Models; // Asegura que apunte a donde está tu archivo de contexto

var builder = WebApplication.CreateBuilder(args);

// ===> REGISTRO DE LA BASE DE DATOS LOCAL (EN MEMORIA RAM) <===
// Esto hace que el sistema funcione al 100% sin depender de internet ni de Supabase
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("FloanyVisionLocalDb"));

// Pausamos temporalmente Supabase para evitar errores de red ("Host desconocido")
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Nota: Si te da advertencias de HTTPS en local, puedes comentar esta línea poniendo // adelante
app.UseHttpsRedirection();
app.UseStaticFiles(); // Cambiado a UseStaticFiles para mayor compatibilidad en .NET 9 mvc estándar

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();