namespace Exino.Application.Common.Wrappers
{
    public class GenericResponse
    {
        public GenericResponse(string message, IEnumerable<string> errors)
        {
            Message = message;
            Errors = errors.ToArray();
        }

        public string   Message { get; set; }
        public string[] Errors { get; set; }
        public bool ValidationError { get; set; } = false;
    }

    public static class Response<T>
    {
        public static IResult<T> SuccessResponese(T value)
        {
            return new Success<T>(value);
        }

        public static IResult<T> ErrorResponse(IEnumerable<string> errors)
        {
            return new Error<T>(new GenericResponse("Error Occured", errors));
        }

        public static IResult<T> ErrorResponse(GenericResponse genericResponse)
        {
            return new Error<T>(genericResponse);
        }

        public static IResult<GenericResponse> SuccessResponese(string message)
        {
            return new Success<GenericResponse>(new GenericResponse(message, Array.Empty<string>()));
        }
    }
}
