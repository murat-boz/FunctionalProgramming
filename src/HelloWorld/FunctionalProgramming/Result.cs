namespace FunctionalProgramming
{
    public sealed class Result<T>
    {
        public T Value { get; }
        public Statement Error { get; }
        private readonly bool isSuccess;

        private Result(T value)
        {
            this.Value = value;
            this.isSuccess = true;
        }

        private Result(T value, Statement error)
        {
            this.Value = value;
            this.Error = error;
            this.isSuccess = false;
        }

        public static Result<T> Create(T value) => new Result<T>(value);
        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(T value, Statement statement) => new Result<T>(value, statement);
    }
}
