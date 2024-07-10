namespace SharedKernel
{
    public  record Error(string Code, string? Descripcion = null) : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty);
        public static readonly Error NullValue = new("Error.NullValue");
        public static readonly Error Unknow = new("Error.Desconocido");
    }
}