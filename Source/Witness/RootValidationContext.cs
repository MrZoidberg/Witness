namespace Witness
{
    using System;

    /// <summary>
    /// Root validation context object.
    /// </summary>
    /// <typeparam name="T">Type of the root object under validation.</typeparam>
    public class RootValidationContext<T> : ValidationContext<T, T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootValidationContext{T}"/> class.
        /// </summary>
        /// <param name="rootUov">Root UOV.</param>
        /// <exception cref="ArgumentException">Thrown when root OUV has wrong type.</exception>
        public RootValidationContext(T rootUov)
            : base(rootUov)
        {
        }
    }
}