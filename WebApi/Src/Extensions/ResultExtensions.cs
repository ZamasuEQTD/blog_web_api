using SharedKernel;
using SharedKernel.Abstractions;

namespace WebApi.Extensions
{
    public static class ResultExtensions
    {
        static public IResult ToProblem(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Result es valido!");
            }
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                extensions: new Dictionary<string, object?>{
                    {
                        "errors",new [] {result.Error}
                    }
                }
            );
        }
    }
    public static class ErrorExtensions
    {
        public static IResult HandleFailure(this Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(""),
            IValidationResult r => CreateDetails(
                    "Validation Error",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    r.Errors
                ),
            _ => CreateDetails(
                    result.Error.Code,
                    StatusCodes.Status400BadRequest,
                    result.Error
                )
        };

        private static IResult CreateDetails(
            string title,
            int status,
            Error error,
            Error[]? errors = null
        )
        {
            return Results.Problem(
                title: title,
                statusCode: status,
                detail: error.Descripcion,
                extensions: new Dictionary<string, object?>{
                    {
                        nameof(errors),errors
                    }
                }
           );
        }
    }
}