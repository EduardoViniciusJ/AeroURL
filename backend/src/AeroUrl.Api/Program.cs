namespace AeroUrl.Api;

public class Program
{

    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "AeroURL is running"); 


        app.Run();

    }

}
