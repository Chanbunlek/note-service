using TheFirstProject.DBConnection;
using TheFirstProject.Repository;
using TheFirstProject.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IConnector>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

builder.Services.AddCors(
    cors =>
    {
        cors.AddPolicy("AllowAll", policy =>
        {
            policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
        });
    }
);

var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseMiddleware<ExceptionHandler>();

app.Run();
