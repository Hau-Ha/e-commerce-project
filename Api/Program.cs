
using Api.src.Database;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<DatabaseContext>();
        


        var app = builder.Build();

        app.UseHttpsRedirection();
     
        app.UseSwagger();


        app.UseAuthentication(); /* check if user exists in database ? */

        app.UseAuthorization(); /*  personal information, validation */

        app.MapControllers();

        app.Run();
    }
}
