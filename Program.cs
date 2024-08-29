using System.Text.Json.Serialization;
using CRUD_Entity_Framework_Core_MVC_01.Helpers;
using CRUD_Entity_Framework_Core_MVC_01.Reopsitories;
using CRUD_Entity_Framework_Core_MVC_01.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddCors();
    services.AddControllers().AddJsonOptions(x => {
        // serialize enums as strings in api responses
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        //ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // services.AddAutoMapper(typeof(Program).Assembly);

    // configure strongly typed settings object
    services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

    //configure container for applications services
    services.AddSingleton<DataContext>();
    services.AddScoped<ISongRepository, SongRepository>();
    services.AddScoped<ISongService, SongService>();
}

var app = builder.Build();

//ensure database and tables exist
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await context.Init();
}

//configure HTTP request pipeline
{
    //global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );

    //global error handler
    app.UseMiddleware<ErrorHandlerMiddleWare>();

    app.MapControllers();
}

app.Run("http://localhost:4000");