using OficinalAPI.Middleware;
using OficinalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Oficina Mecânica",
        Version = "v1",
        Description = "API para gerenciar orçamentos de serviços em oficina mecânica",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Suporte",
            Email = "suporte@oficina.com"
        }
    });
});

// Registrar serviços de negócio (Dependency Injection)
builder.Services.AddScoped<IOrcamentoService, OrcamentoService>();

// Configurar CORS (se necessário)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodos", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Adicionar middleware de tratamento de exceções
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configurar o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Oficina Mecânica v1");
        options.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.UseHttpsRedirection();
app.UseCors("PermitirTodos");
app.UseAuthorization();
app.MapControllers();

app.Run();
