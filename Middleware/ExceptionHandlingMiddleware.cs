using OficinalAPI.Exceptions;
using OficinalAPI.Models;
using System.Net;

namespace OficinalAPI.Middleware
{
    /// <summary>
    /// Middleware para tratamento global de exceções
    /// Padrão: Exception Handling Middleware
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            ErroResponse response;

            switch (exception)
            {
                case OrcamentoValidacaoException validacaoEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ErroResponse(
                        "VALIDACAO_ERRO",
                        "Os dados fornecidos são inválidos.",
                        validacaoEx.Erros
                    );
                    break;

                case RecursoNaoEncontradoException naoEncontradoEx:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new ErroResponse(
                        "RECURSO_NAO_ENCONTRADO",
                        naoEncontradoEx.Message
                    );
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = new ErroResponse(
                        "ERRO_INTERNO",
                        "Ocorreu um erro interno no servidor."
                    );
                    break;
            }

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
