using Microsoft.EntityFrameworkCore;
using ProjectReactAndNet.Application.Services;
using ProjectReactAndNet.Domain.Interfaces;
using ProjectReactAndNet.Infrastructure.Data;
using ProjectReactAndNet.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Data Base
// Opção 1 — SQL Server (recomendado para produção)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Opção 2 — SQLite (comentar a linha acima e descomentar abaixo, para desenvolvimento rápido)
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlite("Data Source=app.db"));

//Injeção de Dependência
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",  // Vite (dev)
                "http://localhost:3000"   // CRA (dev)
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MyApp API", Version = "v1" });
});

var app = builder.Build();

//Middlewares 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await ApplyMigrationsAsync(app); // Aplica migrations automaticamente no dev
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); // CORS DEVE vir antes de MapControllers
app.UseAuthorization();
app.MapControllers();

app.Run();

//Aplicar migrations automaticamente
static async Task ApplyMigrationsAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}
