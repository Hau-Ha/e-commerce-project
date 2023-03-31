
using Api.src.Database;
using Api.src.Repositories.UserRepo;
using Api.src.Services.UserService;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

y

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer ();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<DatabaseContext>();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);


        builder.Services
           .AddScoped<IUserRepo, UserRepo>()
           .AddScoped<IUserService, UserService>();



        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo");
            options.RoutePrefix = string.Empty;
        });


        app.UseAuthentication(); /* check if user exists in database ? */

        app.UseAuthorization(); /*  personal information, validation */

        app.MapControllers();

        app.Run();
    }
}
