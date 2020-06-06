namespace Witness
{
    using System;

    /// <summary>
    /// <see cref="IValidationContext{T,TR}"/> extensions.
    /// </summary>
    public static class ValidationContextExtensions
    {
        /// <summary>
        /// Map validation context to change object under validation.
        /// </summary>
        /// <param name="context">Current validation context.</param>
        /// <param name="map">Map function.</param>
        /// <param name="valueName">New OUV name.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <typeparam name="TR">Type of current OUV.</typeparam>
        /// <typeparam name="T2">Type of mapped OUV.</typeparam>
        /// <returns>Validation context with T2 OUV.</returns>
        public static IValidationContext<T, T2> Map<T, TR, T2>(this IValidationContext<T, TR> context,  Func<IValidationContext<T, TR>, T2> map, string valueName)
            where T : class
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            var contextInstance = context;
            return new ValidationContext<T, T2>(contextInstance.RootOUV, map(contextInstance), valueName, contextInstance.ValidationErrors);
        }
    }
}