using Workshop.API.Middlewares;
using Workshop.API.Services.Integration;

namespace Workshop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient<ICatalogServiceClient, CatalogServiceClient>(client =>
            {
                // Procura o endereço configurado no teu appsettings.json[cite: 1]
                client.BaseAddress = new Uri(builder.Configuration["ExternalServices:PartsCatalogUrl"] ?? "https://localhost:5001");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
