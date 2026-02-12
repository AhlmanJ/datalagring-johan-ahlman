
/*
 * I took this Exception handler from one of our lessons where the teacher showed how to handle exceptions globally and return them to the frontend in Json format.
 * I have expanded this class with the help of chatGPT explaining what each part of the code does and then i have looked for general information about Exception handling on the web.
 * Earlier during the development of this project i used a middleware in the Domain-layer but after reading more about this,
 * i understood it as i should avoid Custom Exceptions unless they are needed to describe very specific exceptions?
 */

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Presentation.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title, detail) = exception switch // Declaring what information to return to frontend when Exception been thrown.
        {
            ArgumentNullException => (StatusCodes.Status400BadRequest, "Bad Request", "Input cannot be empty."),    // Mapping all the Exceptions i got in my solution
            ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request", "Invalid input. Please try again."),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Not Found", "Request Not Found"),

            _ => (StatusCodes.Status500InternalServerError, "Server Error", "An unexpected error occured.")
        };

        httpContext.Response.StatusCode = statusCode; // Sets the httpContext to my different status codes.
       
        return await httpContext.RequestServices    // Returns message to frontend in Json-format.
            .GetRequiredService<IProblemDetailsService>()
            .TryWriteAsync(new ProblemDetailsContext // .TryWriteAsync serialize the message to Json.
            {
                HttpContext = httpContext,
                ProblemDetails = new ProblemDetails
                {
                    Title = title,
                    Detail = detail
                }
            });
    }
}
