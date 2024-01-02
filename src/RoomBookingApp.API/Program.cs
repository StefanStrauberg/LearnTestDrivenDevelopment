using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Persistence;
using RoomBookingApp.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = "Filename=:memory:";
var connection = new SqliteConnection(connectionString);
connection.Open();
builder.Services.AddDbContext<RoomBookingAppDbContext>(
        opt => opt.UseSqlite(connection));
EnsureDatabaseCreated(connection);
builder.Services.AddScoped<IRoomBookingService, RoomBookingService>();
builder.Services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();

static void EnsureDatabaseCreated(SqliteConnection con)
{
    var dbContOptBuilder = new DbContextOptionsBuilder<RoomBookingAppDbContext>();
    dbContOptBuilder.UseSqlite(con);

    using var context = new RoomBookingAppDbContext(dbContOptBuilder.Options);
    context.Database.EnsureCreated();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();