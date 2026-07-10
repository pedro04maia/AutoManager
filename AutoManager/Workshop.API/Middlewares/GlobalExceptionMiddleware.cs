namespace Workshop.API.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Text.Json;

    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Cria o padrão ProblemDetails exigido no guião
            var problemDetails = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Type = "https://httpstatuses.com/500",
                Title = "Ocorreu um erro interno no servidor.",
                Detail = exception.Message // Em produção omitia-se, mas para o projeto ajuda a depurar
            };

            var json = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(json);
        }
    }
}