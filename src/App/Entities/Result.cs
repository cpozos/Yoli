namespace Yoli.Core.App.Entities
{
    public record Result<T>
    {
        private bool? _success;
        public bool Succeeded => _success ??= !Errors.Any() && !Data.IsNullOrDefault();
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
        public T Data { get; set; } = default!;
    }

    public static class ObjectExtensions
    {
        public static bool IsNullOrDefault<T>(this T argument)
        {
            // deal with normal scenarios
            if (argument == null) return true;
            if (object.Equals(argument, default(T))) return true;

            // deal with non-null nullables
            Type methodType = typeof(T);
            if (Nullable.GetUnderlyingType(methodType) != null) return false;

            // deal with boxed value types
            Type argumentType = argument.GetType();
            if (argumentType.IsValueType && argumentType != methodType)
            {
                object obj = Activator.CreateInstance(argument.GetType());
                return obj.Equals(argument);
            }

            return false;
        }
    }
}