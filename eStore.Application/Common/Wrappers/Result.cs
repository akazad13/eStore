namespace eStore.Application.Common.Wrappers
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
            return new Result(true, []);
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }

    public class Success<T>(T results) : IResult<T>
    {
        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<GenericResponse, TResult> onError)
        {
            return onSuccess(results);
        }

        public IResult<TResult> Map<TResult>(Func<T, TResult> f)
        {
            return new Success<TResult>(f(results));
        }

        public IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> f)
        {
            return f(results);
        }
    }

    public class Error<T>(GenericResponse error) : IResult<T>
    {
        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<GenericResponse, TResult> onError)
        {
            return onError(error);
        }

        public IResult<TResult> Map<TResult>(Func<T, TResult> f)
        {
            return new Error<TResult>(error);
        }

        public IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> f)
        {
            return new Error<TResult>(error);
        }
    }
}
