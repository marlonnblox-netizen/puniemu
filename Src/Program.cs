using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Puniemu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ajoute les services nécessaires
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Middleware pour le logging et le routing
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // Fallback route (gère tout ce qui n'est pas un endpoint précis)
                endpoints.MapFallback(async context =>
                {
                    var path = context.Request.Path.Value?.Trim('/') ?? "";
                    var query = context.Request.QueryString.ToString();

                    Console.WriteLine($"[PuniÉmu] Requête reçue : {path}{query}");

                    var response = new
                    {
                        success = true,
                        message = "PuniÉmu Server - Version Française",
                        data = new
                        {
                            timestamp = DateTime.UtcNow,
                            port = 8080,
                            status = "running"
                        }
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });

            app.Run();
        }
    }
}
