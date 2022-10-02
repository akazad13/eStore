namespace Exino.Application.Common.Wrappers
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }

    public class Success<T> : IResult<T>
    {
        private readonly T _results;

        public Success(T results)
        {
            _results = results;
        }

        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<GenericResponse, TResult> _)
        {
            return onSuccess(_results);
        }

        public IResult<TResult> Map<TResult>(Func<T, TResult> f)
        {
            return new Success<TResult>(f(_results));
        }

        public IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> f)
        {
            return f(_results);
        }
    }

    public class Error<T> : IResult<T>
    {
        private readonly GenericResponse _error;

        public Error(GenericResponse error)
        {
            _error = error;
        }

        public TResult Match<TResult>(Func<T, TResult> _, Func<GenericResponse, TResult> onError)
        {
            return onError(_error);
        }

        public IResult<TResult> Map<TResult>(Func<T, TResult> _)
        {
            return new Error<TResult>(_error);
        }

        public IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> _)
        {
            return new Error<TResult>(_error);
        }
    }
}
