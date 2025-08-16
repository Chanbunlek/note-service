using TheFirstProject.DBConnection;
using TheFirstProject.Repository;
using TheFirstProject.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IConnector>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseMiddleware<ExceptionHandler>();

app.Run();
