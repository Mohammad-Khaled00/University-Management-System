using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using UniversityManagement.Core.Models;
using UniversityManagement.Core.ViewModels;
using UniversityManagement.Infrastructure.Data;

namespace UniversityManagement.Infrastructure.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Capture request
            var request = await FormatRequest(context.Request);

            // Capture response
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await _next(context);

                    // Log request and response
                    ResponseEntity<string> response = await FormatResponse(context.Response);

                    _logger.LogInformation($"Request: {request}");
                    _logger.LogInformation($"Response: {response}");

                    // Save log to database
                    SaveLogToDatabase(context, request, response);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred: {ex}");
                }

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = string.Empty;

            // Check if the request has a body
            if (request.ContentLength.HasValue && request.ContentLength > 0 &&
                request.ContentType != null && request.ContentType.Contains("application/json"))
            {
                request.EnableBuffering(); // Enable buffering of the request body stream

                using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = await reader.ReadToEndAsync();
                    body = body.Replace("\n", "").Replace("\"", "").Replace(", ", "");
                    // Reset the position of the request body stream
                    request.Body.Seek(0, SeekOrigin.Begin);
                }
            }
            return $"{body}";
        }

        private async Task<ResponseEntity<string>> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return new ResponseEntity<string>
            {
                Data = text,
                StatusCode = response.StatusCode,
                Errors = null // Assuming we don't have errors to log in this context
            };
        }

        private void SaveLogToDatabase(HttpContext context, string request, ResponseEntity<string> responseEntity)
        {
            using (var dbContext = new UniverisityDbContext())
            {
                var logEntry = new RequestResponseLog
                {
                    RequestUrl = context.Request.Path ,
                    RequestMethod = context.Request.Method,
                    RequestBody = request,
                    ResponseStatusCode = responseEntity.StatusCode,
                    ResponseBody = responseEntity.Data?.Replace("\"", "") ?? "",
                    LogDateTime = DateTime.Now
                };
                dbContext.RequestResponseLogs.Add(logEntry);
                dbContext.SaveChanges();
            }
        }
    }
}
